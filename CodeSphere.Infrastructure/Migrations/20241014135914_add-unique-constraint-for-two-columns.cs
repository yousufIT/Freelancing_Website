using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CodeSphere.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class adduniqueconstraintfortwocolumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Reviews_ClientId",
                table: "Reviews");

            migrationBuilder.DropIndex(
                name: "IX_Bids_ProjectId",
                table: "Bids");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_ClientId_FreelancerId",
                table: "Reviews",
                columns: new[] { "ClientId", "FreelancerId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Bids_ProjectId_FreelancerId",
                table: "Bids",
                columns: new[] { "ProjectId", "FreelancerId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Reviews_ClientId_FreelancerId",
                table: "Reviews");

            migrationBuilder.DropIndex(
                name: "IX_Bids_ProjectId_FreelancerId",
                table: "Bids");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_ClientId",
                table: "Reviews",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Bids_ProjectId",
                table: "Bids",
                column: "ProjectId");
        }
    }
}
