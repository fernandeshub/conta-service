using System.Collections.Generic;
using System.Threading.Tasks;

namespace  ContaService.API.Application.Queries
{
    public interface IContaCorrenteQueries
    {
        Task<ContaSaldo> GetSaldoAsync(string contaNumero);
        Task<IEnumerable<ContaLancamento>> GetLancamentosAsync(string contaNumero);
    }
}