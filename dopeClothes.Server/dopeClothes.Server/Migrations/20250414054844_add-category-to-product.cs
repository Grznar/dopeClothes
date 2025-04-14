using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace dopeClothes.Server.Migrations
{
    /// <inheritdoc />
    public partial class addcategorytoproduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Categeory",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Categeory",
                table: "Products");
        }
    }
}
