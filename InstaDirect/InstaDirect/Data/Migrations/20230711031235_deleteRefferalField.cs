using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InstaDirect.Data.Migrations
{
    /// <inheritdoc />
    public partial class deleteRefferalField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Referral",
                table: "BotUsers");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "Referral",
                table: "BotUsers",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }
    }
}
