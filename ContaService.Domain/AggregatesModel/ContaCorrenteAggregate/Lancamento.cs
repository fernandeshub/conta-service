using System;
using ContaService.Domain.SeedWork;

namespace ContaService.Domain.AggregatesModel.ContaCorrenteAggregate
{
    public class Lancamento : Entity
    {
        private decimal _valor;
        private DateTime _dataLancamento;
        private TipoLancamento _tipoLancamento;

        public Lancamento() { }
       
        public Lancamento(decimal valor, TipoLancamento tipoLancamento)
        {
            _valor = valor != 0 ? valor : throw new InvalidOperationException();
            _dataLancamento = DateTime.UtcNow;
            _tipoLancamento = tipoLancamento;
        }
    }
}