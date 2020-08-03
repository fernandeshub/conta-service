using System;

namespace ContaService.API.ViewModels
{
    public class LancamentoDetalhe
    {
        public DateTime Data { get; set; }
        public decimal Valor { get; set; }
        public string Operacao { get; set; }
    }
}