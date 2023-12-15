using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Imagenes.API.Domain.DbSet
{
    [Table("hceImageneGrupo")]
    public class ElementosGrupo
    {
        [Key]
        public int IdGrupo { get; set; }
        public int IdRegistro { get; set; }
        public int IdAtencion { get; set; }
        public int IdMedico { get; set; }
        public DateTime FecCreacion { get; set; }

        public string Asunto { get; set; }
        public string Observaciones { get; set; }

    }
}
