using ContaService.Infrastructure.EntityConfiguration;
using Microsoft.EntityFrameworkCore;
using ContaService.Domain.Models;

namespace ContaService.Infrastructure
{
    public class ContaServiceContext : DbContext
    {
         public ContaServiceContext(DbContextOptions<ContaServiceContext> options) : base(options){}

        public DbSet<ContaCorrente> ContasCorrentes { get; set; }
        public DbSet<Lancamento> Lancamentos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ContaCorrenteEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new LancamentoEntityTypeConfiguration());
        }
    }
}