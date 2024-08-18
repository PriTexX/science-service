using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Science.Migrations
{
    /// <inheritdoc />
    public partial class more_strings : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "Articles",
                type: "character varying(26)",
                maxLength: 26,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(20,0)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Id",
                table: "Articles",
                type: "numeric(20,0)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(26)",
                oldMaxLength: 26);
        }
    }
}
