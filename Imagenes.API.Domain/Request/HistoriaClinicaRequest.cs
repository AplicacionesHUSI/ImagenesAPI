using System;
using System.Collections.Generic;
using System.Text;

namespace Imagenes.API.Domain.Request
{
    public class HistoriaClinicaRequest
    {
        public int idPaciente { get; set; }
        public int idAtencion { get; set; }
        public int IdMedico { get; set; }
        public int IdRegistro { get; set; }
        public int IdElemento { get; set; }
        public string nombreElemento { get; set; }
        public string tipoElemento { get; set; }
        public string Elemento { get; set; }

    }
}
