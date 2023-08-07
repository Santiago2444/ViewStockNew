using Microsoft.EntityFrameworkCore;
using WebStock.Models;

namespace WebStock.Data
{
    public class DBViewStockContext: DbContext
    {
        public DBViewStockContext(DbContextOptions<DBViewStockContext> options)
           : base(options)
        {

        }
        public DbSet<Localidad> Localidades { get; set; }
        public DbSet<Marca> Marcas { get; set; }
        public DbSet<TipoProducto> TipoProducto { get; set; }
        public DbSet<SPEC> SPECS { get; set; }
        public DbSet<Provincia> Provincias { get; set; }
        public DbSet<TipoDeUsuario> TipoDeUsuarios { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Proveedor> Proveedores { get; set; }
        public DbSet<Producto> Productos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Localidad>(entity =>
            {
                entity.ToTable("localidades");
            });
            //
            modelBuilder.Entity<Marca>(entity =>
            {
                entity.ToTable("marcas");
            });
            //
            modelBuilder.Entity<TipoProducto>(entity =>
            {
                entity.ToTable("tipoProductos");
            });
            //
            modelBuilder.Entity<SPEC>(entity =>
            {
                entity.ToTable("especificaciones");
            });
            //
            modelBuilder.Entity<Provincia>(entity =>
            {
                entity.ToTable("provincias");
            });
            //
            modelBuilder.Entity<TipoDeUsuario>(entity =>
            {
                entity.ToTable("tiposDeUsuarios");
            });
            //
            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.ToTable("usuarios");
            });
            //
            modelBuilder.Entity<Proveedor>(entity =>
            {
                entity.ToTable("proveedores");
            });
            //
            modelBuilder.Entity<Producto>(entity =>
            {
                entity.ToTable("productos");
            });
        }

    }
}
