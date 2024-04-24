using Ong_AnimalAPI.Context;
using Ong_AnimalAPI.DTOs;
using Ong_AnimalAPI.Models;
using Ong_AnimalAPI.Pagination;

namespace Ong_AnimalAPI.Repositories
{
    public class AnimalRepository : Repository<Animal>, IAnimalRepository
    {
        public AnimalRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<PagedList<Animal>> GetAnimalsAsync(AnimalsParameters animalsParameters)
        {
            var animals = await GetAllAsync();
            
            var animalsOrdered = animals.OrderBy(p => p.AnimalID).AsQueryable();
            
            var animalsOrder = PagedList<Animal>.ToPagedList(animalsOrdered, animalsParameters.PageNumber, animalsParameters.PageSize);

            return animalsOrder;
        }

        public async Task<PagedList<Animal>> GetFilteredAnimalsAsync(AnimalsFilter animalFilter)
        {
            var animals = await GetAllAsync();

            if(!string.IsNullOrEmpty(animalFilter.Gender))
            {
                animals = animals.Where(p => p.Gender == animalFilter.Gender);
            }

            var animalFiltered = PagedList<Animal>.ToPagedList(animals.AsQueryable(), animalFilter.PageSize, animalFilter.PageNumber);

            return animalFiltered;
        }
    }
}
