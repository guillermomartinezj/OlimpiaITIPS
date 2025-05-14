using Dominio.Olimpia.Autenticacion;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestructura.Persistence.Configuration.Authentication
{
    public class HistoryRefreshTokenConfiguration : IEntityTypeConfiguration<HistoryRefreshToken>
    {
        private const string Schema = "dbo";
        public void Configure(EntityTypeBuilder<HistoryRefreshToken> builder)
        {
            builder.ToTable("HistorialRefreshToken", Schema);
            builder.HasKey(h => h.Id);
            builder.Property(h => h.Id).HasColumnName("IdHistorialToken");
            builder.Property(h => h.AuthenticationId).HasColumnName("IdAutenticacion");
            builder.Property(h => h.ClientId).HasColumnName("IdCliente");
            builder.Property(h => h.Token).HasColumnName("Token");
            builder.Property(h => h.RefreshToken).HasColumnName("RefreshToken");
            builder.Property(h => h.CreationDate).HasColumnName("FechaCreacion");
            builder.Property(h => h.ExpirationDate).HasColumnName("FechaExpiracion");
        }
    }
}
