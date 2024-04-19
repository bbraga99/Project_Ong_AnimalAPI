using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Ong_AnimalAPI.DTOs;
using Ong_AnimalAPI.Models;
using Ong_AnimalAPI.Pagination;
using Ong_AnimalAPI.Repositories;

namespace Ong_AnimalAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnimalController : ControllerBase
    {
        private readonly IUnitOfWork _uof;
        private readonly IMapper _mapper;

        public AnimalController(IUnitOfWork uof, IMapper mapper)
        {
            _uof = uof;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<AnimalDTO>> GetAll()
        {
            var animals = _uof.AnimalRepository.GetAll();

            if (animals is null) return NotFound();

            var animalsDto = _mapper.Map<IEnumerable<AnimalDTO>>(animals);

            return Ok(animalsDto);
        }

        [HttpGet("{id:int}")]
        public ActionResult<AnimalDTO> GetById(int id)
        {
            var animal = _uof.AnimalRepository.GetById(p => p.AnimalID == id);

            if (animal is null) return NotFound();

            var animalDto = _mapper.Map<AnimalDTO>(animal);

            return Ok(animalDto);
        }

        [HttpGet("pagionation")]
        public ActionResult<IEnumerable<AnimalDTO>> Get([FromQuery] AnimalsParameters animalsParameters)
        {
            var animals = _uof.AnimalRepository.GetAnimals(animalsParameters);

            var metada = new
            {
                animals.TotalCount,
                animals.PageSize,
                animals.CurrentPage,
                animals.TotalPages,
                animals.HasNext,
                animals.HasPrevious
            };

            Response.Headers.Append("X-Pagination", JsonConvert.SerializeObject(metada));

            return Ok(_mapper.Map<IEnumerable<AnimalDTO>>(animals));  
        } 

        [HttpPost]
        public ActionResult<AnimalDTO> Create(AnimalDTO animalDto)
        {
            if (animalDto is null) return BadRequest();

            var animal = _mapper.Map<Animal>(animalDto);

            _uof.AnimalRepository.Create(animal);

            _uof.Commit();

            var animalDtoCreated = _mapper.Map<AnimalDTO>(animal);

            return Ok(animalDtoCreated);
        }

        [HttpPut]
        public ActionResult<AnimalDTO> Update(int id, AnimalDTO animalDto)
        {
            var animal = _mapper.Map<Animal>(animalDto);

            if(id != animal.AnimalID) return NotFound();

            var animalUpdated = _uof.AnimalRepository.Update(animal);
            _uof.Commit();

            var animalDtoUpdated = _mapper.Map<AnimalDTO>(animalUpdated);

            return Ok(animalDtoUpdated);
        }

        [HttpDelete]
        public ActionResult<AnimalDTO> Delete(int id)
        {
            var animal = _uof.AnimalRepository.GetById(p => p.AnimalID == id);
            if (animal is null) return NotFound();

            _uof.AnimalRepository.Delete(animal);
            _uof.Commit();

            var animalRemovedDto = _mapper.Map<AnimalDTO>(animal);

            return Ok(animalRemovedDto);
        }
    }
}
