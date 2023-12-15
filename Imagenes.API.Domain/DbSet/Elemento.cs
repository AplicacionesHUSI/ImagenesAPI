using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Imagenes.API.Domain.DbSet
{

    [Table("hceImagenesElementos")]
    public class Elemento
    {
        [Key]
        public int IdElemento { get; set; }
        public int IdGrupo { get; set; }
        public string Tipo { get; set; }
        public DateTime FecRegistro { get; set; }
        [Column("Elemento")]
        public string Element { get; set; }
        public string NombreElemento { get; set; }
    }
}
