using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Imagenes.API.Domain.DbSet
{
    [Table("hcePersonal")]
    public class Doctor
    {
        [Key]
        [JsonProperty(PropertyName = "idMedico")]
        public int IdPersonal { get; set; }
        [JsonProperty(PropertyName = "nombres")]
        public string NomPersonal { get; set; }
        [JsonProperty(PropertyName = "apellidos")]
        public string ApePersonal { get; set; }
        public string TipoMedico { get; set; }

        [NotMapped]
        public string Email { get; set; }
        [NotMapped]
        public int IdEspecialidad { get; set; }

        [NotMapped]
        public List<int> ListaEspecialiad { get; set; }
        [JsonIgnore]
        public Int16 IdProfesion { get; set; }
        [JsonIgnore]
        public Int16 IdUsuario { get; set; }
        [JsonIgnore]
        public bool IndHabilitado { get; set; }


    }
}
