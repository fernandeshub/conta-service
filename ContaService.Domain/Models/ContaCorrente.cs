using System;
using System.Collections.Generic;
using ContaService.Domain.SeedWork;

namespace ContaService.Domain.Models
{
    public class ContaCorrente
    {
        public int Id { get; private set; }
        public string Numero { get; private set; }
        public decimal Saldo { get; private set; }

        public ContaCorrente(int id, string numero, decimal saldo)
        {
            this.Id = id;
            this.Numero = numero;
            this.Saldo = saldo;
        }

        public void Debitar(decimal valor)
        {
            this.Saldo = this.Saldo - valor;
        }

        public void Creditar(decimal valor)
        {
            this.Saldo = this.Saldo + valor;
        }

    }
}