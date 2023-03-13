using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Teko.Diary.Data.Migrations
{
    public partial class AddDiaryForeign : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Entry_Diary_DiaryId",
                table: "Entry");

            migrationBuilder.AlterColumn<int>(
                name: "DiaryId",
                table: "Entry",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Entry_Diary_DiaryId",
                table: "Entry",
                column: "DiaryId",
                principalTable: "Diary",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Entry_Diary_DiaryId",
                table: "Entry");

            migrationBuilder.AlterColumn<int>(
                name: "DiaryId",
                table: "Entry",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Entry_Diary_DiaryId",
                table: "Entry",
                column: "DiaryId",
                principalTable: "Diary",
                principalColumn: "Id");
        }
    }
}
