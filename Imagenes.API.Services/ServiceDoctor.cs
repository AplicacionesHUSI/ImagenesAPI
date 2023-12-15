using Imagenes.API.Domain.DbSet;
using Imagenes.API.Domain.IRepository;
using Imagenes.API.Domain.IService;
using Imagenes.API.Domain.Request;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Imagenes.API.Services
{
    public class ServiceDoctor : IServiceDoctor
    {
        private readonly IRepositoryDoctor _repo;

        public ServiceDoctor(IRepositoryDoctor repositoryEspecialidad) {
            _repo = repositoryEspecialidad;        
        }

        public async Task<Doctor> GetDoctor(UsuarioRequest request)
        {
            return await _repo.GetDoctor(request);
        }

        public async Task<List<Especialidad>> GetList()
        {
            return await _repo.GetList();
        }

        public async Task<List<Doctor>> GetListDoctor(DoctorRequest request)
        {
            return await _repo.GetListDoctor(request);
        }
    }
}
