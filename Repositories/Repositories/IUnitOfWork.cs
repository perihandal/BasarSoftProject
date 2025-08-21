namespace BasarSoftProject.Repositories
{
    public interface IUnitOfWork
    {
        Task<int> SaveChangesAsync();
    }
}