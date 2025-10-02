using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AvitoBackendDriven.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Changes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Compositions_Components_ComponentId",
                table: "Compositions");

            migrationBuilder.DropIndex(
                name: "IX_Compositions_ComponentId",
                table: "Compositions");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Compositions_ComponentId",
                table: "Compositions",
                column: "ComponentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Compositions_Components_ComponentId",
                table: "Compositions",
                column: "ComponentId",
                principalTable: "Components",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
