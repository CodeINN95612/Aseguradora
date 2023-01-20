using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aseguradora.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Aplicaciones : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Aplicacion_Empresa_EmpresaId",
                table: "Aplicacion");

            migrationBuilder.DropForeignKey(
                name: "FK_Aplicacion_Empresa_IdEmpresaAsegurado",
                table: "Aplicacion");

            migrationBuilder.DropForeignKey(
                name: "FK_Aplicacion_Empresa_IdEmpresaPagador",
                table: "Aplicacion");

            migrationBuilder.DropIndex(
                name: "IX_Aplicacion_EmpresaId",
                table: "Aplicacion");

            migrationBuilder.DropColumn(
                name: "EmpresaId",
                table: "Aplicacion");

            migrationBuilder.RenameColumn(
                name: "IdEmpresaPagador",
                table: "Aplicacion",
                newName: "IdUsuario");

            migrationBuilder.RenameColumn(
                name: "IdEmpresaAsegurado",
                table: "Aplicacion",
                newName: "IdEmpresa");

            migrationBuilder.RenameIndex(
                name: "IX_Aplicacion_IdEmpresaPagador",
                table: "Aplicacion",
                newName: "IX_Aplicacion_IdUsuario");

            migrationBuilder.RenameIndex(
                name: "IX_Aplicacion_IdEmpresaAsegurado",
                table: "Aplicacion",
                newName: "IX_Aplicacion_IdEmpresa");

            migrationBuilder.AlterColumn<int>(
                name: "Numero",
                table: "Aplicacion",
                type: "int",
                nullable: false,
                computedColumnSql: "[Id]",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Aplicacion_Numero",
                table: "Aplicacion",
                column: "Numero",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Aplicacion_Empresa_IdEmpresa",
                table: "Aplicacion",
                column: "IdEmpresa",
                principalTable: "Empresa",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Aplicacion_Usuario_IdUsuario",
                table: "Aplicacion",
                column: "IdUsuario",
                principalTable: "Usuario",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Aplicacion_Empresa_IdEmpresa",
                table: "Aplicacion");

            migrationBuilder.DropForeignKey(
                name: "FK_Aplicacion_Usuario_IdUsuario",
                table: "Aplicacion");

            migrationBuilder.DropIndex(
                name: "IX_Aplicacion_Numero",
                table: "Aplicacion");

            migrationBuilder.RenameColumn(
                name: "IdUsuario",
                table: "Aplicacion",
                newName: "IdEmpresaPagador");

            migrationBuilder.RenameColumn(
                name: "IdEmpresa",
                table: "Aplicacion",
                newName: "IdEmpresaAsegurado");

            migrationBuilder.RenameIndex(
                name: "IX_Aplicacion_IdUsuario",
                table: "Aplicacion",
                newName: "IX_Aplicacion_IdEmpresaPagador");

            migrationBuilder.RenameIndex(
                name: "IX_Aplicacion_IdEmpresa",
                table: "Aplicacion",
                newName: "IX_Aplicacion_IdEmpresaAsegurado");

            migrationBuilder.AlterColumn<int>(
                name: "Numero",
                table: "Aplicacion",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldComputedColumnSql: "[Id]");

            migrationBuilder.AddColumn<int>(
                name: "EmpresaId",
                table: "Aplicacion",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Aplicacion_EmpresaId",
                table: "Aplicacion",
                column: "EmpresaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Aplicacion_Empresa_EmpresaId",
                table: "Aplicacion",
                column: "EmpresaId",
                principalTable: "Empresa",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Aplicacion_Empresa_IdEmpresaAsegurado",
                table: "Aplicacion",
                column: "IdEmpresaAsegurado",
                principalTable: "Empresa",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Aplicacion_Empresa_IdEmpresaPagador",
                table: "Aplicacion",
                column: "IdEmpresaPagador",
                principalTable: "Empresa",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
