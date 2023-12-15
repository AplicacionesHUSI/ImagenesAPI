using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Imagenes.API.Domain.DbSet
{
    
    [Table("ASI_USUA")]
    public class Usuario
    {
        [Key]
        public Int16 IdUsuario { get; set; }
        public string UsuarioWin { get; set; }
        public string Ind_esta { get; set; }

        [JsonProperty(PropertyName = "cod_usua")]
        public int Cod_Usua { get; set; }
    }
}
