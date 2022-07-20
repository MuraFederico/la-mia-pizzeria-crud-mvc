using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace la_mia_pizzeria_static.Migrations
{
    public partial class category : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IngredientPizza_Ingredient_ingredientsId",
                table: "IngredientPizza");

            migrationBuilder.DropPrimaryKey(
                name: "PK_IngredientPizza",
                table: "IngredientPizza");

            migrationBuilder.DropIndex(
                name: "IX_IngredientPizza_ingredientsId",
                table: "IngredientPizza");

            migrationBuilder.RenameColumn(
                name: "ingredientsId",
                table: "IngredientPizza",
                newName: "IngredientsId");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Pizza",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "Pizza",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_IngredientPizza",
                table: "IngredientPizza",
                columns: new[] { "IngredientsId", "PizzasId" });

            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Pizza_CategoryId",
                table: "Pizza",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_IngredientPizza_PizzasId",
                table: "IngredientPizza",
                column: "PizzasId");

            migrationBuilder.AddForeignKey(
                name: "FK_IngredientPizza_Ingredient_IngredientsId",
                table: "IngredientPizza",
                column: "IngredientsId",
                principalTable: "Ingredient",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Pizza_Category_CategoryId",
                table: "Pizza",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IngredientPizza_Ingredient_IngredientsId",
                table: "IngredientPizza");

            migrationBuilder.DropForeignKey(
                name: "FK_Pizza_Category_CategoryId",
                table: "Pizza");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropIndex(
                name: "IX_Pizza_CategoryId",
                table: "Pizza");

            migrationBuilder.DropPrimaryKey(
                name: "PK_IngredientPizza",
                table: "IngredientPizza");

            migrationBuilder.DropIndex(
                name: "IX_IngredientPizza_PizzasId",
                table: "IngredientPizza");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Pizza");

            migrationBuilder.RenameColumn(
                name: "IngredientsId",
                table: "IngredientPizza",
                newName: "ingredientsId");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Pizza",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AddPrimaryKey(
                name: "PK_IngredientPizza",
                table: "IngredientPizza",
                columns: new[] { "PizzasId", "ingredientsId" });

            migrationBuilder.CreateIndex(
                name: "IX_IngredientPizza_ingredientsId",
                table: "IngredientPizza",
                column: "ingredientsId");

            migrationBuilder.AddForeignKey(
                name: "FK_IngredientPizza_Ingredient_ingredientsId",
                table: "IngredientPizza",
                column: "ingredientsId",
                principalTable: "Ingredient",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
