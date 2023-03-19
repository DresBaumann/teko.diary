using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Teko.Diary.Migrations
{
    public partial class tagchange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EntryId",
                table: "Tag",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tag_EntryId",
                table: "Tag",
                column: "EntryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tag_Entry_EntryId",
                table: "Tag",
                column: "EntryId",
                principalTable: "Entry",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tag_Entry_EntryId",
                table: "Tag");

            migrationBuilder.DropIndex(
                name: "IX_Tag_EntryId",
                table: "Tag");

            migrationBuilder.DropColumn(
                name: "EntryId",
                table: "Tag");
        }
    }
}
