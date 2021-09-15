using Microsoft.EntityFrameworkCore.Migrations;

namespace TaskingoAPI.Database.Migrations
{
    public partial class AddingWorkGroupForTask : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "WorkGroupId",
                table: "WorkTasks",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_WorkTasks_WorkGroupId",
                table: "WorkTasks",
                column: "WorkGroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkTasks_Roles_WorkGroupId",
                table: "WorkTasks",
                column: "WorkGroupId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkTasks_Roles_WorkGroupId",
                table: "WorkTasks");

            migrationBuilder.DropIndex(
                name: "IX_WorkTasks_WorkGroupId",
                table: "WorkTasks");

            migrationBuilder.DropColumn(
                name: "WorkGroupId",
                table: "WorkTasks");
        }
    }
}
