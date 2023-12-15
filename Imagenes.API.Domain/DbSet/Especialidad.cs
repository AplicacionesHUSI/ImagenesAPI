using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Imagenes.API.Domain.DbSet
{
    [Table("hceEspecialidad")]
    public class Especialidad
    {
        
        [JsonProperty(PropertyName = "idEspecialidad")]
        [Key]
        public Int16 IdEspecialidad {get;set;}
        [JsonProperty(PropertyName = "Especialidad")]
        public string NomEspecialidad { get; set; }
        [JsonIgnore]
        public bool IndHabilitado { get; set; }
    }
}
