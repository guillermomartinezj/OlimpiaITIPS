namespace Dominio.Olimpia.Autenticacion
{
    public interface ILoginIPSRepository
    {
        Task<(int?, string?)> CheckPasswordSignInAsync(string userName, string password);
    }
}
