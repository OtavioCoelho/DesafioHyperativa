using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DesafioHyperativa.Repository.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    DtRegister = table.Column<string>(type: "TEXT", nullable: false),
                    DtUpdate = table.Column<string>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    Password = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Lot",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    DtRegister = table.Column<string>(type: "TEXT", nullable: false),
                    DtUpdate = table.Column<string>(type: "TEXT", nullable: false),
                    UserId = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    DateOperation = table.Column<string>(type: "TEXT", nullable: false),
                    LotNumber = table.Column<string>(type: "TEXT", nullable: false),
                    NumberRecords = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lot", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Lot_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Card",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    DtRegister = table.Column<string>(type: "TEXT", nullable: false),
                    DtUpdate = table.Column<string>(type: "TEXT", nullable: false),
                    UserId = table.Column<string>(type: "TEXT", nullable: false),
                    LineIdentifier = table.Column<string>(type: "TEXT", nullable: true),
                    LotNumber = table.Column<string>(type: "TEXT", nullable: true),
                    Number = table.Column<string>(type: "TEXT", nullable: false),
                    LotId = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Card", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Card_Lot_LotId",
                        column: x => x.LotId,
                        principalTable: "Lot",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Card_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "DtRegister", "DtUpdate", "Email", "Password" },
                values: new object[,]
                {
                    { "bc63d576-0408-4270-b31e-b4c1541ea03d", "jfTVy2K5abZuEsqsYFn3JLQLRuJeTPYH", "jfTVy2K5abZuEsqsYFn3JLQLRuJeTPYH", "KkXGkbDeAzkQ7EydVqVR1w==", "7evzsDzFFEc+yYUCM4RjiQ==" },
                    { "ce07edbb-721d-4693-a0f2-cd01fe469acf", "jfTVy2K5abZuEsqsYFn3JLQLRuJeTPYH", "jfTVy2K5abZuEsqsYFn3JLQLRuJeTPYH", "UNRb5m4m+nUQ7EydVqVR1w==", "O9yO1laZW/c+yYUCM4RjiQ==" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Card_LotId",
                table: "Card",
                column: "LotId");

            migrationBuilder.CreateIndex(
                name: "IX_Card_Number",
                table: "Card",
                column: "Number",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Card_UserId",
                table: "Card",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Lot_Name_DateOperation_LotNumber",
                table: "Lot",
                columns: new[] { "Name", "DateOperation", "LotNumber" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Lot_UserId",
                table: "Lot",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_User_Email",
                table: "User",
                column: "Email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Card");

            migrationBuilder.DropTable(
                name: "Lot");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
