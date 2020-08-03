using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using ContaService.API.ViewModels;
using ContaService.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ContaService.API.Controllers
{
    [ApiController]
    public class LancamentoController : ControllerBase
    {
        private readonly ILancamentoService _service;

        public LancamentoController(ILancamentoService service)
        {
            _service = service;
        }

        [HttpPost]
        [Route("api/v1/lancamentos")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult Post([FromBody] Lancamento lancamento)
        {
            try
            {
                _service.Depositar(lancamento.ContaOrigem, 
                    lancamento.ContaDestino, lancamento.Valor);

                return Ok();
            }
            catch (Exception ex)
            {
               return BadRequest(ex.Message);
            }
        }

        
    }
}
