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
    }
}
