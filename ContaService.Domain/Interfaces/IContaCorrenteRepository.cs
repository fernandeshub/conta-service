using System.Threading.Tasks;
using ContaService.Domain.Models;

namespace ContaService.Domain.Interfaces
{
    public interface IContaCorrenteRepository
    {        
        Task<ContaCorrente> GetByNumeroAsync(string numero);
        Task UpdateAsync(ContaCorrente contaCorrente);

    }
}