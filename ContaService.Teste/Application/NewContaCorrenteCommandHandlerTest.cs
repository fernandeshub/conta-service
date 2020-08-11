using Moq;
using ContaService.Domain.AggregatesModel.ContaCorrenteAggregate;
using MediatR;
using Xunit;
using System.Threading;
using System.Threading.Tasks;
using ContaService.API.Application.Commands;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace ContaService.Teste.Application
{
   
    public class NewContaCorrenteCommandHandlerTest
    {
        private readonly Mock<IMediator> _mediator;
        private readonly Mock<IContaCorrenteRepository> _repositoryMock;

        public NewContaCorrenteCommandHandlerTest()
        {
            _mediator = new Mock<IMediator>();
            _repositoryMock = new Mock<IContaCorrenteRepository>();
        }
         
        [Fact]
        public async Task Handle_return_false_if_conta_corrente_is_not_persisted()
        {
            var fakeContaCmd = FakeContaCorrenteRequest(new Dictionary<string, object>
            { ["numero"] = "12345-6" });

             _repositoryMock.Setup(rep => rep.GetAsync(It.IsAny<string>()))
               .Returns(Task.FromResult<ContaCorrente>(FakeConta()));
            
            _repositoryMock.Setup(rep => rep.UnitOfWork.SaveChangesAsync(default(CancellationToken)))
            .Returns(Task.FromResult(1));

            var LoggerMock = new Mock<ILogger<CreateContaCorrenteCommandHandler>>();

            var handler = new CreateContaCorrenteCommandHandler(_repositoryMock.Object, _mediator.Object, LoggerMock.Object);
            var cltToken = new System.Threading.CancellationToken();
            var result = await handler.Handle(fakeContaCmd, cltToken);
            
            Assert.False(result);
        }

        private ContaCorrente FakeConta()
        {
            return new ContaCorrente("12345-6");
        }

         private CreateContaCorrenteCommand FakeContaCorrenteRequest(Dictionary<string, object> args = null)
        {
            return new CreateContaCorrenteCommand(
                numero: args != null && args.ContainsKey("numero") ? (string)args["numero"] : null);
              
        }
    }
}
