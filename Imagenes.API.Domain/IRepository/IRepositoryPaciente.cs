using Imagenes.API.Domain.DbSet;
using Imagenes.API.Domain.Request;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Imagenes.API.Domain.IRepository
{
    public interface IRepositoryPaciente
    {
        Task<Cliente> GetCliente(PacienteRequest request);
    }
}
