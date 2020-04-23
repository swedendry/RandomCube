using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.Migrations
{
    public partial class _202004231700 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SkillId",
                table: "Cubes");

            migrationBuilder.AddColumn<int>(
                name: "Money",
                table: "Users",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SkillId",
                table: "CubeDatas",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "SkillDatas",
                columns: table => new
                {
                    SkillId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Percent = table.Column<float>(nullable: false),
                    Duration = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SkillDatas", x => x.SkillId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SkillDatas");

            migrationBuilder.DropColumn(
                name: "Money",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "SkillId",
                table: "CubeDatas");

            migrationBuilder.AddColumn<int>(
                name: "SkillId",
                table: "Cubes",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
