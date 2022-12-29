using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class addedTodoHeader : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Todo",
                table: "TodoDatas",
                newName: "TodoHeader");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "TodoDatas",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "TodoContent",
                table: "TodoDatas",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TodoContent",
                table: "TodoDatas");

            migrationBuilder.RenameColumn(
                name: "TodoHeader",
                table: "TodoDatas",
                newName: "Todo");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "TodoDatas",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}
