using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mechanico_Api.Migrations
{
    /// <inheritdoc />
    public partial class MechanicStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Password",
                table: "Mechanics",
                newName: "RejectedReason");

            migrationBuilder.AddColumn<string>(
                name: "LicenseImage",
                table: "Mechanics",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Mechanics",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LicenseImage",
                table: "Mechanics");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Mechanics");

            migrationBuilder.RenameColumn(
                name: "RejectedReason",
                table: "Mechanics",
                newName: "Password");
        }
    }
}
