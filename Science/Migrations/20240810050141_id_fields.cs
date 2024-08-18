using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Science.Migrations
{
    /// <inheritdoc />
    public partial class id_fields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ScopusId",
                table: "Articles",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WoSId",
                table: "Articles",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ScopusId",
                table: "Articles");

            migrationBuilder.DropColumn(
                name: "WoSId",
                table: "Articles");
        }
    }
}
