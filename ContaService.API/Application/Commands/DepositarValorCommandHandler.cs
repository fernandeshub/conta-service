using System;
using System.Threading;
using System.Threading.Tasks;
using ContaService.Domain.AggregatesModel.ContaCorrenteAggregate;
using ContaService.Infrastructure.Idempotency;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ContaService.API.Application.Commands
{
    public class DepositarValorCommandHandler
        : IRequestHandler<DepositarValorCommand, bool>
    {
        private readonly IContaCorrenteRepository _repository;
        private readonly IMediator _mediator;
        private readonly ILogger<DepositarValorCommandHandler> _logger;

        public DepositarValorCommandHandler(IContaCorrenteRepository respository,
        IMediator mediator, ILogger<DepositarValorCommandHandler> logger)
        {
            _repository = respository ?? throw new ArgumentNullException(nameof(respository));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<bool> Handle(DepositarValorCommand request, CancellationToken cancellationToken)
        {
            var conta = await _repository.GetAsync(request.ContaNumero);
            conta.Creditar(request.Valor);
           
            return await _repository.UnitOfWork
                .SaveEntitiesAsync(cancellationToken);
        }
    }

    public class DepositarValorIdentifiedCommandHandler : IdentifiedCommandHandler<DepositarValorCommand, bool>
    {
        public DepositarValorIdentifiedCommandHandler(
            IMediator mediator,
            IRequestManager requestManager,
            ILogger<IdentifiedCommandHandler<DepositarValorCommand, bool>> logger)
            : base(mediator, requestManager, logger)
        {
        }

        protected override bool CreateResultForDuplicateRequest()
        {
            return true;    
        }
    }
}