﻿// <auto-generated />
using System;
using ContaService.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ContaService.API.Migrations
{
    [DbContext(typeof(ContaServiceContext))]
    partial class ContaServiceContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024");

            modelBuilder.Entity("ContaService.Domain.Models.Lancamento", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ContaId");

                    b.Property<DateTime>("Data");

                    b.Property<int>("TipoOperacao");

                    b.Property<decimal>("Valor");

                    b.HasKey("Id");

                    b.ToTable("Lancamentos");

                    b.HasData(
                        new { Id = 1, ContaId = 1, Data = new DateTime(2020, 8, 3, 12, 12, 6, 567, DateTimeKind.Utc), TipoOperacao = 1, Valor = 1000m },
                        new { Id = 2, ContaId = 2, Data = new DateTime(2020, 8, 3, 12, 12, 6, 567, DateTimeKind.Utc), TipoOperacao = 1, Valor = 1000m },
                        new { Id = 3, ContaId = 3, Data = new DateTime(2020, 8, 3, 12, 12, 6, 567, DateTimeKind.Utc), TipoOperacao = 1, Valor = 1000m }
                    );
                });

            modelBuilder.Entity("ContaService.Domain.Models.Lancamento", b =>
                {
                    b.OwnsOne("ContaService.Domain.Models.ContaCorrente", "Conta", b1 =>
                        {
                            b1.Property<int>("ContaId");

                            b1.Property<string>("Numero")
                                .IsRequired();

                            b1.Property<decimal>("Saldo");

                            b1.ToTable("ContaCorrente");

                            b1.HasOne("ContaService.Domain.Models.Lancamento")
                                .WithOne("Conta")
                                .HasForeignKey("ContaService.Domain.Models.ContaCorrente", "ContaId")
                                .OnDelete(DeleteBehavior.Cascade);

                            b1.HasData(
                                new { ContaId = 1, Numero = "12345-6", Saldo = 1000m },
                                new { ContaId = 2, Numero = "54321-0", Saldo = 1000m },
                                new { ContaId = 3, Numero = "02468-9", Saldo = 1000m }
                            );
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
