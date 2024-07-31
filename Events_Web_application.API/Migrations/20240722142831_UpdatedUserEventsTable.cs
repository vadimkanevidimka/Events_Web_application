using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Events_Web_application.API.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedUserEventsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Category",
                table: "Events");

            migrationBuilder.AddColumn<Guid>(
                name: "AsscesTokenId",
                table: "Users",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CategoryId",
                table: "Events",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "AccesTokens",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Token = table.Column<string>(type: "TEXT", nullable: false),
                    ExpirationJWTDateTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    RefreshToken = table.Column<string>(type: "TEXT", nullable: false),
                    ExpirationRTDateTime = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccesTokens", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VentCategories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VentCategories", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_AsscesTokenId",
                table: "Users",
                column: "AsscesTokenId");

            migrationBuilder.CreateIndex(
                name: "IX_Events_CategoryId",
                table: "Events",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Events_VentCategories_CategoryId",
                table: "Events",
                column: "CategoryId",
                principalTable: "VentCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_AccesTokens_AsscesTokenId",
                table: "Users",
                column: "AsscesTokenId",
                principalTable: "AccesTokens",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Events_VentCategories_CategoryId",
                table: "Events");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_AccesTokens_AsscesTokenId",
                table: "Users");

            migrationBuilder.DropTable(
                name: "AccesTokens");

            migrationBuilder.DropTable(
                name: "VentCategories");

            migrationBuilder.DropIndex(
                name: "IX_Users_AsscesTokenId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Events_CategoryId",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "AsscesTokenId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Events");

            migrationBuilder.AddColumn<string>(
                name: "Category",
                table: "Events",
                type: "TEXT",
                maxLength: 100,
                nullable: false,
                defaultValue: "");
        }
    }
}
