using Imagenes.API.Domain.DbSet;
using Imagenes.API.Domain.Request;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Imagenes.API.Domain.IService
{
    public interface IServicePaciente
    {
        Task<Cliente> GetCliente(PacienteRequest request);
    }
}
