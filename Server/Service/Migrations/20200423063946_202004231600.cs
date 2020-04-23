using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.Migrations
{
    public partial class _202004231600 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CubeDatas",
                columns: table => new
                {
                    CubeId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    AD = table.Column<float>(nullable: false),
                    AS = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CubeDatas", x => x.CubeId);
                });

            migrationBuilder.CreateTable(
                name: "Managers",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Password = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Managers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 60, nullable: false),
                    Name = table.Column<string>(maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cubes",
                columns: table => new
                {
                    CubeId = table.Column<int>(nullable: false),
                    UserId = table.Column<string>(maxLength: 60, nullable: false),
                    Lv = table.Column<byte>(nullable: false),
                    Parts = table.Column<int>(nullable: false),
                    SkillId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cubes", x => new { x.CubeId, x.UserId });
                    table.ForeignKey(
                        name: "FK_Cubes_CubeDatas_CubeId",
                        column: x => x.CubeId,
                        principalTable: "CubeDatas",
                        principalColumn: "CubeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Cubes_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Entries",
                columns: table => new
                {
                    UserId = table.Column<string>(maxLength: 60, nullable: false),
                    Slots = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Entries", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_Entries_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cubes_UserId",
                table: "Cubes",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cubes");

            migrationBuilder.DropTable(
                name: "Entries");

            migrationBuilder.DropTable(
                name: "Managers");

            migrationBuilder.DropTable(
                name: "CubeDatas");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
