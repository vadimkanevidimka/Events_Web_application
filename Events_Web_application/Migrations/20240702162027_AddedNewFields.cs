using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Events_Web_application.API.Migrations
{
    /// <inheritdoc />
    public partial class AddedNewFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "ParticipantStatus",
                table: "UsersEvents",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "NameOfHost",
                table: "Events",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ParticipantStatus",
                table: "UsersEvents");

            migrationBuilder.DropColumn(
                name: "NameOfHost",
                table: "Events");
        }
    }
}
