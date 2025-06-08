using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MindFactory.News.Infraestructure.Migrations
{
    /// <inheritdoc />
    public partial class create_entities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "admin");
            
            migrationBuilder.CreateTable(
                name: "__EFMigrationsHistory",
                schema: "admin",
                columns: table => new
                {
                    MigrationId = table.Column<string>(nullable: false),
                    ProductVersion = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK___EFMigrationsHistory", x => x.MigrationId);
                });

            migrationBuilder.EnsureSchema(
                name: "news");

            migrationBuilder.CreateTable(
                name: "Authors",
                schema: "news",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    CreatedUserId = table.Column<int>(type: "integer", nullable: false),
                    CreatedDateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "(now())::timestamp without time zone"),
                    UpdatedUserId = table.Column<int>(type: "integer", nullable: true),
                    UpdatedDateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Enabled = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Author", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Editorials",
                schema: "news",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    PublishDate = table.Column<DateOnly>(type: "date", nullable: false),
                    CreatedUserId = table.Column<int>(type: "integer", nullable: false),
                    CreatedDateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "(now())::timestamp without time zone"),
                    UpdatedUserId = table.Column<int>(type: "integer", nullable: true),
                    UpdatedDateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Enabled = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Editorial", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NewsItems",
                schema: "news",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Body = table.Column<string>(type: "text", nullable: false),
                    PublishDate = table.Column<DateOnly>(type: "date", nullable: false),
                    ImageUrl = table.Column<string>(type: "text", nullable: false),
                    AuthorId = table.Column<int>(type: "integer", nullable: false),
                    EditorialId = table.Column<int>(type: "integer", nullable: true),
                    CreatedUserId = table.Column<int>(type: "integer", nullable: false),
                    CreatedDateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "(now())::timestamp without time zone"),
                    UpdatedUserId = table.Column<int>(type: "integer", nullable: true),
                    UpdatedDateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Enabled = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NewsItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NewsItem_AuthorId",
                        column: x => x.AuthorId,
                        principalSchema: "news",
                        principalTable: "Authors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NewsItem_EditorialId",
                        column: x => x.EditorialId,
                        principalSchema: "news",
                        principalTable: "Editorials",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateIndex(
                name: "IX_NewsItems_AuthorId",
                schema: "news",
                table: "NewsItems",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_NewsItems_EditorialId",
                schema: "news",
                table: "NewsItems",
                column: "EditorialId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NewsItems",
                schema: "news");

            migrationBuilder.DropTable(
                name: "Authors",
                schema: "news");

            migrationBuilder.DropTable(
                name: "Editorials",
                schema: "news");
        }
    }
}
