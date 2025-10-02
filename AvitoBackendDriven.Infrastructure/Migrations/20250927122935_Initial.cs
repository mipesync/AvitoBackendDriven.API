using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace AvitoBackendDriven.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Experiments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                    Description = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: true),
                    Platforms = table.Column<int[]>(type: "integer[]", nullable: false),
                    MinVersion = table.Column<double>(type: "double precision", nullable: true),
                    MaxVersion = table.Column<double>(type: "double precision", nullable: true),
                    TrafficPercentage = table.Column<double>(type: "double precision", nullable: false),
                    Buckets = table.Column<string>(type: "jsonb", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    StartDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    EndDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Experiments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Screens",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Platform = table.Column<int>(type: "integer", nullable: false),
                    MinVersion = table.Column<double>(type: "double precision", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Properties = table.Column<string>(type: "jsonb", nullable: true),
                    States = table.Column<string>(type: "jsonb", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Screens", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Components",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Kind = table.Column<int>(type: "integer", nullable: false),
                    Platform = table.Column<int>(type: "integer", nullable: false),
                    Version = table.Column<double>(type: "double precision", nullable: true),
                    DefaultProperties = table.Column<string>(type: "jsonb", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ValidFrom = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ValidTo = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ScreenId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Components", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Components_Screens_ScreenId",
                        column: x => x.ScreenId,
                        principalTable: "Screens",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Compositions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ScreenId = table.Column<int>(type: "integer", nullable: false),
                    ComponentId = table.Column<int>(type: "integer", nullable: false),
                    OrderIndex = table.Column<int>(type: "integer", nullable: false),
                    Properties = table.Column<string>(type: "jsonb", nullable: false),
                    ParentCompositionId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Compositions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Compositions_Components_ComponentId",
                        column: x => x.ComponentId,
                        principalTable: "Components",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Compositions_Screens_ScreenId",
                        column: x => x.ScreenId,
                        principalTable: "Screens",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UiChangeHistories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ScreenId = table.Column<int>(type: "integer", nullable: false),
                    ComponentId = table.Column<int>(type: "integer", nullable: false),
                    ChangeType = table.Column<int>(type: "integer", nullable: false),
                    ChangeData = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UiChangeHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UiChangeHistories_Components_ComponentId",
                        column: x => x.ComponentId,
                        principalTable: "Components",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UiChangeHistories_Screens_ScreenId",
                        column: x => x.ScreenId,
                        principalTable: "Screens",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Components_Id",
                table: "Components",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Components_Name",
                table: "Components",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Components_ScreenId",
                table: "Components",
                column: "ScreenId");

            migrationBuilder.CreateIndex(
                name: "IX_Compositions_ComponentId",
                table: "Compositions",
                column: "ComponentId");

            migrationBuilder.CreateIndex(
                name: "IX_Compositions_Id",
                table: "Compositions",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Compositions_ScreenId",
                table: "Compositions",
                column: "ScreenId");

            migrationBuilder.CreateIndex(
                name: "IX_Experiments_Id",
                table: "Experiments",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Screens_Id",
                table: "Screens",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Screens_Name",
                table: "Screens",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UiChangeHistories_ComponentId",
                table: "UiChangeHistories",
                column: "ComponentId");

            migrationBuilder.CreateIndex(
                name: "IX_UiChangeHistories_Id",
                table: "UiChangeHistories",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UiChangeHistories_ScreenId",
                table: "UiChangeHistories",
                column: "ScreenId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Compositions");

            migrationBuilder.DropTable(
                name: "Experiments");

            migrationBuilder.DropTable(
                name: "UiChangeHistories");

            migrationBuilder.DropTable(
                name: "Components");

            migrationBuilder.DropTable(
                name: "Screens");
        }
    }
}
