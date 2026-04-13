/* 
Entrada: Opciones de configuración de la base de datos (DbContextOptions)
Salida: Contexto de base de datos CineTecDbContext
Restricciones: La conexión a la base de datos debe estar correctamente configurada
Descripción: Define el contexto de la base de datos utilizando Entity Framework, 
incluyendo las tablas (DbSet) y las relaciones entre entidades del sistema CineTec
Modificado por: Mario
*/

using Microsoft.EntityFrameworkCore;
using CineTec.API.Models;

namespace CineTec.API.Data
{
    // Clase que representa el contexto de la base de datos
    public class CineTecDbContext : DbContext
    {
        // Constructor que recibe la configuración del contexto (cadena de conexión, etc.)
        public CineTecDbContext(DbContextOptions<CineTecDbContext> options) : base(options)
        {
        }

        // Tabla de Administradores
        public DbSet<Administrador> Administradores => Set<Administrador>();

        // Tabla de Películas
        public DbSet<Pelicula> Peliculas => Set<Pelicula>();

        // Tabla de Sucursales
        public DbSet<Sucursal> Sucursales => Set<Sucursal>();

        // Tabla de Salas
        public DbSet<Sala> Salas => Set<Sala>();

        // Tabla de Proyecciones
        public DbSet<Proyeccion> Proyecciones => Set<Proyeccion>();

        // Método para configurar relaciones y restricciones entre tablas
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Relación: Sala pertenece a una Sucursal (clave foránea SucursalId)
            modelBuilder.Entity<Sala>()
                .HasOne(s => s.Sucursal)
                .WithMany()
                .HasForeignKey(s => s.SucursalId);

            // Relación: Proyección pertenece a una Película (clave foránea PeliculaId)
            modelBuilder.Entity<Proyeccion>()
                .HasOne(p => p.Pelicula)
                .WithMany()
                .HasForeignKey(p => p.PeliculaId);

            // Relación: Proyección pertenece a una Sala (clave foránea SalaId)
            modelBuilder.Entity<Proyeccion>()
                .HasOne(p => p.Sala)
                .WithMany()
                .HasForeignKey(p => p.SalaId);
        }
    }
}