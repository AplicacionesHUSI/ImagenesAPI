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
    public class MedicoController : ControllerBase
    {
        private readonly IServiceDoctor  _service;
        public MedicoController(IServiceDoctor serviceEspecialidad) {
            _service = serviceEspecialidad;
        }

        /// <summary>
        /// WS de validación de médico en SAHI 
        /// </summary>
        /// <param name="usuarioRequest"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("validar")]
        public async Task<ActionResult<ResponseBase<MedicoResponse>>> AuthActiveUser(UsuarioRequest usuarioRequest)
        {
            var response = new ResponseBase<MedicoResponse>();
            try
            {
                var doctor = await _service.GetDoctor(usuarioRequest);
                if (doctor != null)
                {
                    var medico = new MedicoResponse()
                    {
                        IdMedico = doctor.IdPersonal
                       ,IdEspecialdiad=doctor.IdEspecialidad,
                        TipoMedico=doctor.TipoMedico,
                        ListaEspecialiad=doctor.ListaEspecialiad
                    };

                    response.Data = medico;
                    response.code = (int)HttpStatusCode.OK;
                    response.hasError = false;
                    response.message = $" ok ";
                }
                else {
                    response.Data =null;
                    response.code = (int)HttpStatusCode.NotFound;
                    response.hasError = false;
                    response.message = $" Medico no activo ";
                }
            }
            catch (Exception e)
            {
                response.code = (int)HttpStatusCode.InternalServerError;
                response.hasError = true;
                response.message = $"Se ha presentado una excepcion :: {e.Message}";
            }
            response.date = DateTime.Now;
            return StatusCode((int)HttpStatusCode.OK, response);
        }
        /// <summary>
        /// WS devuelve las especialidades
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("especialidades")]
        public async Task<ActionResult<ResponseBase<List<Especialidad>>>> GetEspecialidades()
        {
            var response = new ResponseBase<List<Especialidad>>();
            try
            {
                response.Data = await _service.GetList();
                response.code = (int)HttpStatusCode.OK;
                response.hasError = false ;
                response.message = $" ok ";
            } catch (Exception e) {
                response.code = (int)HttpStatusCode.InternalServerError;
                response.hasError = true;
                response.message = $"Se ha presentado una excepcion :: {e.Message}";
            }
            response.date = DateTime.Now;
            return StatusCode((int)HttpStatusCode.OK, response); 
        }

        /// <summary>
        /// WS de buscar médico en SAHI 
        /// </summary>
        /// <param name="usuarioRequest"></param>
        /// <returns></returns>
        [HttpPost]        
        public async Task<ActionResult<List<Doctor>>> GetMedicos(DoctorRequest usuarioRequest)
        {
            var response = new ResponseBase<List<Doctor>>();
            try
            {
                response.Data = await _service.GetListDoctor(usuarioRequest);
                response.code = (int)HttpStatusCode.OK;
                response.hasError = false;
                response.message = $" ok ";
            }
            catch (Exception e)
            {
                response.code = (int)HttpStatusCode.InternalServerError;
                response.hasError = true;
                response.message = $"Se ha presentado una excepcion :: {e.Message}";
            }
            response.date = DateTime.Now;
            return StatusCode((int)HttpStatusCode.OK, response);
        }
    }
}