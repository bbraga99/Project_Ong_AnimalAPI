using Ong_AnimalAPI.Models;
using Ong_AnimalAPI.Pagination;

namespace Ong_AnimalAPI.Repositories
{
    public interface IAnimalRepository : IRepository<Animal>
    {
        public PagedList<Animal>GetAnimals(AnimalsParameters animals);
    }
}
