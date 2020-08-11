using System;
using System.Threading;
using System.Threading.Tasks;
using ContaService.Domain.AggregatesModel.ContaCorrenteAggregate;
using ContaService.Infrastructure.Idempotency;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ContaService.API.Application.Commands
{
    public class TransferirValorCommandHandler
        : IRequestHandler<TransferirValorCommand, bool>
    {
        private readonly IContaCorrenteRepository _repository;
        private readonly IMediator _mediator;
        private readonly ILogger<CreateContaCorrenteCommandHandler> _logger;

        public TransferirValorCommandHandler(IContaCorrenteRepository respository,
        IMediator mediator, ILogger<CreateContaCorrenteCommandHandler> logger)
        {
            _repository = respository ?? throw new ArgumentNullException(nameof(respository));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<bool> Handle(TransferirValorCommand request, CancellationToken cancellationToken)
        {
            var origem =  await _repository.GetAsync(request.ContaOrigem);
            var destino = await _repository.GetAsync(request.ContaDestino);

            origem.Debitar(request.Valor);
            destino.Creditar(request.Valor);

            return await _repository.UnitOfWork
                .SaveEntitiesAsync(cancellationToken);
        }
    }

    public class CreateTransferenciaIdentifiedCommandHandler : IdentifiedCommandHandler<TransferirValorCommand, bool>
    {
        public CreateTransferenciaIdentifiedCommandHandler(
            IMediator mediator,
            IRequestManager requestManager,
            ILogger<IdentifiedCommandHandler<TransferirValorCommand, bool>> logger)
            : base(mediator, requestManager, logger)
        {
        }

        protected override bool CreateResultForDuplicateRequest()
        {
            return true;    
        }
    }
}