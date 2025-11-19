using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication1.Migrations
{
    /// <inheritdoc />
    public partial class UpdateModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PlayerMonsterKills",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    PlayerId = table.Column<string>(type: "TEXT", nullable: true),
                    MonsterId = table.Column<string>(type: "TEXT", nullable: true),
                    KilledAt = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerMonsterKills", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlayerMonsterKills_Monsters_MonsterId",
                        column: x => x.MonsterId,
                        principalTable: "Monsters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_PlayerMonsterKills_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PlayerMonsterKills_MonsterId",
                table: "PlayerMonsterKills",
                column: "MonsterId");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerMonsterKills_PlayerId",
                table: "PlayerMonsterKills",
                column: "PlayerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PlayerMonsterKills");
        }
    }
}
