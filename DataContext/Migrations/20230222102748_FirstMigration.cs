using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataContext.Migrations
{
    /// <inheritdoc />
    public partial class FirstMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Matches_idGame",
                table: "Matches");

            migrationBuilder.CreateIndex(
                name: "IX_Matches_idGame",
                table: "Matches",
                column: "idGame");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Matches_idGame",
                table: "Matches");

            migrationBuilder.CreateIndex(
                name: "IX_Matches_idGame",
                table: "Matches",
                column: "idGame",
                unique: true);
        }
    }
}
