using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Imagenes.API.Domain.DbSet
{
    [Table("hcePersonalEspe")]
    public class DoctorEspecialidad
    {
        public int IdPersonal { get; set; }
        public int IdEspecialidad { get; set; }
        public bool IndHabilitado { get; set; }
    }
}
