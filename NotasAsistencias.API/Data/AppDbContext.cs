using Microsoft.EntityFrameworkCore;
using NotasAsistencias.API.Models;

namespace NotasAsistencias.API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Estudiante> Estudiantes { get; set; }
        public DbSet<Materia> Materias { get; set; }
        public DbSet<Docente> Docentes { get; set; }
        public DbSet<MateriaEstudiante> MateriasEstudiantes { get; set; }
        public DbSet<DocenteMateria> DocentesMaterias { get; set; }
        public DbSet<Calificacion> Calificaciones { get; set; }
        public DbSet<Asistencia> Asistencias { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Relación Estudiante - Materia
            modelBuilder.Entity<MateriaEstudiante>()
                .HasKey(me => new { me.EstudianteId, me.MateriaId });

            modelBuilder.Entity<MateriaEstudiante>()
                .HasOne(me => me.Estudiante)
                .WithMany(e => e.Materias)
                .HasForeignKey(me => me.EstudianteId);

            modelBuilder.Entity<MateriaEstudiante>()
                .HasOne(me => me.Materia)
                .WithMany(m => m.Estudiantes)
                .HasForeignKey(me => me.MateriaId);

            // Relación Docente - Materia
            modelBuilder.Entity<DocenteMateria>()
                .HasKey(dm => new { dm.DocenteId, dm.MateriaId });

            modelBuilder.Entity<DocenteMateria>()
                .HasOne(dm => dm.Docente)
                .WithMany(d => d.MateriasAsignadas)
                .HasForeignKey(dm => dm.DocenteId);

            modelBuilder.Entity<DocenteMateria>()
                .HasOne(dm => dm.Materia)
                .WithMany(m => m.DocentesAsignados)
                .HasForeignKey(dm => dm.MateriaId);

            // Relaciones de Calificacion

            modelBuilder.Entity<Calificacion>()
                .HasOne(c => c.Docente)
                .WithMany()
                .HasForeignKey(c => c.DocenteId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Calificacion>()
                .HasOne(c => c.Materia)
                .WithMany()
                .HasForeignKey(c => c.MateriaId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Calificacion>()
                .HasOne(c => c.Estudiante)
                .WithMany()
                .HasForeignKey(c => c.EstudianteId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

