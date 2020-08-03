using System;
using ContaService.Domain.Models;
using ContaService.Domain.SeedWork;
using Microsoft.EntityFrameworkCore;

namespace ContaService.Infrastructure
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            var conta1 = new ContaCorrente(1, "12345-6", 1000);
            var conta2 = new ContaCorrente(2, "54321-0", 1000);
            var conta3 = new ContaCorrente(3, "02468-9", 1000);

            modelBuilder.Entity<ContaCorrente>()
                .HasData(conta1, conta2, conta3);

            modelBuilder.Entity<Lancamento>()
                .HasData(
                   new Lancamento(1, 1000, DateTime.UtcNow, 1, TipoOperacao.Credito),
                   new Lancamento(2, 1000, DateTime.UtcNow, 2, TipoOperacao.Credito),
                   new Lancamento(3, 1000, DateTime.UtcNow, 3, TipoOperacao.Credito)
            );
        }
    }
}