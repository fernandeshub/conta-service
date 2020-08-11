using System;

namespace  ContaService.API.Application.Queries
{
    public class ContaLancamento
    {
        public decimal Valor { get; set; }
        public DateTime Data { get; set; }
    }

    public class ContaSaldo
    {
        public decimal Saldo { get; set; }
    }
}