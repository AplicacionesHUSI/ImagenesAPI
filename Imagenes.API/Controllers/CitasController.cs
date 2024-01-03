using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data;
using System;
using Microsoft.Extensions.Logging;
using Imagenes.API.Domain.Request;
using Imagenes.API.Domain.Response;

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

            using (SqlConnection con = new SqlConnection(configuration.GetConnectionString("conexion2")))
            {
                using (SqlCommand cmd = new SqlCommand("ConsultarCitaPaciente", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@tipoDoc", citasRequest.idTipoDoc);
                    cmd.Parameters.AddWithValue("@numDoc", citasRequest.numDoc);
                    cmd.Parameters.AddWithValue("@idCita", citasRequest.idCita);
                    try
                    {
                        con.Open();

                        SqlDataReader reader = cmd.ExecuteReader();
                        var result = new Dictionary<string, object>();

                        ListaCita response = new ListaCita();
                        List<CitaResponse> listCita = new List<CitaResponse>();

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
                                //citaResponse.IdEspecialidad = reader.IsDBNull(0) ? null : reader.GetString(0));
                                //result.Add(citaResponse.NombreEspecialista, reader.IsDBNull(1) ? null : reader.GetString(1));
                                //result.Add(citaResponse.EmailEspecialista, reader.IsDBNull(2) ? null : reader.GetString(2));
                                //result.Add(citaResponse.FechaCita, reader.IsDBNull(3) ? null : reader.GetDateTime(3));
                                //result.Add(citaResponse.HoraCita, reader.IsDBNull(4) ? null : reader.GetString(4));
                                //result.Add(citaResponse.IdPaciente, reader.IsDBNull(5) ? null : reader.GetString(5));
                                //result.Add(citaResponse.IdAtención, reader.IsDBNull(6) ? null : reader.GetInt64(6));
                                //result.Add(citaResponse.IdEspecialista, reader.IsDBNull(7) ? null : reader.GetString(7));
                                //result.Add(citaResponse.DuracionCita, reader.IsDBNull(8) ? null : reader.GetString(8));
                                //result.Add(citaResponse.numeroatencion, reader.IsDBNull(9) ? null : reader.GetInt32(9));
                            }

                            response.citasResponses = listCita;
                        }
                        else
                        {
                            //result.Add(citaResponse.status, "true");
                            //result.Add(citaResponse.message, String.Empty);
                            //result.Add(citaResponse.estado, "No Atendida");
                            //result.Add(citaResponse.numeroatencion, String.Empty);
                        }

                        return Ok(response.citasResponses);
                    }
                    catch (Exception e)
                    {
                        var result = new Dictionary<string, object>();
                        result.Add("error", e.Message + e.StackTrace);
                        return NotFound(result);
                    }
                }
            }

            return Ok();
        }
    }
}

