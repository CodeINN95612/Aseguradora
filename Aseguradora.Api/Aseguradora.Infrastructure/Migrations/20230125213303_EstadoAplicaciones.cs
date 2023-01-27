using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aseguradora.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class EstadoAplicaciones : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Estado",
                table: "Aplicacion",
                type: "bit",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Estado",
                table: "Aplicacion");
        }
    }
}
