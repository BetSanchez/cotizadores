using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using pr_proyecto.Models;

namespace pr_proyecto.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext() 
            : base("DefaultConnection") 
        { }

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
            // 1. Desactivar pluralización automática
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            // 2. Clave compuesta para la tabla intermedia
            modelBuilder.Entity<PlantillaServicio>()
                .HasKey(ps => new { ps.IdPlantilla, ps.IdServicio });

            // 3. Mapear cada entidad al nombre real de la tabla (minúsculas y plural)
            modelBuilder.Entity<Pais>().ToTable("paises");
            modelBuilder.Entity<Estado>().ToTable("estados");
            modelBuilder.Entity<Municipio>().ToTable("municipios");
            modelBuilder.Entity<Colonia>().ToTable("colonias");
            modelBuilder.Entity<Empresa>().ToTable("empresas");
            modelBuilder.Entity<Rol>().ToTable("roles");
            modelBuilder.Entity<Usuario>().ToTable("usuarios");
            modelBuilder.Entity<Sucursal>().ToTable("sucursales");
            modelBuilder.Entity<Contacto>().ToTable("contactos");
            modelBuilder.Entity<Cotizacion>().ToTable("cotizaciones");
            modelBuilder.Entity<Servicio>().ToTable("servicios");
            modelBuilder.Entity<CotizacionServicio>().ToTable("cotizacion_servicios");
            modelBuilder.Entity<Plantilla>().ToTable("plantillas");
            modelBuilder.Entity<PlantillaServicio>().ToTable("plantilla_servicios");
            modelBuilder.Entity<Privilegio>().ToTable("privilegios");
            modelBuilder.Entity<PrivilegioRol>().ToTable("privilegio_rol");

            base.OnModelCreating(modelBuilder);
        }
    }
}
