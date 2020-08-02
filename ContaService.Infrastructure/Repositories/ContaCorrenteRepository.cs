using System;
using System.Threading.Tasks;
using ContaService.Domain.Interfaces;
using ContaService.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace ContaService.Infrastructure.Repositories
{
    public class ContaCorrenteRepository : IContaCorrenteRepository
    {
       private readonly ContaServiceContext _context;

         public ContaCorrenteRepository(ContaServiceContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<ContaCorrente> GetByNumeroAsync(string numero)
        {
            if (string.IsNullOrEmpty(numero))
            {
                throw new ArgumentNullException(nameof(numero));
            }

           return await _context.ContasCorrentes.FirstOrDefaultAsync(x => x.Numero.Equals(numero));
        }

        public async Task UpdateAsync(ContaCorrente contaCorrente)
        {
             _context.Entry(contaCorrente).State = EntityState.Modified;
             await _context.SaveChangesAsync();
        }
    }
}