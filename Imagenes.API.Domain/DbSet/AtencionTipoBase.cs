using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Imagenes.API.Domain.DbSet
{
    [Table("admAtenTipoBase")]
    public class AtencionTipoBase
    {
        [Key]
        public short IdAtenTipoBase { get; set; }
        public string NomAtenTipoBase { get; set; }      
    }
}
