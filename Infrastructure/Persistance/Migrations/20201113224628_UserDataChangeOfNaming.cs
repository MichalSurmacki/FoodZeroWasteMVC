using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Persistance.Migrations
{
    public partial class UserDataChangeOfNaming : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FavouriteRecipies_UserData_UserId",
                table: "FavouriteRecipies");

            migrationBuilder.DropIndex(
                name: "IX_FavouriteRecipies_UserId",
                table: "FavouriteRecipies");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "FavouriteRecipies");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "UserData",
                maxLength: 250,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UserDataId",
                table: "FavouriteRecipies",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_FavouriteRecipies_UserDataId",
                table: "FavouriteRecipies",
                column: "UserDataId");

            migrationBuilder.AddForeignKey(
                name: "FK_FavouriteRecipies_UserData_UserDataId",
                table: "FavouriteRecipies",
                column: "UserDataId",
                principalTable: "UserData",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FavouriteRecipies_UserData_UserDataId",
                table: "FavouriteRecipies");

            migrationBuilder.DropIndex(
                name: "IX_FavouriteRecipies_UserDataId",
                table: "FavouriteRecipies");

            migrationBuilder.DropColumn(
                name: "UserDataId",
                table: "FavouriteRecipies");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "UserData",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 250);

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "FavouriteRecipies",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_FavouriteRecipies_UserId",
                table: "FavouriteRecipies",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_FavouriteRecipies_UserData_UserId",
                table: "FavouriteRecipies",
                column: "UserId",
                principalTable: "UserData",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
