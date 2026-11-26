using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Nuance.API.Migrations
{
    /// <inheritdoc />
    public partial class AddWardrobeRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "WardrobeId",
                table: "ClothingItems",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Wardrobes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wardrobes", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClothingItems_WardrobeId",
                table: "ClothingItems",
                column: "WardrobeId");

            migrationBuilder.AddForeignKey(
                name: "FK_ClothingItems_Wardrobes_WardrobeId",
                table: "ClothingItems",
                column: "WardrobeId",
                principalTable: "Wardrobes",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClothingItems_Wardrobes_WardrobeId",
                table: "ClothingItems");

            migrationBuilder.DropTable(
                name: "Wardrobes");

            migrationBuilder.DropIndex(
                name: "IX_ClothingItems_WardrobeId",
                table: "ClothingItems");

            migrationBuilder.DropColumn(
                name: "WardrobeId",
                table: "ClothingItems");
        }
    }
}
