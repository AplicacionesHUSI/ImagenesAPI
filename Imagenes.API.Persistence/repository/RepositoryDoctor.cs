using Imagenes.API.Domain.DbSet;
using Imagenes.API.Domain.IRepository;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Imagenes.API.Domain.Request;

namespace Imagenes.API.Persistence.repository
{
    public class RepositoryDoctor : IRepositoryDoctor
    {
        private readonly SahiDBContext _context;

        private const string rolImagem = "IMAGEM";

        public RepositoryDoctor(SahiDBContext dbContext)
        {
            _context = dbContext;
        }


        public async Task<List<Especialidad>> GetList()
        {
            var query = from d in _context.Doctors
                        join e in _context.DoctorEspecialidades
                        on d.IdPersonal equals e.IdPersonal
                        join u in _context.Usuarios
                        on d.IdUsuario equals u.IdUsuario
                        where u.Ind_esta=="A" && e.IndHabilitado
                      
                        select e.IdEspecialidad;
            var listEspece =query.ToList();


            return await _context.Especialidades.Where(x=>x.IndHabilitado && listEspece.Contains(x.IdEspecialidad)).OrderBy(x=>x.NomEspecialidad).ToListAsync();
        }

        
        public async Task<List<Doctor>> GetListDoctor(DoctorRequest request)
        {

            IQueryable<Doctor> query;
            query = from d in _context.Doctors
                    join e in _context.DoctorEspecialidades
                    on d.IdPersonal equals e.IdPersonal
                    join u in _context.Usuarios
                    on d.IdUsuario equals u.IdUsuario
                    where e.IdEspecialidad == request.idEspecialidad && e.IndHabilitado && d.IdProfesion == 1 && d.IndHabilitado && u.Ind_esta=="A"                   

                    select new Doctor()
                    {
                        IdPersonal = d.IdPersonal,
                        NomPersonal = d.NomPersonal,
                        ApePersonal = d.ApePersonal
                    ,IdEspecialidad=e.IdEspecialidad,
                        TipoMedico=d.TipoMedico,
                        Email = $"{u.UsuarioWin}@husi.org.co"
                    };

            if (!string.IsNullOrEmpty(request.medico))
                query = query.Where(x => x.NomPersonal.Contains(request.medico) || x.ApePersonal.Contains(request.medico));

            if (!query.Any())
            {
                IQueryable<Doctor> query2;
                query2 = from d in _context.Doctors
                        join e in _context.DoctorEspecialidades
                        on d.IdPersonal equals e.IdPersonal
                        join u in _context.Usuarios
                        on d.IdUsuario equals u.IdUsuario
                         join c in _context.RolUsuario
                             on u.Cod_Usua equals c.Cod_Usua
                         where e.IdEspecialidad == request.idEspecialidad && e.IndHabilitado && d.IndHabilitado && u.Ind_esta == "A" && c.Cod_Role == "IMAGEM"

                         select new Doctor()
                        {
                            IdPersonal = d.IdPersonal,
                            NomPersonal = d.NomPersonal,
                            ApePersonal = d.ApePersonal
                        ,
                            IdEspecialidad = e.IdEspecialidad,
                            TipoMedico = "P",
                            Email = $"{u.UsuarioWin}@husi.org.co"
                        };

                if (!string.IsNullOrEmpty(request.medico))
                    query = query2.Where(x => x.NomPersonal.Contains(request.medico) || x.ApePersonal.Contains(request.medico));
            }

            return await query.ToListAsync();
        }

        public async Task<Doctor> GetDoctor(UsuarioRequest request)
        {
            var query = from d in _context.Doctors
                        join u in _context.Usuarios
                        on d.IdUsuario equals u.IdUsuario
                        join e in _context.DoctorEspecialidades
                        on d.IdPersonal equals e.IdPersonal
                        join c in _context.RolUsuario
                        on u.Cod_Usua equals c.Cod_Usua
                        where (d.IdProfesion == 1 || c.Cod_Role == "IMAGEM" ) && d.IndHabilitado && u.Ind_esta == "A" && u.UsuarioWin == request.usuario && e.IndHabilitado
                        select new Doctor {
                            IdEspecialidad = e.IdEspecialidad,
                            Email = u.UsuarioWin.Contains("@") ? u.UsuarioWin : u.UsuarioWin + "@husi.org.co",
                            IdPersonal = d.IdPersonal,
                            TipoMedico =  c.Cod_Role == "IMAGEM" ? "P" : d.TipoMedico
                        };
            var result = await query.FirstOrDefaultAsync();

            if (result != null)
            {
                var queryLista = from d in _context.Doctors
                                 join u in _context.Usuarios
                                 on d.IdUsuario equals u.IdUsuario
                                 join e in _context.DoctorEspecialidades
                                 on d.IdPersonal equals e.IdPersonal
                                 join c in _context.RolUsuario
                                 on u.Cod_Usua equals c.Cod_Usua
                                 where (d.IdProfesion == 1 || c.Cod_Role == "IMAGEM") && d.IndHabilitado && u.Ind_esta == "A" && u.UsuarioWin == request.usuario && e.IndHabilitado
                                 group e by e.IdEspecialidad into g
                                 select g.Key;
                var lista = await queryLista.ToListAsync();
                result.ListaEspecialiad = lista;
            }
           
            return result;
        }
    }
}
