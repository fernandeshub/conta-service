using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using ContaService.API.Application.Behaviors;
using ContaService.API.Application.Commands;
using ContaService.API.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ContaService.API.Controllers
{
    [ApiController]
    [Authorize]
    public class ContaCorrenteController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IContaCorrenteQueries _queries;
        private readonly ILogger<ContaCorrenteController> _logger;

        public ContaCorrenteController(IMediator mediator,IContaCorrenteQueries queries,
         ILogger<ContaCorrenteController> logger)
        {
            _mediator = mediator;
            _queries = queries;
            _logger = logger;
        }

        [HttpGet]
        [Route("api/v1/conta-corrente/{numero}/lancamentos")]
        [ProducesResponseType(typeof(IEnumerable<ContaLancamento>),(int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetLancamentosAsync(string numero)
        {
            var lancamentos = await _queries.GetLancamentosAsync(numero);

            return Ok(lancamentos);
        }

        [HttpGet]
        [Route("api/v1/conta-corrente/{numero}/saldo")]
        [ProducesResponseType(typeof(IEnumerable<ContaLancamento>),(int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetSaldoAsync(string numero)
        {
            var saldo = await _queries.GetSaldoAsync(numero);

            return Ok(saldo);
        }

        [HttpPost]
        [Route("api/v1/conta-corrente")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]

        public async Task<IActionResult> CreateContaCorrenteAsync([FromBody] CreateContaCorrenteCommand createContaCorrenteCommand)
        {
            bool commandResult = false;

            _logger.LogInformation(
             "----- Sending command: {CommandName} - {IdProperty}: {CommandId} ({@Command})",
             createContaCorrenteCommand.GetGenericTypeName(),
             nameof(createContaCorrenteCommand.Numero),
             createContaCorrenteCommand.Numero,
             createContaCorrenteCommand);

            commandResult = await _mediator.Send(createContaCorrenteCommand);

            if (!commandResult)
            {
                return BadRequest();
            }

            return Ok();
        }

         [HttpPost]
        [Route("api/v1/conta-corrente/depositar")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> DepositarValorAsync([FromBody] DepositarValorCommand depositarValorCommand)
        {
            bool commandResult = false;

             _logger.LogInformation(
             "----- Sending command: {CommandName} - {IdProperty}: {CommandId} ({@Command})",
             depositarValorCommand.GetGenericTypeName(),
             nameof(depositarValorCommand.ContaNumero),
             depositarValorCommand.ContaNumero,
             depositarValorCommand);

            commandResult = await _mediator.Send(depositarValorCommand);

            if (!commandResult)
            {
                return BadRequest();
            }

            return Ok();
        }


        [HttpPost]
        [Route("api/v1/conta-corrente/transferir")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> TransferirValorAsync([FromBody] TransferirValorCommand createTransferenciaCommand)
        {
            bool commandResult = false;

            _logger.LogInformation(
            "----- Sending command: {CommandName} - ({IdProperty} : {CommandId}, {IdProperty}: {CommandId}) ({@Command})",
            createTransferenciaCommand.GetGenericTypeName(),
            nameof(createTransferenciaCommand.ContaOrigem),
            createTransferenciaCommand.ContaOrigem,
            nameof(createTransferenciaCommand.ContaDestino),
            createTransferenciaCommand.ContaDestino,
            createTransferenciaCommand);

            commandResult = await _mediator.Send(createTransferenciaCommand);

            if (!commandResult)
            {
                return BadRequest();
            }

            return Ok();
        }

    }
}
