using System;
using System.Threading.Tasks;
using ContaService.Domain.AggregatesModel.ContaCorrenteAggregate;
using ContaService.Domain.SeedWork;
using Microsoft.EntityFrameworkCore;

namespace ContaService.Infrastructure.Repositories
{
    public class ContaCorrenteRepository : IContaCorrenteRepository
    {
        private readonly ContaServiceContext _context;

        public IUnitOfWork UnitOfWork
        {
            get
            {
                return _context;
            }
        }

        public ContaCorrenteRepository(ContaServiceContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public ContaCorrente Add(ContaCorrente conta)
        {
           return _context.ContasCorrentes.Add(conta).Entity;
        }

        public async Task<ContaCorrente> GetAsync(string contaNumero)
        {
            var conta = await _context.ContasCorrentes
            .FirstOrDefaultAsync(c => c.Numero.Equals(contaNumero));

            if (conta != null)
            {
                await _context.Entry(conta)
                .Collection(c => c.Lancamentos).LoadAsync();
            }

            return conta;
        }

        public void Update(ContaCorrente conta)
        {
             _context.Entry(conta).State = EntityState.Modified;
        }
    }
}