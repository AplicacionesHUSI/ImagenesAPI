using Imagenes.API.Domain.DbSet;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Imagenes.API.Domain.IRepository
{
    public interface IRepositoryElemento
    {
        Task<bool> SaveElemento(Elemento elemento);
        Task<int> SaveGrupo(ElementosGrupo grupo);
        Task<bool> UpdateGrupo(ElementosGrupo grupo);
        Task<ElementosGrupo> FindGrupo(int IdGrupo);

        Task<Elemento> FindElemento(int IdElemento);
    }
}
