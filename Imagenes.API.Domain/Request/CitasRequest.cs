using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Imagenes.API.Domain.Request
{
    public class CitasRequest
    {
        public Int32 idTipoDoc { get; set; }
        public string numDoc { get; set; }
        public Int64 idCita { get; set; }
    }
}
