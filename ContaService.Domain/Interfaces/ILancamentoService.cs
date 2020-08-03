using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ContaService.Domain.Models;
using ContaService.Domain.SeedWork;

namespace ContaService.Domain.Interfaces
{
    public interface ILancamentoService
    {
        Task Depositar(string contaOrigem, string contaDestino, decimal valor);
        Task<IEnumerable<Lancamento>> ListFiltered(string conta, TipoOperacao? tipoOperacao = null,
         DateTime? start = null, DateTime? end = null, int offset = 0,  int maxRow = 30);
    }
}