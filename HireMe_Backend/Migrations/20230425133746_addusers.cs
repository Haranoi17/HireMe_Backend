using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HireMe_Backend.Migrations
{
    /// <inheritdoc />
    public partial class addusers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "offers",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_offers_ApplicationUserId",
                table: "offers",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_offers_AspNetUsers_ApplicationUserId",
                table: "offers",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_offers_AspNetUsers_ApplicationUserId",
                table: "offers");

            migrationBuilder.DropIndex(
                name: "IX_offers_ApplicationUserId",
                table: "offers");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "offers");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUsers");
        }
    }
}
