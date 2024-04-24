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
        public async Task<ActionResult<IEnumerable<AnimalDTO>>> GetAll()
        {
            var animals = await _uof.AnimalRepository.GetAllAsync();

            if (animals is null) return NotFound();

            var animalsDto = _mapper.Map<IEnumerable<AnimalDTO>>(animals);

            return Ok(animalsDto);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<AnimalDTO>> GetById(int id)
        {
            var animal = await _uof.AnimalRepository.GetByIdAsync(p => p.AnimalID == id);

            if (animal is null) return NotFound();

            var animalDto = _mapper.Map<AnimalDTO>(animal);

            return Ok(animalDto);
        }

        [HttpGet("pagionation")]
        public async Task<ActionResult<IEnumerable<AnimalDTO>>> Get([FromQuery] AnimalsParameters animalsParameters)
        {
            var animals = await _uof.AnimalRepository.GetAnimalsAsync(animalsParameters);

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

        [HttpGet("filter/gender/pagionation")]
        public async Task<ActionResult<IEnumerable<AnimalDTO>>> GetFiltered([FromQuery] AnimalsFilter animalsFilter)
        {
            var animals = await _uof.AnimalRepository.GetFilteredAnimalsAsync(animalsFilter);

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
        public async Task<ActionResult<AnimalDTO>> Create(AnimalDTO animalDto)
        {
            if (animalDto is null) return BadRequest();

            var animal = _mapper.Map<Animal>(animalDto);

            _uof.AnimalRepository.Create(animal);

            await _uof.CommitAsync();

            var animalDtoCreated = _mapper.Map<AnimalDTO>(animal);

            return Ok(animalDtoCreated);
        }

        [HttpPut]
        public async Task<ActionResult<AnimalDTO>> Update(int id, AnimalDTO animalDto)
        {
            var animal = _mapper.Map<Animal>(animalDto);

            if(id != animal.AnimalID) return NotFound();

            var animalUpdated = _uof.AnimalRepository.Update(animal);
            await _uof.CommitAsync();

            var animalDtoUpdated = _mapper.Map<AnimalDTO>(animalUpdated);

            return Ok(animalDtoUpdated);
        }

        [HttpDelete]
        public async Task<ActionResult<AnimalDTO>> Delete(int id)
        {
            var animal = await _uof.AnimalRepository.GetByIdAsync(p => p.AnimalID == id);
            if (animal is null) return NotFound();

            _uof.AnimalRepository.Delete(animal);
            await _uof.CommitAsync();

            var animalRemovedDto = _mapper.Map<AnimalDTO>(animal);

            return Ok(animalRemovedDto);
        }
    }
}
