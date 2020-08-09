using System;
using ContaService.Domain.AggregatesModel.ContaCorrenteAggregate;
using ContaService.Domain.SeedWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ContaService.Infrastructure.EntityConfiguration
{
    public class LancamentoEntityTypeConfiguration : IEntityTypeConfiguration<Lancamento>
    {
        public void Configure(EntityTypeBuilder<Lancamento> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property<decimal>("Valor").IsRequired();
            builder.Property<DateTime>("DataLancamento").IsRequired();
            builder.Property<TipoLancamento>("TipoLancamento").IsRequired();

            builder.ToTable("Lancamento");
        }
    }
}