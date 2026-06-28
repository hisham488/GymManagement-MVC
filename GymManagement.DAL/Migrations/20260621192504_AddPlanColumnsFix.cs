using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GymManagement.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddPlanColumnsFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Plans",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Plans",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Plans");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Plans");
        }
    }
}
