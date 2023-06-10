using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SimbaBot.Data.Migrations
{
    /// <inheritdoc />
    public partial class Inviter : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "Inviter",
                table: "BotUsers",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Inviter",
                table: "BotUsers");
        }
    }
}
