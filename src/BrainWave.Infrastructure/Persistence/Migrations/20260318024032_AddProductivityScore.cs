using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BrainWave.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddProductivityScore : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "ProductivityScore",
                table: "Users",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductivityScore",
                table: "Users");
        }
    }
}
