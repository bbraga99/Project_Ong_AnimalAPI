namespace Ong_AnimalAPI.Repositories
{
    public interface IUnitOfWork
    {
        IAnimalRepository AnimalRepository { get; }
        void Commit();    
    }
}
