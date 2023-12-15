using System;
using System.Collections.Generic;
using System.Text;

namespace Imagenes.API.Domain.Request
{
    public class DoctorRequest
    {
        public Int16 idEspecialidad{ get; set; }
        public string medico { get; set; }
    }
}
