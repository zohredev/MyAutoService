using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyAutoService.Data.Migrations
{
    /// <inheritdoc />
    public partial class newMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ShoppingCartId",
                table: "ServicesShoppingCarts",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ServicesShoppingCarts_ShoppingCartId",
                table: "ServicesShoppingCarts",
                column: "ShoppingCartId");

            migrationBuilder.AddForeignKey(
                name: "FK_ServicesShoppingCarts_Cars_ShoppingCartId",
                table: "ServicesShoppingCarts",
                column: "ShoppingCartId",
                principalTable: "Cars",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ServicesShoppingCarts_Cars_ShoppingCartId",
                table: "ServicesShoppingCarts");

            migrationBuilder.DropIndex(
                name: "IX_ServicesShoppingCarts_ShoppingCartId",
                table: "ServicesShoppingCarts");

            migrationBuilder.DropColumn(
                name: "ShoppingCartId",
                table: "ServicesShoppingCarts");
        }
    }
}
