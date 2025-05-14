namespace Dominio.Primitives
{
    public interface IUnitOfWorkAuthentication
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
