using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace dopeClothes.Server.Migrations
{
    /// <inheritdoc />
    public partial class addcategorytoproductupdatetypo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Categeory",
                table: "Products",
                newName: "Category");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Category",
                table: "Products",
                newName: "Categeory");
        }
    }
}
