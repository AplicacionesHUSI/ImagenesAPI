using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Imagenes.API.Domain.DbSet
{
    [Table("ASI_ROLE")]
    public class RolUsuario
    {
        [Key]
        [JsonProperty(PropertyName = "cod_enti")]
        public int CodEnti { get; set; }

        [JsonProperty(PropertyName = "cod_role")]
        public string Cod_Role { get; set; }

        [JsonProperty(PropertyName = "cod_usua")]
        public int Cod_Usua { get; set; }
    }
}
