using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ContaService.API.Application.Commands;
using ContaService.API.Application.Queries;
using ContaService.API.Controllers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace ContaService.Teste.Application
{
    public class ContaCorrenteWebApiTeste
    {
        private readonly Mock<IMediator> _mediatorMock;
        private readonly Mock<IContaCorrenteQueries> _queriesMock;
        private readonly Mock<ILogger<ContaCorrenteController>> _loggerMock;

        public ContaCorrenteWebApiTeste()
        {
            _mediatorMock = new Mock<IMediator>();
            _queriesMock = new Mock<IContaCorrenteQueries>();
            _loggerMock = new Mock<ILogger<ContaCorrenteController>>();
        }

        [Fact]
        public async Task Create_conta_corrente_bad_request()
        {
           _mediatorMock.Setup(x => x.Send(It.IsAny<IdentifiedCommand<CreateContaCorrenteCommand, bool>>(), default(CancellationToken)))
                .Returns(Task.FromResult(true));

            var contaCorrenteController = new ContaCorrenteController(_mediatorMock.Object, _queriesMock.Object, _loggerMock.Object);
            var actionResult = await contaCorrenteController.CreateContaCorrenteAsync(new CreateContaCorrenteCommand("12345-6")) as BadRequestResult;

            Assert.Equal(actionResult.StatusCode, (int)System.Net.HttpStatusCode.BadRequest);
        }

         [Fact]
        public async Task Transferir_valor_bad_request()
        {
           _mediatorMock.Setup(x => x.Send(It.IsAny<IdentifiedCommand<TransferirValorCommand, bool>>(), default(CancellationToken)))
                .Returns(Task.FromResult(true));

            var contaCorrenteController = new ContaCorrenteController(_mediatorMock.Object, _queriesMock.Object, _loggerMock.Object);
            var actionResult = await contaCorrenteController.TransferirValorAsync(new TransferirValorCommand("12345-6", "65432-1", 1000)) as BadRequestResult;

            Assert.Equal(actionResult.StatusCode, (int)System.Net.HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Get_lancamentos_success()
        {
            var fakeDynamicResult = Enumerable.Empty<ContaLancamento>();

            _queriesMock.Setup(x => x.GetLancamentosAsync("12345-6"))
                .Returns(Task.FromResult(fakeDynamicResult));

            var contaCorrenteController = new ContaCorrenteController(_mediatorMock.Object, _queriesMock.Object, _loggerMock.Object);
            var actionResult = await contaCorrenteController.GetLancamentosAsync("12345-6");

            Assert.Equal((actionResult as OkObjectResult).StatusCode, (int)System.Net.HttpStatusCode.OK);
        }

        [Fact]
        public async Task Get_saldo_success()
        {
            var fakeDynamicResult = new ContaSaldo();

            _queriesMock.Setup(x => x.GetSaldoAsync("12345-6"))
                .Returns(Task.FromResult(fakeDynamicResult));

            var contaCorrenteController = new ContaCorrenteController(_mediatorMock.Object, _queriesMock.Object, _loggerMock.Object);
            var actionResult = await contaCorrenteController.GetSaldoAsync("12345-6");

            Assert.Equal((actionResult as OkObjectResult).StatusCode, (int)System.Net.HttpStatusCode.OK);
        }
        
    }
}