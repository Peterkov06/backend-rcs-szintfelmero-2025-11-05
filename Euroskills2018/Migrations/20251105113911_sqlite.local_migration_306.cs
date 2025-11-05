using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Euroskills2018.Migrations
{
    /// <inheritdoc />
    public partial class sqlitelocal_migration_306 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Orszagok",
                columns: table => new
                {
                    id = table.Column<string>(type: "TEXT", nullable: false),
                    orszagNev = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orszagok", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Szakmak",
                columns: table => new
                {
                    id = table.Column<string>(type: "TEXT", nullable: false),
                    szakmaNev = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Szakmak", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Versenyzok",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    nev = table.Column<string>(type: "TEXT", nullable: false),
                    szakmaId = table.Column<string>(type: "TEXT", nullable: false),
                    orszagId = table.Column<string>(type: "TEXT", nullable: false),
                    pont = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Versenyzok", x => x.id);
                    table.ForeignKey(
                        name: "FK_Versenyzok_Orszagok_orszagId",
                        column: x => x.orszagId,
                        principalTable: "Orszagok",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Versenyzok_Szakmak_szakmaId",
                        column: x => x.szakmaId,
                        principalTable: "Szakmak",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Versenyzok_orszagId",
                table: "Versenyzok",
                column: "orszagId");

            migrationBuilder.CreateIndex(
                name: "IX_Versenyzok_szakmaId",
                table: "Versenyzok",
                column: "szakmaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Versenyzok");

            migrationBuilder.DropTable(
                name: "Orszagok");

            migrationBuilder.DropTable(
                name: "Szakmak");
        }
    }
}
