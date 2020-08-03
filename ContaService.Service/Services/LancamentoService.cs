using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ContaService.Domain.Interfaces;
using ContaService.Domain.Models;
using ContaService.Domain.SeedWork;
using ContaService.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace ContaService.Service.Services
{
    public class LancamentoService : ILancamentoService
    {
        private readonly ContaServiceContext _context;

        public LancamentoService(ContaServiceContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public async Task Depositar(string contaOrigem, string contaDestino, decimal valor)
        {
            try
            {
                var origem = await _context.ContasCorrentes.FirstOrDefaultAsync(c =>
                    c.Numero.Equals(contaOrigem));

                if (origem == null)
                {
                    throw new NullReferenceException("Conta de Origem não encontrada");
                }

                var destino = await _context.ContasCorrentes.FirstOrDefaultAsync(c =>
                    c.Numero.Equals(contaDestino));

                if (destino == null)
                {
                    throw new NullReferenceException("Conta de destino não encontrada.");
                }

                if (origem.Saldo < valor)
                {
                    throw new ArgumentOutOfRangeException("Saldo insuficiente");
                }

                origem.Debitar(valor);
                destino.Creditar(valor);

                var lancamentoOrigem = new Lancamento(Decimal.Negate(valor), DateTime.UtcNow,
                    origem, TipoOperacao.Debito);

                var lancamentoDestino = new Lancamento(valor, DateTime.UtcNow, destino,
                    TipoOperacao.Credito);

                await _context.Lancamentos.AddAsync(lancamentoOrigem);
                await _context.Lancamentos.AddAsync(lancamentoDestino);

                _context.Entry(contaOrigem).State = EntityState.Modified;
                _context.Entry(contaDestino).State = EntityState.Modified;

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<Lancamento>> ListFiltered(string conta, TipoOperacao? tipoOperacao = null,
         DateTime? start = null, DateTime? end = null, int offset = 0,  int maxRow = 30)
        {
           var query = _context.Lancamentos.Where(l => l.Conta.Numero.Equals(conta));

          var teste = _context.Lancamentos.ToList();

            var res = query.ToList();
           if (tipoOperacao != null)
           {
               query = query.Where(l => l.TipoOperacao == tipoOperacao);
           }

            if (start > end)
            {
                throw new ArgumentOutOfRangeException("Invalid date range");
            }

           if (start.HasValue)
           {
               query = query.Where(l => l.Data.Date >= start.Value.Date);
           }

           if (end.HasValue)
           {
                query = query.Where(l => l.Data.Date <= end.Value.Date.AddDays(1));
           }

           if (offset > 0)
           {
               query = query.Skip(offset);
           }

           if (maxRow != 30)
           {
               query.Take(maxRow);
           }

           return await query.ToListAsync();
        }
    }
}