using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Events_Web_application.API.Migrations
{
    /// <inheritdoc />
    public partial class Added_ID_To_UsersEventsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_UsersEvents",
                table: "UsersEvents");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "UsersEvents",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_UsersEvents",
                table: "UsersEvents",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_UsersEvents_EventId",
                table: "UsersEvents",
                column: "EventId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_UsersEvents",
                table: "UsersEvents");

            migrationBuilder.DropIndex(
                name: "IX_UsersEvents_EventId",
                table: "UsersEvents");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "UsersEvents");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UsersEvents",
                table: "UsersEvents",
                columns: new[] { "EventId", "ParticipantId" });
        }
    }
}
