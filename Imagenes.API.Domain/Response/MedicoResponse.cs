using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Imagenes.API.Domain.Response
{
    public class MedicoResponse
    {
        [JsonProperty(PropertyName = "idMedico")]
        public int IdMedico { get; set; }
        [JsonProperty(PropertyName = "idEspecialidad")]
        public int IdEspecialdiad { get; set; } 
        [JsonProperty(PropertyName = "listaEspecialidad")]
        public List<int> ListaEspecialiad { get; set; } 
        [JsonProperty(PropertyName = "tipoMedico")]
        public string TipoMedico { get; set; } = "P";
    }
}
