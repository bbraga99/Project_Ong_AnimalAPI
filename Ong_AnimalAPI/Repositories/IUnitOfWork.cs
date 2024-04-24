namespace Ong_AnimalAPI.Repositories
{
    public interface IUnitOfWork
    {
        IAnimalRepository AnimalRepository { get; }
        Task CommitAsync();    
    }
}
