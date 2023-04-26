using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HireMe_Backend.Migrations
{
    /// <inheritdoc />
    public partial class oneToManyUserToOffers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "prize",
                table: "offers",
                newName: "Prize");

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "offers",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "offers",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_offers_UserId1",
                table: "offers",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_offers_AspNetUsers_UserId1",
                table: "offers",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_offers_AspNetUsers_UserId1",
                table: "offers");

            migrationBuilder.DropIndex(
                name: "IX_offers_UserId1",
                table: "offers");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "offers");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "offers");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "Prize",
                table: "offers",
                newName: "prize");
        }
    }
}
