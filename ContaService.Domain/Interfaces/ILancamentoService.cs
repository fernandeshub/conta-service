using System.Threading.Tasks;

namespace ContaService.Domain.Interfaces
{
    public interface ILancamentoService
    {
        Task Depositar(string contaOrigem, string contaDestino, decimal valor);
    }
}