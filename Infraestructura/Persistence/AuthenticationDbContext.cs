using Dominio.Olimpia.Autenticacion;
using Dominio.Primitives;
using Infraestructura.Persistence.Configuration.Authentication;
using System.Data;

namespace Infraestructura.Persistence
{
    public class AuthenticationDbContext : DbContext, IUnitOfWorkAuthentication
    {
        private readonly IPublisher _publisher;

        public AuthenticationDbContext(DbContextOptions<AuthenticationDbContext> options, IPublisher publisher) : base(options)
        {
            _publisher = publisher ?? throw new ArgumentNullException(nameof(publisher));
        }

        public DbSet<HistoryRefreshToken> HistoryRefreshTokens { get; set; }
        public DbSet<LoginIPS> LoginIPSs { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AuthenticationAssembly).Assembly);

            base.OnModelCreating(modelBuilder);
        }


        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            var domainEvents = ChangeTracker.Entries<AggregateRoot>()
                .Select(e => e.Entity)
                .Where(e => e.GetDomainEvents().Any())
                .SelectMany(e => e.GetDomainEvents());

            var result = await base.SaveChangesAsync(cancellationToken);

            foreach (var domainEvent in domainEvents)
            {
                await _publisher.Publish(domainEvent, cancellationToken);
            }

            return result;
        }
    }
}
