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
            modelBuilder.Entity<Usuario>()
               .HasData(
                   new Usuario { Id = 1, AccessKey = "6a15395e10db3e76b4a953ffb439d32da" },
                   new Usuario { Id = 2, AccessKey = "a4ae2f7f58dc1e0973fb79c9677a09161" }
               );

            modelBuilder.Entity<ContaCorrente>()
                .HasData(
                    new ContaCorrente(1, "12345-6", 1000),
                    new ContaCorrente(2, "54321-0", 1000),
                    new ContaCorrente(3, "02468-9", 1000)
                );

            modelBuilder.Entity<Lancamento>()
                .HasData(
                   new Lancamento(1, 1, 1000, 1000, DateTime.UtcNow, TipoOperacao.Credito),
                   new Lancamento(2, 2, 1000, 1000, DateTime.UtcNow, TipoOperacao.Credito),
                   new Lancamento(3, 3, 1000, 1000, DateTime.UtcNow, TipoOperacao.Credito)
                );
        }
    }
}