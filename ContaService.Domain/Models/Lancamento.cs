using System;
using ContaService.Domain.SeedWork;

namespace ContaService.Domain.Models
{
    public class Lancamento
    {
        public int Id { get; protected set; }
        public DateTime Data { get; private set; }
        public decimal Valor { get; private set; }
        public int ContaId { get; private set; }
        public virtual ContaCorrente Conta { get; private set; }
        public TipoOperacao TipoOperacao { get; private set; }

        public Lancamento(int id, decimal valor, DateTime data, int contaId, TipoOperacao tipoOperacao)
        {
            this.Id = id;
            this.Valor = valor;
            this.Data = data;
            this.ContaId = contaId;
            this.TipoOperacao = tipoOperacao;
        }

        public Lancamento(decimal valor, DateTime data, ContaCorrente conta, TipoOperacao tipoOperacao)
        {
            this.Valor = valor;
            this.Data = data;
            this.Conta = conta;
            this.ContaId = conta.ContaId;
            this.TipoOperacao = tipoOperacao;
        }
    }
}