﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NotasAsistencias.API.Data;

#nullable disable

namespace NotasAsistencias.API.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("NotasAsistencias.API.Models.Asistencia", b =>
                {
                    b.Property<int>("AsistenciaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AsistenciaId"));

                    b.Property<int>("DocenteId")
                        .HasColumnType("int");

                    b.Property<string>("Estado")
                        .IsRequired()
                        .HasColumnType("nvarchar(20)");

                    b.Property<int>("EstudianteId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Fecha")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("FechaRegistro")
                        .HasColumnType("datetime2");

                    b.Property<int>("MateriaId")
                        .HasColumnType("int");

                    b.Property<string>("Observaciones")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("AsistenciaId");

                    b.HasIndex("DocenteId");

                    b.HasIndex("EstudianteId");

                    b.HasIndex("MateriaId");

                    b.ToTable("Asistencias");
                });

            modelBuilder.Entity("NotasAsistencias.API.Models.Calificacion", b =>
                {
                    b.Property<int>("CalificacionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CalificacionId"));

                    b.Property<int>("DocenteId")
                        .HasColumnType("int");

                    b.Property<int>("EstudianteId")
                        .HasColumnType("int");

                    b.Property<int?>("EstudianteId1")
                        .HasColumnType("int");

                    b.Property<DateTime>("FechaRegistro")
                        .HasColumnType("datetime2");

                    b.Property<int>("MateriaId")
                        .HasColumnType("int");

                    b.Property<int?>("MateriaId1")
                        .HasColumnType("int");

                    b.Property<int>("Nota")
                        .HasColumnType("int");

                    b.HasKey("CalificacionId");

                    b.HasIndex("DocenteId");

                    b.HasIndex("EstudianteId");

                    b.HasIndex("EstudianteId1");

                    b.HasIndex("MateriaId");

                    b.HasIndex("MateriaId1");

                    b.ToTable("Calificaciones");
                });

            modelBuilder.Entity("NotasAsistencias.API.Models.Docente", b =>
                {
                    b.Property<int>("DocenteId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DocenteId"));

                    b.Property<string>("Correo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Direccion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Estado")
                        .HasColumnType("bit");

                    b.Property<DateTime>("FechaContratacion")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("FechaIngreso")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("FechaNacimiento")
                        .HasColumnType("datetime2");

                    b.Property<string>("NombreCompleto")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Telefono")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("DocenteId");

                    b.ToTable("Docentes");
                });

            modelBuilder.Entity("NotasAsistencias.API.Models.DocenteMateria", b =>
                {
                    b.Property<int>("DocenteId")
                        .HasColumnType("int");

                    b.Property<int>("MateriaId")
                        .HasColumnType("int");

                    b.HasKey("DocenteId", "MateriaId");

                    b.HasIndex("MateriaId");

                    b.ToTable("DocentesMaterias");
                });

            modelBuilder.Entity("NotasAsistencias.API.Models.Estudiante", b =>
                {
                    b.Property<int>("EstudianteId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("EstudianteId"));

                    b.Property<bool>("Activo")
                        .HasColumnType("bit");

                    b.Property<string>("Correo")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Direccion")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<DateTime>("FechaNacimiento")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("FechaRegistro")
                        .HasColumnType("datetime2");

                    b.Property<string>("Matricula")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("NombreCompleto")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Telefono")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("EstudianteId");

                    b.ToTable("Estudiantes");
                });

            modelBuilder.Entity("NotasAsistencias.API.Models.Materia", b =>
                {
                    b.Property<int>("MateriaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MateriaId"));

                    b.Property<bool>("Activa")
                        .HasColumnType("bit");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("MateriaId");

                    b.ToTable("Materias");
                });

            modelBuilder.Entity("NotasAsistencias.API.Models.MateriaEstudiante", b =>
                {
                    b.Property<int>("EstudianteId")
                        .HasColumnType("int");

                    b.Property<int>("MateriaId")
                        .HasColumnType("int");

                    b.HasKey("EstudianteId", "MateriaId");

                    b.HasIndex("MateriaId");

                    b.ToTable("MateriasEstudiantes");
                });

            modelBuilder.Entity("NotasAsistencias.API.Models.Asistencia", b =>
                {
                    b.HasOne("NotasAsistencias.API.Models.Docente", "Docente")
                        .WithMany()
                        .HasForeignKey("DocenteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("NotasAsistencias.API.Models.Estudiante", "Estudiante")
                        .WithMany("Asistencias")
                        .HasForeignKey("EstudianteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("NotasAsistencias.API.Models.Materia", "Materia")
                        .WithMany("Asistencias")
                        .HasForeignKey("MateriaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Docente");

                    b.Navigation("Estudiante");

                    b.Navigation("Materia");
                });

            modelBuilder.Entity("NotasAsistencias.API.Models.Calificacion", b =>
                {
                    b.HasOne("NotasAsistencias.API.Models.Docente", "Docente")
                        .WithMany()
                        .HasForeignKey("DocenteId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("NotasAsistencias.API.Models.Estudiante", "Estudiante")
                        .WithMany()
                        .HasForeignKey("EstudianteId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("NotasAsistencias.API.Models.Estudiante", null)
                        .WithMany("Calificaciones")
                        .HasForeignKey("EstudianteId1");

                    b.HasOne("NotasAsistencias.API.Models.Materia", "Materia")
                        .WithMany()
                        .HasForeignKey("MateriaId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("NotasAsistencias.API.Models.Materia", null)
                        .WithMany("Calificaciones")
                        .HasForeignKey("MateriaId1");

                    b.Navigation("Docente");

                    b.Navigation("Estudiante");

                    b.Navigation("Materia");
                });

            modelBuilder.Entity("NotasAsistencias.API.Models.DocenteMateria", b =>
                {
                    b.HasOne("NotasAsistencias.API.Models.Docente", "Docente")
                        .WithMany("MateriasAsignadas")
                        .HasForeignKey("DocenteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("NotasAsistencias.API.Models.Materia", "Materia")
                        .WithMany("DocentesAsignados")
                        .HasForeignKey("MateriaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Docente");

                    b.Navigation("Materia");
                });

            modelBuilder.Entity("NotasAsistencias.API.Models.MateriaEstudiante", b =>
                {
                    b.HasOne("NotasAsistencias.API.Models.Estudiante", "Estudiante")
                        .WithMany("Materias")
                        .HasForeignKey("EstudianteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("NotasAsistencias.API.Models.Materia", "Materia")
                        .WithMany("Estudiantes")
                        .HasForeignKey("MateriaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Estudiante");

                    b.Navigation("Materia");
                });

            modelBuilder.Entity("NotasAsistencias.API.Models.Docente", b =>
                {
                    b.Navigation("MateriasAsignadas");
                });

            modelBuilder.Entity("NotasAsistencias.API.Models.Estudiante", b =>
                {
                    b.Navigation("Asistencias");

                    b.Navigation("Calificaciones");

                    b.Navigation("Materias");
                });

            modelBuilder.Entity("NotasAsistencias.API.Models.Materia", b =>
                {
                    b.Navigation("Asistencias");

                    b.Navigation("Calificaciones");

                    b.Navigation("DocentesAsignados");

                    b.Navigation("Estudiantes");
                });
#pragma warning restore 612, 618
        }
    }
}
