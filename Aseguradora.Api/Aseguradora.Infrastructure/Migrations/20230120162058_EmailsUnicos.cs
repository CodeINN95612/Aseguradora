using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aseguradora.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class EmailsUnicos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Usuario",
                table: "Usuario",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Usuario",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Empresa",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "RUC",
                table: "Empresa",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Usuario",
                keyColumn: "Id",
                keyValue: 1,
                column: "Email",
                value: "admin@email.com");

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_Email",
                table: "Usuario",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_Usuario",
                table: "Usuario",
                column: "Usuario",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Empresa_Email",
                table: "Empresa",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Empresa_RUC",
                table: "Empresa",
                column: "RUC",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Usuario_Email",
                table: "Usuario");

            migrationBuilder.DropIndex(
                name: "IX_Usuario_Usuario",
                table: "Usuario");

            migrationBuilder.DropIndex(
                name: "IX_Empresa_Email",
                table: "Empresa");

            migrationBuilder.DropIndex(
                name: "IX_Empresa_RUC",
                table: "Empresa");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Usuario");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Empresa");

            migrationBuilder.DropColumn(
                name: "RUC",
                table: "Empresa");

            migrationBuilder.AlterColumn<string>(
                name: "Usuario",
                table: "Usuario",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}
