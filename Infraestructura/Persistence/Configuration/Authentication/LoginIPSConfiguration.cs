using Dominio.Olimpia.Autenticacion;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestructura.Persistence.Configuration.Authentication
{
    class LoginIPSConfiguration : IEntityTypeConfiguration<LoginIPS>
    {
        private const string Schema = "dbo";
        public void Configure(EntityTypeBuilder<LoginIPS> builder)
        {
            builder.ToTable("LoginIPS", Schema);
            builder.HasKey(prop => prop.IdLoginIPS);
            builder.Property(prop => prop.IdLoginIPS).HasColumnName("IdLoginIPS");
            builder.Property(prop => prop.IdTipoAccesoIPS).HasColumnName("IdTipoAccesoIPS");
            builder.Property(prop => prop.UserName).HasColumnName("UserName");
            builder.Property(prop => prop.Clave).HasColumnName("Clave");
            builder.Property(prop => prop.Estado).HasColumnName("Estado");
        }
    }
}
