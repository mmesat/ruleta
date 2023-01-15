using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RuletaAPi.DTOs;
using RuletaAPi.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RuletaAPi.Controllers
{
    [Route("/ruleta")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Logueado")]
    public class RuletaController : Controller
    {
        private readonly IRuletaService _service;

        public RuletaController(IRuletaService service)
        {
            _service = service;
        }

        [HttpPost("crear")]
        [AllowAnonymous]
        public async Task<ActionResult<Guid>> CreateRulet()
        {
            try
            {
                return Ok(await _service.CreateRulet());
            }
            catch
            {
                //Se puede implementar logs, traza detallada etc...
                return BadRequest();
            }
            
        }

        [HttpPut("apertura/{id:guid}")]
        [AllowAnonymous]
        public async Task<ActionResult<bool>> Apertura(Guid id)
        {
            try
            {
                return Ok(await _service.Apertura(id));
            }
            catch
            {
                //Se puede implementar logs, traza detallada etc...
                return NotFound();
            }
        }

        [HttpPost("apuesta")]
        //Descomentariar para usar, ya que valida token real
        //[AllowAnonymous]
        public async Task<ActionResult<bool>> Apuesta([FromBody] ApuestaDTO apuestaDTO)
        {
            try
            {
                return Ok(await _service.Apuesta(apuestaDTO));
            }
            catch
            {
                //Se puede implementar logs, traza detallada etc...
                return BadRequest();
            }
            
        }

        [AllowAnonymous]
        [HttpGet("cierre/{id:guid}")]
        public async Task<ActionResult<List<ApuestaDTO>>> Cierre(Guid id)
        {
            try
            {
                return Ok(await _service.Cierre(id));
            }
            catch
            {
                //Se puede implementar logs, traza detallada etc...
                return BadRequest();
            }
        }

        [HttpGet("ruletas")]
        [AllowAnonymous]
        public async Task<ActionResult<List<RuletaDTO>>> ObtenerRuletas()
        {
            try { 
            return Ok(await _service.ObtenerTodos());
            }
            catch
            {
                //Se puede implementar logs, traza detallada etc...
                return NotFound();
             }
        }

        [HttpGet("historico")]
        [AllowAnonymous]
        public async Task<ActionResult<List<ApuestaDTO>>> ObtenerHistoricoApuestas()
        {
            try
            {
                return Ok(await _service.ObtenerHistoricoApuestas());
            }
            catch
            {
                //Se puede implementar logs, traza detallada etc...
                return NotFound();
            }
        }
    }
}
