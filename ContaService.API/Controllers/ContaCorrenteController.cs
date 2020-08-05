using System;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using ContaService.API.Models;
using ContaService.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ContaService.API.Controllers
{
    [ApiController]
    public class ContaCorrenteController : ControllerBase
    {
        private readonly ILogger<ContaCorrenteController> _logger;
        private readonly IContaCorrenteService _service;

        public ContaCorrenteController(ILogger<ContaCorrenteController> logger,
         IContaCorrenteService service)
        {
            _logger = logger;
            _service = service;
        }

    
        [HttpPost]
        [Authorize("Bearer")]
        [Route("api/v1/conta-corrente/transferir")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Transferir([FromBody] Transferencia transferencia)
        {
            try
            {
                await _service.Transferir(transferencia.ContaOrigem,
                            transferencia.ContaDestino, transferencia.Valor);

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                return BadRequest(ex.Message);
            }
        }


    }
}
