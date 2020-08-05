using System;
using ContaService.Domain.SeedWork;

namespace ContaService.Domain.Models
{
    public class Lancamento
    {
        public int Id { get; private set; }
        public DateTime Data { get; private set; }
        public decimal Valor { get; private set; }
        public decimal SaldoAtual { get; set; }
        public int ContaId { get; private set; }
        public TipoOperacao TipoOperacao { get; private set; }

        public Lancamento(int id, int contaId, decimal valor, decimal saldoAtual, DateTime data, TipoOperacao tipoOperacao)
        {
            this.Id = id;
            this.Valor = valor;
            this.SaldoAtual = saldoAtual;
            this.Data = data;
            this.ContaId = contaId;
            this.TipoOperacao = tipoOperacao;
        }

        public Lancamento(int contaId, decimal valor, decimal saldoAtual, DateTime data,  TipoOperacao tipoOperacao)
        {
            this.ContaId = contaId;
            this.Valor = valor;
            this.SaldoAtual = saldoAtual;
            this.Data = data;
            this.TipoOperacao = tipoOperacao;
        }
    }
}