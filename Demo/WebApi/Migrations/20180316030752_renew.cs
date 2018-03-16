using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace WebApi.Migrations
{
    public partial class renew : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GoogleRoads_MapComponents_MapComponentId",
                table: "GoogleRoads");

            migrationBuilder.DropTable(
                name: "MapComponents");

            migrationBuilder.RenameColumn(
                name: "MapComponentId",
                table: "GoogleRoads",
                newName: "MapId");

            migrationBuilder.RenameIndex(
                name: "IX_GoogleRoads_MapComponentId",
                table: "GoogleRoads",
                newName: "IX_GoogleRoads_MapId");

            migrationBuilder.CreateTable(
                name: "CommentIcons",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Descriptions = table.Column<string>(nullable: true),
                    MapId = table.Column<int>(nullable: false),
                    Url = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommentIcons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CommentIcons_Maps_MapId",
                        column: x => x.MapId,
                        principalTable: "Maps",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GoogleRoadIcons",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Descriptions = table.Column<string>(nullable: true),
                    GoogleRoadId = table.Column<int>(nullable: false),
                    Lat = table.Column<double>(nullable: false),
                    Lng = table.Column<double>(nullable: false),
                    Url = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GoogleRoadIcons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GoogleRoadIcons_GoogleRoads_GoogleRoadId",
                        column: x => x.GoogleRoadId,
                        principalTable: "GoogleRoads",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CommentIcons_MapId",
                table: "CommentIcons",
                column: "MapId");

            migrationBuilder.CreateIndex(
                name: "IX_GoogleRoadIcons_GoogleRoadId",
                table: "GoogleRoadIcons",
                column: "GoogleRoadId");

            migrationBuilder.AddForeignKey(
                name: "FK_GoogleRoads_Maps_MapId",
                table: "GoogleRoads",
                column: "MapId",
                principalTable: "Maps",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GoogleRoads_Maps_MapId",
                table: "GoogleRoads");

            migrationBuilder.DropTable(
                name: "CommentIcons");

            migrationBuilder.DropTable(
                name: "GoogleRoadIcons");

            migrationBuilder.RenameColumn(
                name: "MapId",
                table: "GoogleRoads",
                newName: "MapComponentId");

            migrationBuilder.RenameIndex(
                name: "IX_GoogleRoads_MapId",
                table: "GoogleRoads",
                newName: "IX_GoogleRoads_MapComponentId");

            migrationBuilder.CreateTable(
                name: "MapComponents",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    MapId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MapComponents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MapComponents_Maps_MapId",
                        column: x => x.MapId,
                        principalTable: "Maps",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MapComponents_MapId",
                table: "MapComponents",
                column: "MapId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_GoogleRoads_MapComponents_MapComponentId",
                table: "GoogleRoads",
                column: "MapComponentId",
                principalTable: "MapComponents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
