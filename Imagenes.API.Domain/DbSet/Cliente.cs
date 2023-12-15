using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Imagenes.API.Domain.DbSet
{
    [Table("AdmCliente")]
    public class Cliente
    {
        [JsonProperty(PropertyName = "idPaciente")]
        [Key]
        public int IdCliente { get; set; }
        [JsonProperty(PropertyName = "nombres")]
        public string NomCliente { get; set; }
        [JsonProperty(PropertyName = "apellidos")]
        public string ApeCliente { get; set; }
        [JsonIgnore]
        public short IdSexo { get; set; }
        public byte IdTipoDoc { get; set; }
        public string NumDocumento { get; set; }
        [JsonProperty(PropertyName = "direccion")]
        public string DirCasa { get; set; }
        [JsonProperty(PropertyName = "telefono")]
        public string TelCasa { get; set; }
        [JsonProperty(PropertyName = "celular")]
        public string TelCelular { get; set; }
        [JsonProperty(PropertyName = "Email")]
        public string CorreoE { get; set; }
        [NotMapped]
        public string Genero { get; set; }
        [JsonProperty(PropertyName = "fechaNacimiento")]
        public DateTime FecNacimiento { get; set; }
        [JsonIgnore]
        public bool IndHabilitado { get; set; }
        [NotMapped]
        
        public List<Atencion> Atenciones { get; set; }
    }
}
