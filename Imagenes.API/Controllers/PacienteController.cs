using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Imagenes.API.Domain.DbSet;
using Imagenes.API.Domain.IService;
using Imagenes.API.Domain.Request;
using Imagenes.API.Domain.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Imagenes.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class PacienteController : ControllerBase
    {
        private readonly IServicePaciente _service;
        public PacienteController(IServicePaciente service)
        {
            _service = service;
        }

        /// <summary>
        /// WS de Buscar paciente en SAHI  
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]

        public async Task<ActionResult<ResponseBase<Cliente>>> GetPaciente(PacienteRequest request)
        {
            var response = new ResponseBase<Cliente>();

            try {
                var cliente = await _service.GetCliente(request);
                if (cliente != null) {
                    response.code = (int)HttpStatusCode.OK;
                    response.hasError = false;
                    response.Data = cliente;
                }
                else {
                    response.code = (int)HttpStatusCode.NotFound;
                    response.hasError = false;
                }
            }
            catch (Exception e) {
                response.code = (int)HttpStatusCode.InternalServerError;
                response.message = $"Se ha presentado un aexcepcion :: {e.Message}";
                response.hasError = true;
            }
            response.date = DateTime.Now;
            return StatusCode((int)HttpStatusCode.OK, response); ;
        }
    }
}