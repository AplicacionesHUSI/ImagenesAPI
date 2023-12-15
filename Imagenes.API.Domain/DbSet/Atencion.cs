using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Imagenes.API.Domain.DbSet
{
    [Table("AdmAtencion")]
    public class Atencion
    {
        [Key]
        public int IdAtencion { get; set; }
        [JsonIgnore]
        public int IdCliente { get; set; }
        public short IdAtencionTipo { get; set; }
        [JsonProperty(PropertyName = "fechaIngreso")]
        public DateTime FecIngreso { get; set; }
        [JsonIgnore]
        public bool IndActivado { get; set; }
        [JsonIgnore]
        public bool IndHabilitado { get; set; }
        [NotMapped]
        public string TipoAtencion { get; set; }
    }
}
