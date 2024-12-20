using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EventRegistration.Migrations
{
    /// <inheritdoc />
    public partial class AddCreatorIdToEvents : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDrafted",
                table: "Events",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDrafted",
                table: "Events");
        }
    }
}
