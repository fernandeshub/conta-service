using System;
using ContaService.Domain.SeedWork;

namespace ContaService.Domain.Models
{
    public class Lancamento 
    {
        public int Id { get; set; }
        public DateTime Data { get; private set; }
        public decimal Valor { get; private set; }      
        public ContaCorrente Conta{ get; private set; }
        public TipoOperacao TipoOperacao { get; private set; }
        
        public Lancamento(decimal valor, DateTime data, ContaCorrente conta, TipoOperacao tipoOperacao)
        {
            this.Valor = valor;
            this.Data = data;
            this.Conta = conta;
            this.TipoOperacao = tipoOperacao;
        }
    }
}