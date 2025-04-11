﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NotasAsistencias.API.Data;

#nullable disable

namespace NotasAsistencias.API.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20250403051432_ActualizarAsistencia")]
    partial class ActualizarAsistencia
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
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

                    b.Property<int>("Estado")
                        .HasColumnType("int");

                    b.Property<int>("EstudianteId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Fecha")
                        .HasColumnType("datetime2");

                    b.Property<string>("Observaciones")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("AsistenciaId");

                    b.HasIndex("EstudianteId");

                    b.ToTable("Asistencias");
                });

            modelBuilder.Entity("NotasAsistencias.API.Models.Calificacion", b =>
                {
                    b.Property<int>("CalificacionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CalificacionId"));

                    b.Property<int>("EstudianteId")
                        .HasColumnType("int");

                    b.Property<DateTime>("FechaRegistro")
                        .HasColumnType("datetime2");

                    b.Property<string>("Materia")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("Nota")
                        .HasColumnType("int");

                    b.HasKey("CalificacionId");

                    b.HasIndex("EstudianteId");

                    b.ToTable("Calificaciones");
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

                    b.HasIndex("Matricula")
                        .IsUnique()
                        .HasFilter("[Matricula] IS NOT NULL");

                    b.ToTable("Estudiantes");
                });

            modelBuilder.Entity("NotasAsistencias.API.Models.Asistencia", b =>
                {
                    b.HasOne("NotasAsistencias.API.Models.Estudiante", "Estudiante")
                        .WithMany("Asistencias")
                        .HasForeignKey("EstudianteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Estudiante");
                });

            modelBuilder.Entity("NotasAsistencias.API.Models.Calificacion", b =>
                {
                    b.HasOne("NotasAsistencias.API.Models.Estudiante", "Estudiante")
                        .WithMany("Calificaciones")
                        .HasForeignKey("EstudianteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Estudiante");
                });

            modelBuilder.Entity("NotasAsistencias.API.Models.Estudiante", b =>
                {
                    b.Navigation("Asistencias");

                    b.Navigation("Calificaciones");
                });
#pragma warning restore 612, 618
        }
    }
}
