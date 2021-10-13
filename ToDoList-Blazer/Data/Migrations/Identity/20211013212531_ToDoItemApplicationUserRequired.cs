using Microsoft.EntityFrameworkCore.Migrations;

namespace ToDoList_Blazer.Data.Migrations
{
    public partial class ToDoItemApplicationUserRequired : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ToDoItem_AspNetUsers_ApplicationUserId",
                table: "ToDoItem");

            migrationBuilder.AlterColumn<string>(
                name: "ApplicationUserId",
                table: "ToDoItem",
                type: "nvarchar(450)",
                maxLength: 450,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldMaxLength: 450,
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ToDoItem_AspNetUsers_ApplicationUserId",
                table: "ToDoItem",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ToDoItem_AspNetUsers_ApplicationUserId",
                table: "ToDoItem");

            migrationBuilder.AlterColumn<string>(
                name: "ApplicationUserId",
                table: "ToDoItem",
                type: "nvarchar(450)",
                maxLength: 450,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldMaxLength: 450);

            migrationBuilder.AddForeignKey(
                name: "FK_ToDoItem_AspNetUsers_ApplicationUserId",
                table: "ToDoItem",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
