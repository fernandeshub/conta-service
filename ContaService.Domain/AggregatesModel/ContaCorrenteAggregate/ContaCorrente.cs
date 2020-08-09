using System;
using System.Collections.Generic;
using ContaService.Domain.SeedWork;

namespace ContaService.Domain.AggregatesModel.ContaCorrenteAggregate
{
    public class ContaCorrente : Entity, IAggregateRoot
    {
        public string Numero { get; private set; }
        public decimal Saldo { get; private set; }
        private readonly List<Lancamento> _lancamentos;
        public IReadOnlyCollection<Lancamento> Lancamentos => _lancamentos;
       
        public ContaCorrente()
        {
            _lancamentos = new List<Lancamento>();
        }

        public ContaCorrente(string numero)
        {
            Numero = numero;
            _lancamentos = new List<Lancamento>();
        }

        public void Debitar(decimal valor)
        {
            Saldo -= valor;
            AdicionarLancamento(Decimal.Negate(valor), TipoLancamento.Debito);
        }

        public void Creditar(decimal valor)
        {
            Saldo += valor;
            AdicionarLancamento(valor, TipoLancamento.Credito);
        }

        private void AdicionarLancamento(decimal valor, TipoLancamento tipoLancamento)
        {
            var lancamento = new Lancamento(valor, tipoLancamento);
            _lancamentos.Add(lancamento);
        }
    }
}