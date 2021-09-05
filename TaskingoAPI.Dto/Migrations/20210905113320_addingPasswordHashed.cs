using Microsoft.EntityFrameworkCore.Migrations;

namespace TaskingoAPI.Database.Migrations
{
    public partial class addingPasswordHashed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PasswordHashed",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PasswordHashed",
                table: "Users");
        }
    }
}
