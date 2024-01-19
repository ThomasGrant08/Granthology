using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Granthology.Data.Migrations
{
    /// <inheritdoc />
    public partial class Partners : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RelationshipType",
                table: "Relationships",
                newName: "RelationshipTypeDiscriminator");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RelationshipTypeDiscriminator",
                table: "Relationships",
                newName: "RelationshipType");
        }
    }
}
