using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RedSocial.Infrastructure.Identity.Migrations
{
    /// <inheritdoc />
    public partial class Phone : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PhoneNumberNumber",
                schema: "Identity",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "PhoneNumberNumberConfirmed",
                schema: "Identity",
                table: "Users",
                newName: "PhoneNumberConfirmed");

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                schema: "Identity",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                schema: "Identity",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "PhoneNumberConfirmed",
                schema: "Identity",
                table: "Users",
                newName: "PhoneNumberNumberConfirmed");

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumberNumber",
                schema: "Identity",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
