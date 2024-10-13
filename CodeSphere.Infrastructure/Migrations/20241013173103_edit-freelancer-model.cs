using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CodeSphere.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class editfreelancermodel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Hourlysalary",
                table: "AspNetUsers",
                newName: "HourlyRate");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "HourlyRate",
                table: "AspNetUsers",
                newName: "Hourlysalary");
        }
    }
}
