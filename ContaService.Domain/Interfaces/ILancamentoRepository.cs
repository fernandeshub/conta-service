using System.Threading.Tasks;
using ContaService.Domain.Models;

namespace ContaService.Domain.Interfaces
{
    public interface ILancamentoRepository 
    {
        Task AddAsync(Lancamento lancamento);
    }
}