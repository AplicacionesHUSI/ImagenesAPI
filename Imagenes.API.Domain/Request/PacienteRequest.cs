using System;
using System.Collections.Generic;
using System.Text;

namespace Imagenes.API.Domain.Request
{
    public class PacienteRequest
    {
      public byte idTipoDoc { get; set; }
        public string documento { get; set; }
    }
}
