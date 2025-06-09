using Microsoft.EntityFrameworkCore.Migrations;
using NpgsqlTypes;

#nullable disable

namespace MindFactory.News.Infraestructure.Migrations
{
    /// <inheritdoc />
    public partial class search_vector : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<NpgsqlTsVector>(
                name: "SearchVector",
                schema: "news",
                table: "NewsItems",
                type: "tsvector",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_NewsItems_SearchVector",
                schema: "news",
                table: "NewsItems",
                column: "SearchVector")
                .Annotation("Npgsql:IndexMethod", "GIN");
            
            migrationBuilder.Sql(@"
                CREATE FUNCTION update_search_vector() RETURNS trigger AS $$
                BEGIN
                    NEW.""SearchVector"" :=
                        to_tsvector('spanish', coalesce(NEW.""Title"", '') || ' ' ||
                                                coalesce((SELECT ""Name"" FROM ""Authors"" WHERE ""Id"" = NEW.""AuthorId""), ''));
                    RETURN NEW;
                END
                $$ LANGUAGE plpgsql;

                CREATE TRIGGER trg_update_search_vector
                BEFORE INSERT OR UPDATE ON ""NewsItems""
                FOR EACH ROW EXECUTE FUNCTION update_search_vector();

                -- Inicializar valores existentes
                UPDATE ""NewsItems"" SET ""SearchVector"" = 
                    to_tsvector('spanish', coalesce(""Title"", '') || ' ' ||
                                        coalesce((SELECT ""Name"" FROM ""Authors"" WHERE ""Id"" = ""AuthorId""), ''));
                
                CREATE FUNCTION update_related_newsitems_search_vector() RETURNS trigger AS $$
                BEGIN
                    UPDATE ""NewsItems""
                    SET ""SearchVector"" = to_tsvector('spanish',
                        coalesce(""Title"", '') || ' ' ||
                        coalesce((SELECT ""Name"" FROM ""Authors"" WHERE ""Id"" = ""AuthorId""), '')
                    )
                    WHERE ""AuthorId"" = NEW.""Id"";
                    RETURN NEW;
                END;
                $$ LANGUAGE plpgsql;

                CREATE TRIGGER trg_update_newsitems_on_author_change
                AFTER UPDATE OF ""Name"" ON ""Authors""
                FOR EACH ROW
                EXECUTE FUNCTION update_related_newsitems_search_vector();
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_NewsItems_SearchVector",
                schema: "news",
                table: "NewsItems");

            migrationBuilder.DropColumn(
                name: "SearchVector",
                schema: "news",
                table: "NewsItems");
        }
    }
}
