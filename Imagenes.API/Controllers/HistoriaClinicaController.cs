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
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Imagenes.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class HistoriaClinicaController : ControllerBase
    {
        private readonly IServiceHistoriaClinica _service;
        private readonly ILogger<HistoriaClinicaController> _logger;
        private readonly string _pathFileServer;
        public HistoriaClinicaController(IServiceHistoriaClinica service, ILogger<HistoriaClinicaController> logger, IConfiguration configuration) {
            _logger = logger;
            _service = service;
            _pathFileServer = configuration["pathFileServer"];
        }

        /// <summary>
        /// WS de Registrar en la historia clínica del paciente
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<ResponseBase<string>>> GetPaciente(HistoriaClinicaRequest request)
        {
            var json = JsonConvert.SerializeObject(request);
            _logger.LogInformation("request from APP Imagenes :: " + json);
            var response = new ResponseBase<int>();
            try {
                var result = await _service.SaveElement(request);
                if (result>0) {
                    response.Data = result;
                    response.code = (int)HttpStatusCode.OK;
                    response.hasError = false;
                }
                else {
                    response.code = (int)HttpStatusCode.BadRequest;
                    response.Data =0;
                    response.hasError = true;
                }
            } catch (Exception e) {
                response.code = (int)HttpStatusCode.InternalServerError;
                response.message = $"Se ha presentado una excepcion :: "+e.Message;
                response.Data = 0;
                response.hasError = true;
            }
            response.date = DateTime.Now;
             json = JsonConvert.SerializeObject(response);
            _logger.LogInformation("response :: " + json);
            return StatusCode((int)HttpStatusCode.OK, response); ;
        }


        [HttpGet]
        [Route("element/{idElemento}")]
        public async Task<IActionResult> GetElemento(int idElemento)
        {

            var elemento = await _service.FindElemento(idElemento);
            var path=_pathFileServer + elemento.Element;
            var image = System.IO.File.OpenRead(path);
            var tipo = elemento.Tipo == "imagen" ? "image/jpg" : "video/mp4";
            return File(image, tipo);
        }

        /// <summary>
        /// Registrar en la historia clínica del paciente foto, video, imagen o pdf.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("PostRegistrarInfoHC")]
        public async Task<ActionResult<ResponseBase<string>>> PostRegistrarInfoHC(HistoriaClinicaRequest request)
        {
            var json = JsonConvert.SerializeObject(request);
            _logger.LogInformation("Request from APP Imagenes PostRegistrarInfoHC :: " + json);
            var response = new ResponseBase<int>();
            try
            {
                var result = await _service.SaveElements(request);
                if (result > 0)
                {
                    response.Data = result;
                    response.code = (int)HttpStatusCode.OK;
                    response.hasError = false;
                }
                else
                {
                    response.code = (int)HttpStatusCode.BadRequest;
                    response.Data = 0;
                    response.hasError = true;
                }
            }
            catch (Exception e)
            {
                response.code = (int)HttpStatusCode.InternalServerError;
                response.message = $"Se ha presentado una excepcion :: " + e.Message;
                response.Data = 0;
                response.hasError = true;
            }
            response.date = DateTime.Now;
            json = JsonConvert.SerializeObject(response);
            _logger.LogInformation("response :: " + json);
            return StatusCode((int)HttpStatusCode.OK, response); ;
        }
    }
}