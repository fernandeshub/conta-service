﻿// <auto-generated />
using System;
using ContaService.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ContaService.Infrastructure.Migrations
{
    [DbContext(typeof(ContaServiceContext))]
    partial class ContaServiceContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024");

            modelBuilder.Entity("ContaService.Domain.AggregatesModel.ContaCorrenteAggregate.ContaCorrente", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Numero")
                        .IsRequired();

                    b.Property<decimal>("Saldo");

                    b.HasKey("Id");

                    b.ToTable("ContaCorrente");
                });

            modelBuilder.Entity("ContaService.Domain.AggregatesModel.ContaCorrenteAggregate.Lancamento", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("ContaCorrenteId");

                    b.Property<DateTime>("DataLancamento");

                    b.Property<int>("TipoLancamento");

                    b.Property<decimal>("Valor");

                    b.HasKey("Id");

                    b.HasIndex("ContaCorrenteId");

                    b.ToTable("Lancamento");
                });

            modelBuilder.Entity("ContaService.Domain.AggregatesModel.ContaCorrenteAggregate.Lancamento", b =>
                {
                    b.HasOne("ContaService.Domain.AggregatesModel.ContaCorrenteAggregate.ContaCorrente")
                        .WithMany("Lancamentos")
                        .HasForeignKey("ContaCorrenteId");
                });
#pragma warning restore 612, 618
        }
    }
}
