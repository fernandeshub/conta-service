using System;
using System.Threading.Tasks;
using ContaService.Domain.Interfaces;
using ContaService.Domain.Models;
using ContaService.Domain.SeedWork;

namespace ContaService.Service.Services
{
    public class LancamentoService : ILancamentoService
    {
        private readonly ILancamentoRepository _lancamentoRepository;
        private readonly IContaCorrenteRepository _contaCorrenteRepository;

        public LancamentoService(ILancamentoRepository lancamentoRepository, 
            IContaCorrenteRepository contaCorrenteRepository)
        {
            _lancamentoRepository = lancamentoRepository;
            _contaCorrenteRepository = contaCorrenteRepository;
        } 
        public async Task Depositar(string contaOrigem, string contaDestino, decimal valor)
        {
            var origem = await _contaCorrenteRepository.GetByNumeroAsync(contaOrigem);
            if (origem == null)
            {
                throw new NullReferenceException("Conta de Origem não encontrada");
            }

            var destino = await _contaCorrenteRepository.GetByNumeroAsync(contaDestino);
            if (destino == null)
            {
                throw new NullReferenceException("Conta de destino não encontrada.");
            }

            if (origem.Saldo < valor)
            {
                throw new ArgumentOutOfRangeException("Saldo insuficiente");
            }

            var lancamentoOrigem = new Lancamento(Decimal.Negate(valor), DateTime.UtcNow, origem, TipoOperacao.Debito);
            await _lancamentoRepository.AddAsync(lancamentoOrigem);

            var lancamentoDestino = new Lancamento(valor , DateTime.UtcNow, origem, TipoOperacao.Credito);
            await _lancamentoRepository.AddAsync(lancamentoDestino);

            origem.Debitar(valor);
            await _contaCorrenteRepository.UpdateAsync(origem);

            destino.Creditar(valor);
            await _contaCorrenteRepository.UpdateAsync(destino);
        }
    }
}