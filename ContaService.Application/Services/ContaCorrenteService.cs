using System;
using System.Linq;
using System.Threading.Tasks;
using ContaService.Domain.Interfaces;
using ContaService.Domain.Models;
using ContaService.Domain.SeedWork;
using ContaService.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace ContaService.Application.Services
{
    public class ContaCorrenteService : IContaCorrenteService
    {
        private readonly ContaServiceContext _context;

        public ContaCorrenteService(ContaServiceContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public async Task Transferir(string contaOrigem, string contaDestino, decimal valor)
        {
            await Debitar(contaOrigem, valor);
            await Creditar(contaDestino, valor);
        }

        public async Task Creditar(string numero, decimal valor)
        {
            await MovimentarConta(numero, valor, TipoOperacao.Credito);
        }

        public async Task Debitar(string numero, decimal valor)
        {
            await MovimentarConta(numero, valor, TipoOperacao.Debito);
        }

        private async Task MovimentarConta(string numero, decimal valor, TipoOperacao tipoOperacao)
        {
            var conta = await GetContaCorrenteByNumero(numero);
            var saldo = await GetSaldo(conta.Id);

            if (saldo != conta.Saldo)
            {
                throw new ArgumentOutOfRangeException($"A conta número {numero} está com o saldo inconsitente");
            }

            if (tipoOperacao == TipoOperacao.Debito)
            {
                if (conta.Saldo < valor)
                {
                    throw new ArgumentOutOfRangeException("Saldo insuficiente");
                }

                conta.Debitar(valor);
                valor = Decimal.Negate(valor);
            }
            else
            {
                conta.Creditar(valor);
            }

            var lancamento = new Lancamento(conta.Id, valor, conta.Saldo, DateTime.UtcNow, tipoOperacao);

            await _context.Lancamentos.AddAsync(lancamento);

            await _context.SaveChangesAsync();
        }

        private async Task<ContaCorrente> GetContaCorrenteByNumero(string numero)
        {
            return await _context.ContasCorrentes.FirstOrDefaultAsync(c => c.Numero.Equals(numero));
        }

        private async Task<decimal> GetSaldo(int contaId)
        {
            return await _context.Lancamentos.Where(c => c.ContaId == contaId).SumAsync(x => x.Valor);
        }
    }
}