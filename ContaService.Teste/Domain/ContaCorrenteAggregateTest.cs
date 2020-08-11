using System;
using ContaService.Domain.AggregatesModel.ContaCorrenteAggregate;
using ContaService.Domain.SeedWork;
using Xunit;

namespace ContaService.Teste.Domain
{
    public class ContaCorrenteAggregateTest
    {
        public ContaCorrenteAggregateTest()
        {

        }

        [Fact]
        public void Create_conta_corrente_success()
        {
            var fakeConta = new ContaCorrente("12345-6");

            Assert.NotNull(fakeConta);
        }

        [Fact]
        public void Invalid_valor_lancamento()
        {
            Assert.Throws<InvalidOperationException>(() => new Lancamento(0, TipoLancamento.Credito));
        }
    }
}