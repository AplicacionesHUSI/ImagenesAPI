using Imagenes.API.Domain.DbSet;
using Imagenes.API.Domain.IRepository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
namespace Imagenes.API.Persistence.repository
{
    public class RepositoryElemento : IRepositoryElemento
    {
        private readonly SahiDBContext _context;
        public RepositoryElemento(SahiDBContext dbContext)
        {
            _context = dbContext;
        }

        public async Task<Elemento> FindElemento(int IdElemento)
        {
            return await _context.Elementos.FirstOrDefaultAsync(x => x.IdElemento== IdElemento);
        }

        public async Task<ElementosGrupo> FindGrupo(int IdGrupo)
        {
            return await _context.ElementosGrupos.FirstOrDefaultAsync(x => x.IdRegistro == IdGrupo);
        }

        public async Task<bool> SaveElemento(Elemento elemento)
        {
            _context.Elementos.Add(elemento);
            await _context.SaveChangesAsync();           
           return true;
        }

        public async Task<int> SaveGrupo(ElementosGrupo grupo)
        {
            var strategy = _context.Database.CreateExecutionStrategy();
            await strategy.ExecuteAsync(async () =>
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    int max = 0;
                    if (_context.ElementosGrupos.Count() > 0)
                        max = _context.ElementosGrupos.Max(x => x.IdGrupo);
                    max++;
                    grupo.IdGrupo   = (int)max;
                    _context.ElementosGrupos.Add(grupo);
                    await _context.SaveChangesAsync();
                    transaction.Commit();
                }
            });
           
            return grupo.IdGrupo;
        }

        public async Task<bool> UpdateGrupo(ElementosGrupo grupo)
        {
            _context.ElementosGrupos.Update(grupo);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
