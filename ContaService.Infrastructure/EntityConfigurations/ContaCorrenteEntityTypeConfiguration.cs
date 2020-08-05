using ContaService.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ContaService.Infrastructure.EntityConfiguration
{
    public class ContaCorrenteEntityTypeConfiguration : IEntityTypeConfiguration<ContaCorrente>
    {
        public void Configure(EntityTypeBuilder<ContaCorrente> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Numero).IsRequired();
            builder.Property(x => x.Saldo).IsRequired();
           

            builder.ToTable("ContaCorrente");
        }
    }
}