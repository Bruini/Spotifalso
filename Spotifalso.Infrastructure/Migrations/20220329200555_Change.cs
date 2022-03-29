using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Spotifalso.Infrastructure.Migrations
{
    public partial class Change : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ArtistMusic");

            migrationBuilder.AddColumn<Guid>(
                name: "MusicId",
                table: "Artists",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.CreateIndex(
                name: "IX_Artists_MusicId",
                table: "Artists",
                column: "MusicId");

            migrationBuilder.AddForeignKey(
                name: "FK_Artists_Musics_MusicId",
                table: "Artists",
                column: "MusicId",
                principalTable: "Musics",
                principalColumn: "MusicID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Artists_Musics_MusicId",
                table: "Artists");

            migrationBuilder.DropIndex(
                name: "IX_Artists_MusicId",
                table: "Artists");

            migrationBuilder.DropColumn(
                name: "MusicId",
                table: "Artists");

            migrationBuilder.CreateTable(
                name: "ArtistMusic",
                columns: table => new
                {
                    ArtistsId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    MusicsId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArtistMusic", x => new { x.ArtistsId, x.MusicsId });
                    table.ForeignKey(
                        name: "FK_ArtistMusic_Artists_ArtistsId",
                        column: x => x.ArtistsId,
                        principalTable: "Artists",
                        principalColumn: "ArtistID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ArtistMusic_Musics_MusicsId",
                        column: x => x.MusicsId,
                        principalTable: "Musics",
                        principalColumn: "MusicID",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_ArtistMusic_MusicsId",
                table: "ArtistMusic",
                column: "MusicsId");
        }
    }
}
