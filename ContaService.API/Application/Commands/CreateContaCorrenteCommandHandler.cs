using System;
using System.Threading;
using System.Threading.Tasks;
using ContaService.Domain.AggregatesModel.ContaCorrenteAggregate;
using ContaService.Infrastructure.Idempotency;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ContaService.API.Application.Commands
{
    public class CreateContaCorrenteCommandHandler
        : IRequestHandler<CreateContaCorrenteCommand, bool>
    {
        private readonly IContaCorrenteRepository _repository;
        private readonly IMediator _mediator;
        private readonly ILogger<CreateContaCorrenteCommandHandler> _logger;

        public CreateContaCorrenteCommandHandler(IContaCorrenteRepository respository,
        IMediator mediator, ILogger<CreateContaCorrenteCommandHandler> logger)
        {
            _repository = respository ?? throw new ArgumentNullException(nameof(respository));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<bool> Handle(CreateContaCorrenteCommand request, CancellationToken cancellationToken)
        {
            var conta = new ContaCorrente(request.Numero);
            _logger.LogInformation("----- Creating Conta Corrente - Conta: {@Conta}", conta);
            _repository.Add(conta);

            return await _repository.UnitOfWork
                .SaveEntitiesAsync(cancellationToken);
        }
    }

    public class CreateContaCorrenteIdentifiedCommandHandler : IdentifiedCommandHandler<CreateContaCorrenteCommand, bool>
    {
        public CreateContaCorrenteIdentifiedCommandHandler(
            IMediator mediator,
            IRequestManager requestManager,
            ILogger<IdentifiedCommandHandler<CreateContaCorrenteCommand, bool>> logger)
            : base(mediator, requestManager, logger)
        {
        }

        protected override bool CreateResultForDuplicateRequest()
        {
            return true;    
        }
    }
}