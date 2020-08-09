using ContaService.Domain.AggregatesModel.ContaCorrenteAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ContaService.Infrastructure.EntityConfiguration
{
    public class ContaCorrenteEntityTypeConfiguration : IEntityTypeConfiguration<ContaCorrente>
    {
        public void Configure(EntityTypeBuilder<ContaCorrente> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property<string>("Numero").IsRequired();
            builder.Property(x => x.Saldo);

            var navigation = builder.Metadata.FindNavigation(nameof(ContaCorrente.Lancamentos));
            navigation.SetPropertyAccessMode(PropertyAccessMode.Field);

            builder.ToTable("ContaCorrente");
        }
    }
}