using Microsoft.EntityFrameworkCore.Migrations;
using Science.DB;

#nullable disable

namespace Science.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AdminUsers",
                columns: table => new
                {
                    Guid = table.Column<string>(type: "character varying(36)", maxLength: 36, nullable: false),
                    FullName = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdminUsers", x => x.Guid);
                });

            migrationBuilder.CreateTable(
                name: "Articles",
                columns: table => new
                {
                    Id = table.Column<decimal>(type: "numeric(20,0)", nullable: false),
                    DOI = table.Column<string>(type: "text", nullable: false),
                    Authors = table.Column<string>(type: "text", nullable: false),
                    ArticleTitle = table.Column<string>(type: "text", nullable: false),
                    SourceTitle = table.Column<string>(type: "text", nullable: false),
                    DocumentType = table.Column<string>(type: "text", nullable: false),
                    PublicationYear = table.Column<int>(type: "integer", nullable: false),
                    IsWoS = table.Column<bool>(type: "boolean", nullable: false),
                    IsScopus = table.Column<bool>(type: "boolean", nullable: false),
                    WoSData = table.Column<WoSData>(type: "jsonb", nullable: true),
                    ScopusData = table.Column<ScopusData>(type: "jsonb", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Articles", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AdminUsers");

            migrationBuilder.DropTable(
                name: "Articles");
        }
    }
}
