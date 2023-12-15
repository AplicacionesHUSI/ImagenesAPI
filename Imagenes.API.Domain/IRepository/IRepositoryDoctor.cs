using Imagenes.API.Domain.DbSet;
using Imagenes.API.Domain.Request;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Imagenes.API.Domain.IRepository
{
    public interface IRepositoryDoctor
    {
        Task<List<Especialidad>> GetList();
        Task<List<Doctor>> GetListDoctor(DoctorRequest request);
        Task<Doctor> GetDoctor(UsuarioRequest request);
    }
}
