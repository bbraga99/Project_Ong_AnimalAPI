using Ong_AnimalAPI.Context;

namespace Ong_AnimalAPI.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private IAnimalRepository _animalRepository;

        public AppDbContext _context;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        public IAnimalRepository AnimalRepository
        {
            get
            {
                return _animalRepository = _animalRepository ?? new AnimalRepository(_context);
            }
        }

        public void Commit()
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
