using Microsoft.EntityFrameworkCore.Migrations;

namespace TaskingoAPI.Database.Migrations
{
    public partial class AddingChat : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ActualStatus",
                table: "Users",
                newName: "UserIdChat");

            migrationBuilder.AddColumn<bool>(
                name: "IsOnline",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "Messages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WhoSentMessageId = table.Column<int>(type: "int", nullable: true),
                    WhoGotMessageId = table.Column<int>(type: "int", nullable: true),
                    UserMessage = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Messages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Messages_Users_WhoGotMessageId",
                        column: x => x.WhoGotMessageId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Messages_Users_WhoSentMessageId",
                        column: x => x.WhoSentMessageId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Messages_WhoGotMessageId",
                table: "Messages",
                column: "WhoGotMessageId");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_WhoSentMessageId",
                table: "Messages",
                column: "WhoSentMessageId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Messages");

            migrationBuilder.DropColumn(
                name: "IsOnline",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "UserIdChat",
                table: "Users",
                newName: "ActualStatus");
        }
    }
}
