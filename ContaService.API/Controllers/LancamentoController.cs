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
    [Route("api/[controller]")]
    [ApiController]
    public class LancamentoController : ControllerBase
    {
        private readonly ILancamentoService _service;
       
        public LancamentoController(ILancamentoService service)
        {
           _service = service;
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public  IActionResult Post([FromBody] Lancamento lancamento)
        {
            

            return Ok();
        }

      
    }
}
