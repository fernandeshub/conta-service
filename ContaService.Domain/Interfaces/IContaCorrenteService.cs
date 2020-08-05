using System.Threading.Tasks;

namespace ContaService.Domain.Interfaces
{
    public interface IContaCorrenteService
    {
        Task Transferir(string contaOrigem, string contaDestino, decimal valor);

        Task Creditar(string numero, decimal valor);

        Task Debitar(string numero, decimal valor);
        
    }
}