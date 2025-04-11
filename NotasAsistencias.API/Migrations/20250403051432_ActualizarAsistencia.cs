using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NotasAsistencias.API.Migrations
{
    /// <inheritdoc />
    public partial class ActualizarAsistencia : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ConExcusa",
                table: "Asistencias");

            migrationBuilder.DropColumn(
                name: "Presente",
                table: "Asistencias");

            migrationBuilder.AddColumn<int>(
                name: "Estado",
                table: "Asistencias",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Observaciones",
                table: "Asistencias",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Estado",
                table: "Asistencias");

            migrationBuilder.DropColumn(
                name: "Observaciones",
                table: "Asistencias");

            migrationBuilder.AddColumn<bool>(
                name: "ConExcusa",
                table: "Asistencias",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Presente",
                table: "Asistencias",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
