using Imagenes.API.Domain.DbSet;
using Imagenes.API.Domain.Request;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Imagenes.API.Domain.IService
{
    public interface IServiceHistoriaClinica
    {
        Task<int> SaveElement(HistoriaClinicaRequest request);
        Task<Elemento> FindElemento(int IdElemento);

        /// <summary>
        /// Registrar elemento (foto, video, imagen o pdf)
        /// </summary>
        /// <param name="request">Datos que se almacenan en la bd.</param>
        /// <returns>Retorna el estado del proceso.</returns>
        Task<int> SaveElements(HistoriaClinicaRequest request);
    }
}
