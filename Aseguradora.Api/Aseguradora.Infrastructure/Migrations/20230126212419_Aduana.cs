using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aseguradora.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Aduana : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Consignada",
                table: "Aplicacion",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "EmbarcadoPor",
                table: "Aplicacion",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "NotaPredio",
                table: "Aplicacion",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "OrdenCompra",
                table: "Aplicacion",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Perteneciente",
                table: "Aplicacion",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TipoTransporte",
                table: "Aplicacion",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "ListaAplicacionesAduanas",
                columns: table => new
                {
                    IdAplicacion = table.Column<int>(type: "int", nullable: false),
                    Item = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Marca = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Numero = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PesoBruto = table.Column<decimal>(type: "decimal(18,5)", precision: 18, scale: 5, nullable: false),
                    Bultos = table.Column<int>(type: "int", nullable: false),
                    MontoTotal = table.Column<decimal>(type: "decimal(18,5)", precision: 18, scale: 5, nullable: false),
                    PorcentajeOtrosGastos = table.Column<decimal>(type: "decimal(18,5)", precision: 18, scale: 5, nullable: false),
                    SumaAseguradora = table.Column<decimal>(type: "decimal(18,5)", precision: 18, scale: 5, nullable: false),
                    ValorPrima = table.Column<decimal>(type: "decimal(18,5)", precision: 18, scale: 5, nullable: false),
                    DescripcionContenido = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Observaciones = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ListaAplicacionesAduanas", x => x.IdAplicacion);
                    table.ForeignKey(
                        name: "FK_ListaAplicacionesAduanas_Aplicacion_IdAplicacion",
                        column: x => x.IdAplicacion,
                        principalTable: "Aplicacion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ListaAplicacionesAduanas");

            migrationBuilder.DropColumn(
                name: "Consignada",
                table: "Aplicacion");

            migrationBuilder.DropColumn(
                name: "EmbarcadoPor",
                table: "Aplicacion");

            migrationBuilder.DropColumn(
                name: "NotaPredio",
                table: "Aplicacion");

            migrationBuilder.DropColumn(
                name: "OrdenCompra",
                table: "Aplicacion");

            migrationBuilder.DropColumn(
                name: "Perteneciente",
                table: "Aplicacion");

            migrationBuilder.DropColumn(
                name: "TipoTransporte",
                table: "Aplicacion");
        }
    }
}
