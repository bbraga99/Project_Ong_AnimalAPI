using Ong_AnimalAPI.Context;
using Ong_AnimalAPI.Models;

namespace Ong_AnimalAPI.Repositories
{
    public class AnimalRepository : Repository<Animal>, IAnimalRepository
    {
        public AnimalRepository(AppDbContext context) : base(context)
        {
        }
    }
}
