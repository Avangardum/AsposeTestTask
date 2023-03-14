using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Avangardum.AsposeTestTask.Data.Migrations
{
    /// <inheritdoc />
    public partial class ReplacedPostAuthorIdWithName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AuthorId",
                table: "Posts",
                newName: "AuthorName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AuthorName",
                table: "Posts",
                newName: "AuthorId");
        }
    }
}
