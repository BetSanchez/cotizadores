using System.Data.Entity;
using pr_proyecto.Models;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace pr_proyecto.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext() 
            : base("DefaultConnection") 
        {
            // Configurar para MySQL
            this.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);
        }

        // DbSets
        public DbSet<Pais> Paises { get; set; }
        public DbSet<Estado> Estados { get; set; }
        public DbSet<Municipio> Municipios { get; set; }
        public DbSet<Colonia> Colonias { get; set; }
        public DbSet<Empresa> Empresas { get; set; }
        public DbSet<Rol> Roles { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Sucursal> Sucursales { get; set; }
        public DbSet<Contacto> Contactos { get; set; }
        public DbSet<Cotizacion> Cotizaciones { get; set; }
        public DbSet<Servicio> Servicios { get; set; }
        public DbSet<CotizacionServicio> CotizacionServicios { get; set; }
        public DbSet<Plantilla> Plantillas { get; set; }
        public DbSet<PlantillaServicio> PlantillaServicios { get; set; }
        public DbSet<Privilegio> Privilegios { get; set; }
        public DbSet<PrivilegioRol> PrivilegiosRoles { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Configuración específica para MySQL
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
            
            // Configurar clave compuesta para PlantillaServicio
            modelBuilder.Entity<PlantillaServicio>()
                .HasKey(ps => new { ps.IdPlantilla, ps.IdServicio });

            // Configurar relaciones específicas
            modelBuilder.Entity<Sucursal>()
                .HasRequired(s => s.Empresa)
                .WithMany(e => e.Sucursales)
                .HasForeignKey(s => s.IdEmpresa);

            modelBuilder.Entity<Sucursal>()
                .HasRequired(s => s.Colonia)
                .WithMany(c => c.Sucursales)
                .HasForeignKey(s => s.IdColonia)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Contacto>()
                .HasRequired(c => c.Sucursal)
                .WithMany(s => s.Contactos)
                .HasForeignKey(c => c.IdSucursal);

            modelBuilder.Entity<Usuario>()
                .HasRequired(u => u.Rol)       
                .WithMany(r => r.Usuarios)    
                .HasForeignKey(u => u.IdRol);  

            base.OnModelCreating(modelBuilder);
        }

    }
}
