using Imagenes.API.Domain.DbSet;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Imagenes.API.Persistence
{
    public class SahiDBContext:  DbContext
    {
        public SahiDBContext(DbContextOptions<SahiDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Especialidad> Especialidades { get; set; }
        public virtual DbSet<DoctorEspecialidad> DoctorEspecialidades { get; set; }
        public virtual DbSet<Doctor> Doctors { get; set; }
        public virtual DbSet<Usuario> Usuarios { get; set; }
        public virtual DbSet<Cliente> Clientes { get; set; }
        public virtual DbSet<Atencion> Atenciones { get; set; }
        public virtual DbSet<AtencionTipo> AtencionTipo { get; set; }
        public virtual DbSet<AtencionTipoBase> AtencionTipoBase { get; set; }
        public virtual DbSet<Elemento> Elementos { get; set; }
        public virtual DbSet<ElementosGrupo> ElementosGrupos { get; set; }
        public virtual DbSet<RolUsuario> RolUsuario { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("dbo");
            modelBuilder.Entity<DoctorEspecialidad>().HasKey(x => new { x.IdPersonal, x.IdEspecialidad });
        }

    }
}
