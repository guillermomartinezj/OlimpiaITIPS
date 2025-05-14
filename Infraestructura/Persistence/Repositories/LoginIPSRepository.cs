using Dominio.Olimpia.Autenticacion;

namespace Infraestructura.Persistence.Repositories
{
    class LoginIPSRepository: ILoginIPSRepository
    {
        private readonly AuthenticationDbContext _context;

        public LoginIPSRepository(AuthenticationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public async Task<(int?, string?)> CheckPasswordSignInAsync(string userName, string password)
        {
            var user = await _context.LoginIPSs.FirstOrDefaultAsync(prop =>
                prop.Estado &&
                prop.UserName.Equals(userName) &&
                prop.Clave.Equals(password));

            return (user?.IdTipoAccesoIPS, user?.UserName);
        }
    }
}
