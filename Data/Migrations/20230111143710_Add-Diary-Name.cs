using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Teko.Diary.Data.Migrations
{
    public partial class AddDiaryName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Diary",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Diary");
        }
    }
}
