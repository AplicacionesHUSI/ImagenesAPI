using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Imagenes.API.Domain.Response
{
    public class CitaResponse
    {
        public string IdEspecialidad { get; set; }
        public string NombreEspecialista { get; set; }
        public string  EmailEspecialista { get; set; }
        public DateTime FechaCita { get; set; }
        public DateTime HoraCita { get; set; }
        public Int64 IdPaciente { get; set; }
        public Int64 IdAtención { get; set; }
        public string IdEspecialista { get; set; }
        public string DuracionCita { get; set; }
    }
    public class ListaCita 
    {
        public int Codigo { get; set; }
        public string Mensaje { get; set; }
        public List<CitaResponse> citasResponses { get; set; }
    }
}
