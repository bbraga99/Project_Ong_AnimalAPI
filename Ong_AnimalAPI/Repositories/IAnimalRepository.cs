using Ong_AnimalAPI.Models;
using Ong_AnimalAPI.Pagination;

namespace Ong_AnimalAPI.Repositories
{
    public interface IAnimalRepository : IRepository<Animal>
    {
        public Task<PagedList<Animal>> GetAnimalsAsync(AnimalsParameters animals);
        public Task<PagedList<Animal>> GetFilteredAnimalsAsync(AnimalsFilter animalFilter);
    }
}
