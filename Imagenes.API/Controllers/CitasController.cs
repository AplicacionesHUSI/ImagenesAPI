using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data;
using System;
using Microsoft.Extensions.Logging;
using Imagenes.API.Domain.Request;
using Imagenes.API.Domain.Response;
using Azure;
using System.Net;

namespace Imagenes.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CitasController : ControllerBase
    {
        private readonly ILogger<CitasController> logger;

        /// <summary>
        /// Constructor para el controller de consultar número de atención.
        /// </summary>
        /// <param name="logger"></param>
        public CitasController(ILogger<CitasController> logger)
        {
            this.logger = logger;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="infoCitaRequest"></param>
        /// <returns></returns>
        [HttpPost(Name = "ConsultarCita")]
        public IActionResult PostConsultarCita(CitasRequest citasRequest)
        {
            IConfiguration configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            ListaCita response = new ListaCita();
            List<CitaResponse> listCita = new List<CitaResponse>();

            using (SqlConnection con = new SqlConnection(configuration.GetConnectionString("conexion2")))
            {
                using (SqlCommand cmd = new SqlCommand("ConsultarCitaPaciente", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@idTipoID", citasRequest.idTipoDoc);
                    cmd.Parameters.AddWithValue("@numDoc", citasRequest.numDoc);
                    cmd.Parameters.AddWithValue("@idCita", citasRequest.idCita);
                    try
                    {
                        con.Open();
                        SqlDataReader reader = cmd.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                CitaResponse citaResponse = new CitaResponse();
                                citaResponse.IdEspecialidad = reader.IsDBNull(0) ? null : reader.GetString(0);
                                citaResponse.NombreEspecialista = reader.IsDBNull(1) ? null : reader.GetString(1);
                                citaResponse.EmailEspecialista = reader.IsDBNull(2) ? null : reader.GetString(2);
                                citaResponse.FechaCita = reader.GetDateTime(3);
                                citaResponse.HoraCita =  reader.GetDateTime(4);
                                citaResponse.IdPaciente = reader.IsDBNull(5) ? 0 : reader.GetInt64(5);
                                citaResponse.IdAtención = reader.IsDBNull(6) ? 0 : reader.GetInt64(6);
                                citaResponse.IdEspecialista = reader.IsDBNull(7) ? null : reader.GetString(7);
                                citaResponse.DuracionCita = reader.IsDBNull(8) ? null : reader.GetString(8);
                                listCita.Add(citaResponse);
                            }

                            response.Codigo = (int)HttpStatusCode.OK;
                            response.Mensaje = "Transacción exitosa";
                            response.citasResponses = listCita;
                            return Ok(response);
                        }
                        else
                        {
                            response.Codigo = (int)HttpStatusCode.NoContent;
                            response.Mensaje = "No hay información";
                            response.citasResponses = listCita;
                            return NotFound(response);
                        }
                    }
                    catch (Exception ex)
                    {
                        response.Codigo = (int)HttpStatusCode.InternalServerError;
                        response.Mensaje = $"Exception:: {ex.Message}";
                        response.citasResponses = listCita;
                        return NotFound(response);
                    }
                }
            }
        }
    }
}

