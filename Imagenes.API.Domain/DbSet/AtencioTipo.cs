using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Imagenes.API.Domain.DbSet
{
    [Table("admAtencionTipo")]
    public class AtencionTipo
    { 
        [Key]
        public short IdAtencionTipo { get; set; }
        public string NomAtencionTipo { get; set; }
        public short IdAtenTipoBase { get; set; }
    }
}
