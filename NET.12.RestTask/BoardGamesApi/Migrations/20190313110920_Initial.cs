using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BoardGamesApi.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BoardGames",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Created = table.Column<DateTime>(nullable: false),
                    Modified = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BoardGames", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "BoardGames",
                columns: new[] { "Id", "Created", "Description", "Modified", "Name" },
                values: new object[,]
                {
                    { 1, new DateTime(2019, 3, 13, 14, 9, 7, 305, DateTimeKind.Local), "The classic board games like Monopoly.", new DateTime(2019, 3, 13, 11, 9, 7, 305, DateTimeKind.Utc), "Wingspan" },
                    { 2, new DateTime(2019, 3, 13, 14, 9, 7, 306, DateTimeKind.Local), "The best asymmetric strategy board game of the decade.", new DateTime(2019, 3, 13, 11, 9, 7, 306, DateTimeKind.Utc), "Root" },
                    { 3, new DateTime(2019, 3, 13, 14, 9, 7, 306, DateTimeKind.Local), "Exceptional deck-building word game.", new DateTime(2019, 3, 13, 11, 9, 7, 306, DateTimeKind.Utc), "Hardback" },
                    { 4, new DateTime(2019, 3, 13, 14, 9, 7, 306, DateTimeKind.Local), "The brilliant abstract game for two to five players.", new DateTime(2019, 3, 13, 11, 9, 7, 306, DateTimeKind.Utc), "Azul" },
                    { 5, new DateTime(2019, 3, 13, 14, 9, 7, 306, DateTimeKind.Local), "This is basically Jurassic Park: The Game, in all its '80s glory.", new DateTime(2019, 3, 13, 11, 9, 7, 306, DateTimeKind.Utc), "Dinosaur Island" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BoardGames");
        }
    }
}
