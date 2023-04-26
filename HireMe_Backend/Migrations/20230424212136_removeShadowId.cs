using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HireMe_Backend.Migrations
{
    /// <inheritdoc />
    public partial class removeShadowId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_offers_AspNetUsers_UserId1",
                table: "offers");

            migrationBuilder.DropIndex(
                name: "IX_offers_UserId1",
                table: "offers");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "offers");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "offers",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.CreateIndex(
                name: "IX_offers_UserId",
                table: "offers",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_offers_AspNetUsers_UserId",
                table: "offers",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_offers_AspNetUsers_UserId",
                table: "offers");

            migrationBuilder.DropIndex(
                name: "IX_offers_UserId",
                table: "offers");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "offers",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "offers",
                type: "nvarchar(450)",
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
    }
}
