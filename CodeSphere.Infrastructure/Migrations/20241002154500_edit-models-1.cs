using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CodeSphere.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class editmodels1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Skills_Freelancers_FreelancerId",
                table: "Skills");

            migrationBuilder.DropIndex(
                name: "IX_Skills_FreelancerId",
                table: "Skills");

            migrationBuilder.DropColumn(
                name: "FreelancerId",
                table: "Skills");

            migrationBuilder.RenameColumn(
                name: "HourlyRate",
                table: "Freelancers",
                newName: "Hourlysalary");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Hourlysalary",
                table: "Freelancers",
                newName: "HourlyRate");

            migrationBuilder.AddColumn<int>(
                name: "FreelancerId",
                table: "Skills",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Skills_FreelancerId",
                table: "Skills",
                column: "FreelancerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Skills_Freelancers_FreelancerId",
                table: "Skills",
                column: "FreelancerId",
                principalTable: "Freelancers",
                principalColumn: "Id");
        }
    }
}
