using Imagenes.API.Domain.DbSet;
using Imagenes.API.Domain.IRepository;
using Imagenes.API.Domain.Request;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Imagenes.API.Persistence.repository
{
    public class RepositoryPaciente : IRepositoryPaciente
    {
        private readonly SahiDBContext _context;
        public RepositoryPaciente(SahiDBContext dbContext)
        {
            _context = dbContext;
        }
        public async Task<Cliente> GetCliente(PacienteRequest request)
        {
            var query = from c in _context.Clientes                        
                        where c.IdTipoDoc == request.idTipoDoc && c.NumDocumento == request.documento
                        select c;
            var cliente=await query.FirstOrDefaultAsync();
           
            if (cliente != null) {
                cliente.Genero = cliente.IdSexo == 1 ? "Masculino" : "Femenino";
                var atenciones =
                    from x in _context.Atenciones
                    join t in _context.AtencionTipo on 
                    x.IdAtencionTipo equals t.IdAtencionTipo
                    join b in _context.AtencionTipoBase on
                    t.IdAtenTipoBase equals b.IdAtenTipoBase
                    where x.IdCliente == cliente.IdCliente && x.IndActivado && x.IndHabilitado
                    select new Atencion { 
                        IdAtencion=x.IdAtencion,
                        FecIngreso=x.FecIngreso,
                        TipoAtencion=b.NomAtenTipoBase
                        ,IdAtencionTipo=b.IdAtenTipoBase
                    };
                cliente.Atenciones = await atenciones.ToListAsync();
            }
            return cliente;
        }
    }
}
