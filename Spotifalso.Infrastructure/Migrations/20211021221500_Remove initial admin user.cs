using Microsoft.EntityFrameworkCore.Migrations;

namespace Spotifalso.Infrastructure.Migrations
{
    public partial class Removeinitialadminuser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: "8ab3a3f4-bfce-4b7f-bee1-260ef209a754");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserID", "Bio", "Nickname", "Password", "ProfilePhotoId", "Role" },
                values: new object[] { "8ab3a3f4-bfce-4b7f-bee1-260ef209a754", "Initial Admin user", "admin", "admin", "", "Admin" });
        }
    }
}
