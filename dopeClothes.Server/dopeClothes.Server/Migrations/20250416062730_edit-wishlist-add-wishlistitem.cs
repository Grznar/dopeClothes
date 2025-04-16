using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace dopeClothes.Server.Migrations
{
    /// <inheritdoc />
    public partial class editwishlistaddwishlistitem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_WhishLists_WhishListId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_WhishListId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "WhishListId",
                table: "Products");

            migrationBuilder.CreateTable(
                name: "WishListItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WishListId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WishListItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WishListItems_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WishListItems_WhishLists_WishListId",
                        column: x => x.WishListId,
                        principalTable: "WhishLists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WishListItems_ProductId",
                table: "WishListItems",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_WishListItems_WishListId",
                table: "WishListItems",
                column: "WishListId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WishListItems");

            migrationBuilder.AddColumn<int>(
                name: "WhishListId",
                table: "Products",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_WhishListId",
                table: "Products",
                column: "WhishListId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_WhishLists_WhishListId",
                table: "Products",
                column: "WhishListId",
                principalTable: "WhishLists",
                principalColumn: "Id");
        }
    }
}
