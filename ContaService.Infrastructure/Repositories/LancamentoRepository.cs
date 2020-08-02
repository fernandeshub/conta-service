using System;
using System.Threading.Tasks;
using ContaService.Domain.Interfaces;
using ContaService.Domain.Models;

namespace ContaService.Infrastructure.Repositories
{
    public class LancamentoRepository : ILancamentoRepository
    {
       private readonly ContaServiceContext _context;

         public LancamentoRepository(ContaServiceContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task AddAsync(Lancamento lancamento)
        {
           await _context.Lancamentos.AddAsync(lancamento);
           await _context.SaveChangesAsync();
        }
    }
}