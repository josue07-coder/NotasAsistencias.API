using Microsoft.EntityFrameworkCore;
using NotasAsistencias.API.Models;

namespace NotasAsistencias.API.Data
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }

        public DbSet<Estudiante> Estudiantes { get; set; }
        public DbSet<Calificacion> Calificaciones { get; set; }
        public DbSet<Asistencia> Asistencias { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Estudiante>()
                .HasIndex(e => e.Matricula)
                .IsUnique();

            modelBuilder.Entity<Estudiante>()
                .HasMany(e => e.Calificaciones)
                .WithOne(c => c.Estudiante)
                .HasForeignKey(c => c.EstudianteId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Estudiante>()
                .HasMany(e => e.Asistencias)
                .WithOne(a => a.Estudiante)
                .HasForeignKey(a => a.EstudianteId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
