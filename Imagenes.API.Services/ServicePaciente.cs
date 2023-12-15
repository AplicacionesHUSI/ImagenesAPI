using Imagenes.API.Domain.DbSet;
using Imagenes.API.Domain.IRepository;
using Imagenes.API.Domain.IService;
using Imagenes.API.Domain.Request;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Imagenes.API.Services
{
    public class ServicePaciente : IServicePaciente
    {
        private readonly IRepositoryPaciente _repo;

        public ServicePaciente(IRepositoryPaciente repository)
        { 
            _repo = repository;

        }
        public async Task<Cliente> GetCliente(PacienteRequest request)
        {
            return await _repo.GetCliente(request);
        }
    }
}
