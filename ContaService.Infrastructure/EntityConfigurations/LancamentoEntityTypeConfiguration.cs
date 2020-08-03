using ContaService.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ContaService.Infrastructure.EntityConfiguration
{
    public class LancamentoEntityTypeConfiguration : IEntityTypeConfiguration<Lancamento>
    {
        public void Configure(EntityTypeBuilder<Lancamento> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Valor).IsRequired();
            builder.Property(x => x.Data).IsRequired();
            builder.OwnsOne(x => x.Conta).HasForeignKey("ContaId");
            builder.Property(x => x.TipoOperacao).IsRequired();

            builder.ToTable("Lancamentos");
        }
    }
}