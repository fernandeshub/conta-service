using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using ContaService.API.ViewModels;
using ContaService.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ContaService.API.Controllers
{
    [ApiController]
    public class ContaCorrenteController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ILancamentoService _service;

        public ContaCorrenteController(IMapper mapper, ILancamentoService service)
        {
            _mapper = mapper;
            _service = service;
        }

        [HttpPost]
        [Route("api/v1/conta-corrente/{numero}/lancamentos")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Get(string numero)
        {
            try
            {
               var lancamentos =  await _service.ListFiltered(numero);
               var result = _mapper.Map<IEnumerable<LancamentoDetalhe>>(lancamentos);

                return Ok(result);
            }
            catch (Exception ex)
            {
               return BadRequest(ex.Message);
            }
        }

        
    }
}
