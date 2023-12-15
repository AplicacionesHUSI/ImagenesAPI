using Imagenes.API.Domain.DbSet;
using Imagenes.API.Domain.Request;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Imagenes.API.Domain.IService
{
    public interface IServiceDoctor
    {
        Task<List<Especialidad>> GetList();
        Task<List<Doctor>> GetListDoctor(DoctorRequest usuarioRequest);
        Task<Doctor> GetDoctor(UsuarioRequest request);

    }
}
