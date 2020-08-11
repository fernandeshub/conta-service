using System.Threading.Tasks;
using ContaService.Domain.SeedWork;

namespace ContaService.Domain.AggregatesModel.ContaCorrenteAggregate
{
    public interface IContaCorrenteRepository : IRepository<ContaCorrente>
    {
        ContaCorrente Add(ContaCorrente conta);
        void Update(ContaCorrente conta);
        Task<ContaCorrente> GetAsync(string contaNumero);
    }
}