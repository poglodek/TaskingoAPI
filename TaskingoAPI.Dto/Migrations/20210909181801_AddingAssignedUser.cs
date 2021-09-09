using Microsoft.EntityFrameworkCore.Migrations;

namespace TaskingoAPI.Database.Migrations
{
    public partial class AddingAssignedUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AssignedUserId",
                table: "WorkTasks",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_WorkTasks_AssignedUserId",
                table: "WorkTasks",
                column: "AssignedUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkTasks_Users_AssignedUserId",
                table: "WorkTasks",
                column: "AssignedUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkTasks_Users_AssignedUserId",
                table: "WorkTasks");

            migrationBuilder.DropIndex(
                name: "IX_WorkTasks_AssignedUserId",
                table: "WorkTasks");

            migrationBuilder.DropColumn(
                name: "AssignedUserId",
                table: "WorkTasks");
        }
    }
}
