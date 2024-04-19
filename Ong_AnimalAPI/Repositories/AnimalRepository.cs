using Ong_AnimalAPI.Context;
using Ong_AnimalAPI.Models;
using Ong_AnimalAPI.Pagination;

namespace Ong_AnimalAPI.Repositories
{
    public class AnimalRepository : Repository<Animal>, IAnimalRepository
    {
        public AnimalRepository(AppDbContext context) : base(context)
        {
        }

        public PagedList<Animal> GetAnimals(AnimalsParameters animalsParameters)
        {
            var animals = GetAll().OrderBy(p => p.AnimalID).AsQueryable();
            var animalsOrder = PagedList<Animal>.ToPagedList(animals, animalsParameters.PageNumber, animalsParameters.PageSize);

            return animalsOrder;
        }
    }
}
