using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RecipeHubAPI.Migrations
{
    /// <inheritdoc />
    public partial class secondUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RecipeIngredients_Ingredients_IngredientId",
                table: "RecipeIngredients");

            migrationBuilder.DropForeignKey(
                name: "FK_RecipeIngredients_Measurements_MeasurementUnitId",
                table: "RecipeIngredients");

            migrationBuilder.DropForeignKey(
                name: "FK_Recipes_Users_UserId",
                table: "Recipes");

            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingListIngredients_Ingredients_IngredientId",
                table: "ShoppingListIngredients");

            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingListIngredients_Measurements_MeasurementUnitId",
                table: "ShoppingListIngredients");

            migrationBuilder.DropTable(
                name: "RecipeIngredients");

            migrationBuilder.DropIndex(
                name: "IX_ShoppingListIngredients_IngredientId",
                table: "ShoppingListIngredients");

            migrationBuilder.DropIndex(
                name: "IX_RecipeIngredients_IngredientId",
                table: "RecipeIngredients");

            migrationBuilder.DropIndex(
                name: "IX_RecipeCategories_RecipeId",
                table: "RecipeCategories");

            migrationBuilder.DropIndex(
                name: "IX_GroupRecipes_GroupId",
                table: "GroupRecipes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Measurements",
                table: "Measurements");

            migrationBuilder.DropColumn(
                name: "IngredientId",
                table: "ShoppingListIngredients");

            migrationBuilder.RenameTable(
                name: "Measurements",
                newName: "MeasurementUnits");

            migrationBuilder.RenameColumn(
                name: "IngredientId",
                table: "RecipeIngredients",
                newName: "SortOrder");

            migrationBuilder.AddColumn<string>(
                name: "Ingredient",
                table: "ShoppingListIngredients",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "IngredientName",
                table: "RecipeIngredients",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Groups",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Abbreviation",
                table: "MeasurementUnits",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MeasurementUnits",
                table: "MeasurementUnits",
                column: "MeasurementUnitId");

            migrationBuilder.InsertData(
                table: "MeasurementUnits",
                columns: new[] { "MeasurementUnitId", "Abbreviation", "Name" },
                values: new object[,]
                {
                    { 1, "mg", "Milligram" },
                    { 2, "g", "Gram" },
                    { 3, "kg", "Kilogram" },
                    { 4, "t", "Metric ton" },
                    { 5, "oz", "Ounce" },
                    { 6, "lb", "Pound" },
                    { 7, "st", "Stone" },
                    { 8, "ton", "Ton (US)" },
                    { 9, "ton", "Ton (UK)" },
                    { 10, "mL", "Milliliter" },
                    { 11, "cL", "Centiliter" },
                    { 12, "L", "Liter" },
                    { 13, "cm³", "Cubic centimeter" },
                    { 14, "m³", "Cubic meter" },
                    { 15, "tsp", "Teaspoon" },
                    { 16, "tbsp", "Tablespoon" },
                    { 17, "fl oz", "Fluid ounce" },
                    { 18, "c", "Cup" },
                    { 19, "pt", "Pint" },
                    { 20, "qt", "Quart" },
                    { 21, "gal", "Gallon" },
                    { 22, "bbl", "Barrel" },
                    { 23, "mm", "Millimeter" },
                    { 24, "cm", "Centimeter" },
                    { 25, "m", "Meter" },
                    { 26, "km", "Kilometer" },
                    { 27, "in", "Inch" },
                    { 28, "ft", "Foot" },
                    { 29, "yd", "Yard" },
                    { 30, "mi", "Mile" },
                    { 31, "nmi", "Nautical mile" },
                    { 32, "°C", "Celsius" },
                    { 33, "°F", "Fahrenheit" },
                    { 34, "K", "Kelvin" },
                    { 35, "ms", "Millisecond" },
                    { 36, "s", "Second" },
                    { 37, "min", "Minute" },
                    { 38, "h", "Hour" },
                    { 39, "d", "Day" },
                    { 40, "wk", "Week" },
                    { 41, "mo", "Month" },
                    { 42, "yr", "Year" },
                    { 43, "J", "Joule" },
                    { 44, "kJ", "Kilojoule" },
                    { 45, "cal", "Calorie" },
                    { 46, "kcal", "Kilocalorie" },
                    { 47, "Wh", "Watt-hour" },
                    { 48, "kWh", "Kilowatt-hour" },
                    { 49, "m/s", "Meters per second" },
                    { 50, "km/h", "Kilometers per hour" },
                    { 51, "mph", "Miles per hour" },
                    { 52, "kt", "Knots" },
                    { 53, "Pa", "Pascal" },
                    { 54, "kPa", "Kilopascal" },
                    { 55, "bar", "Bar" },
                    { 56, "atm", "Atmosphere" },
                    { 57, "psi", "Pound per square inch" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_RecipeCategories_RecipeId_CategoryId",
                table: "RecipeCategories",
                columns: new[] { "RecipeId", "CategoryId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Groups_UserId",
                table: "Groups",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupRecipes_GroupId_RecipeId",
                table: "GroupRecipes",
                columns: new[] { "GroupId", "RecipeId" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Groups_Users_UserId",
                table: "Groups",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RecipeIngredients_MeasurementUnits_MeasurementUnitId",
                table: "RecipeIngredients",
                column: "MeasurementUnitId",
                principalTable: "MeasurementUnits",
                principalColumn: "MeasurementUnitId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Recipes_Users_UserId",
                table: "Recipes",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingListIngredients_MeasurementUnits_MeasurementUnitId",
                table: "ShoppingListIngredients",
                column: "MeasurementUnitId",
                principalTable: "MeasurementUnits",
                principalColumn: "MeasurementUnitId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Groups_Users_UserId",
                table: "Groups");

            migrationBuilder.DropForeignKey(
                name: "FK_RecipeIngredients_MeasurementUnits_MeasurementUnitId",
                table: "RecipeIngredients");

            migrationBuilder.DropForeignKey(
                name: "FK_Recipes_Users_UserId",
                table: "Recipes");

            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingListIngredients_MeasurementUnits_MeasurementUnitId",
                table: "ShoppingListIngredients");

            migrationBuilder.DropIndex(
                name: "IX_RecipeCategories_RecipeId_CategoryId",
                table: "RecipeCategories");

            migrationBuilder.DropIndex(
                name: "IX_Groups_UserId",
                table: "Groups");

            migrationBuilder.DropIndex(
                name: "IX_GroupRecipes_GroupId_RecipeId",
                table: "GroupRecipes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MeasurementUnits",
                table: "MeasurementUnits");

            migrationBuilder.DeleteData(
                table: "MeasurementUnits",
                keyColumn: "MeasurementUnitId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "MeasurementUnits",
                keyColumn: "MeasurementUnitId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "MeasurementUnits",
                keyColumn: "MeasurementUnitId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "MeasurementUnits",
                keyColumn: "MeasurementUnitId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "MeasurementUnits",
                keyColumn: "MeasurementUnitId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "MeasurementUnits",
                keyColumn: "MeasurementUnitId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "MeasurementUnits",
                keyColumn: "MeasurementUnitId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "MeasurementUnits",
                keyColumn: "MeasurementUnitId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "MeasurementUnits",
                keyColumn: "MeasurementUnitId",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "MeasurementUnits",
                keyColumn: "MeasurementUnitId",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "MeasurementUnits",
                keyColumn: "MeasurementUnitId",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "MeasurementUnits",
                keyColumn: "MeasurementUnitId",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "MeasurementUnits",
                keyColumn: "MeasurementUnitId",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "MeasurementUnits",
                keyColumn: "MeasurementUnitId",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "MeasurementUnits",
                keyColumn: "MeasurementUnitId",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "MeasurementUnits",
                keyColumn: "MeasurementUnitId",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "MeasurementUnits",
                keyColumn: "MeasurementUnitId",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "MeasurementUnits",
                keyColumn: "MeasurementUnitId",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "MeasurementUnits",
                keyColumn: "MeasurementUnitId",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "MeasurementUnits",
                keyColumn: "MeasurementUnitId",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "MeasurementUnits",
                keyColumn: "MeasurementUnitId",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "MeasurementUnits",
                keyColumn: "MeasurementUnitId",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "MeasurementUnits",
                keyColumn: "MeasurementUnitId",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "MeasurementUnits",
                keyColumn: "MeasurementUnitId",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "MeasurementUnits",
                keyColumn: "MeasurementUnitId",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "MeasurementUnits",
                keyColumn: "MeasurementUnitId",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "MeasurementUnits",
                keyColumn: "MeasurementUnitId",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "MeasurementUnits",
                keyColumn: "MeasurementUnitId",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "MeasurementUnits",
                keyColumn: "MeasurementUnitId",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "MeasurementUnits",
                keyColumn: "MeasurementUnitId",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "MeasurementUnits",
                keyColumn: "MeasurementUnitId",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "MeasurementUnits",
                keyColumn: "MeasurementUnitId",
                keyValue: 32);

            migrationBuilder.DeleteData(
                table: "MeasurementUnits",
                keyColumn: "MeasurementUnitId",
                keyValue: 33);

            migrationBuilder.DeleteData(
                table: "MeasurementUnits",
                keyColumn: "MeasurementUnitId",
                keyValue: 34);

            migrationBuilder.DeleteData(
                table: "MeasurementUnits",
                keyColumn: "MeasurementUnitId",
                keyValue: 35);

            migrationBuilder.DeleteData(
                table: "MeasurementUnits",
                keyColumn: "MeasurementUnitId",
                keyValue: 36);

            migrationBuilder.DeleteData(
                table: "MeasurementUnits",
                keyColumn: "MeasurementUnitId",
                keyValue: 37);

            migrationBuilder.DeleteData(
                table: "MeasurementUnits",
                keyColumn: "MeasurementUnitId",
                keyValue: 38);

            migrationBuilder.DeleteData(
                table: "MeasurementUnits",
                keyColumn: "MeasurementUnitId",
                keyValue: 39);

            migrationBuilder.DeleteData(
                table: "MeasurementUnits",
                keyColumn: "MeasurementUnitId",
                keyValue: 40);

            migrationBuilder.DeleteData(
                table: "MeasurementUnits",
                keyColumn: "MeasurementUnitId",
                keyValue: 41);

            migrationBuilder.DeleteData(
                table: "MeasurementUnits",
                keyColumn: "MeasurementUnitId",
                keyValue: 42);

            migrationBuilder.DeleteData(
                table: "MeasurementUnits",
                keyColumn: "MeasurementUnitId",
                keyValue: 43);

            migrationBuilder.DeleteData(
                table: "MeasurementUnits",
                keyColumn: "MeasurementUnitId",
                keyValue: 44);

            migrationBuilder.DeleteData(
                table: "MeasurementUnits",
                keyColumn: "MeasurementUnitId",
                keyValue: 45);

            migrationBuilder.DeleteData(
                table: "MeasurementUnits",
                keyColumn: "MeasurementUnitId",
                keyValue: 46);

            migrationBuilder.DeleteData(
                table: "MeasurementUnits",
                keyColumn: "MeasurementUnitId",
                keyValue: 47);

            migrationBuilder.DeleteData(
                table: "MeasurementUnits",
                keyColumn: "MeasurementUnitId",
                keyValue: 48);

            migrationBuilder.DeleteData(
                table: "MeasurementUnits",
                keyColumn: "MeasurementUnitId",
                keyValue: 49);

            migrationBuilder.DeleteData(
                table: "MeasurementUnits",
                keyColumn: "MeasurementUnitId",
                keyValue: 50);

            migrationBuilder.DeleteData(
                table: "MeasurementUnits",
                keyColumn: "MeasurementUnitId",
                keyValue: 51);

            migrationBuilder.DeleteData(
                table: "MeasurementUnits",
                keyColumn: "MeasurementUnitId",
                keyValue: 52);

            migrationBuilder.DeleteData(
                table: "MeasurementUnits",
                keyColumn: "MeasurementUnitId",
                keyValue: 53);

            migrationBuilder.DeleteData(
                table: "MeasurementUnits",
                keyColumn: "MeasurementUnitId",
                keyValue: 54);

            migrationBuilder.DeleteData(
                table: "MeasurementUnits",
                keyColumn: "MeasurementUnitId",
                keyValue: 55);

            migrationBuilder.DeleteData(
                table: "MeasurementUnits",
                keyColumn: "MeasurementUnitId",
                keyValue: 56);

            migrationBuilder.DeleteData(
                table: "MeasurementUnits",
                keyColumn: "MeasurementUnitId",
                keyValue: 57);

            migrationBuilder.DropColumn(
                name: "Ingredient",
                table: "ShoppingListIngredients");

            migrationBuilder.DropColumn(
                name: "IngredientName",
                table: "RecipeIngredients");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Groups");

            migrationBuilder.DropColumn(
                name: "Abbreviation",
                table: "MeasurementUnits");

            migrationBuilder.RenameTable(
                name: "MeasurementUnits",
                newName: "Measurements");

            migrationBuilder.RenameColumn(
                name: "SortOrder",
                table: "RecipeIngredients",
                newName: "IngredientId");

            migrationBuilder.AddColumn<int>(
                name: "IngredientId",
                table: "ShoppingListIngredients",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Measurements",
                table: "Measurements",
                column: "MeasurementUnitId");

            migrationBuilder.CreateTable(
                name: "RecipeIngredients",
                columns: table => new
                {
                    IngredientId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ingredients", x => x.IngredientId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingListIngredients_IngredientId",
                table: "ShoppingListIngredients",
                column: "IngredientId");

            migrationBuilder.CreateIndex(
                name: "IX_RecipeIngredients_IngredientId",
                table: "RecipeIngredients",
                column: "IngredientId");

            migrationBuilder.CreateIndex(
                name: "IX_RecipeCategories_RecipeId",
                table: "RecipeCategories",
                column: "RecipeId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupRecipes_GroupId",
                table: "GroupRecipes",
                column: "GroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_RecipeIngredients_Ingredients_IngredientId",
                table: "RecipeIngredients",
                column: "IngredientId",
                principalTable: "RecipeIngredients",
                principalColumn: "IngredientId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RecipeIngredients_Measurements_MeasurementUnitId",
                table: "RecipeIngredients",
                column: "MeasurementUnitId",
                principalTable: "Measurements",
                principalColumn: "MeasurementUnitId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Recipes_Users_UserId",
                table: "Recipes",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingListIngredients_Ingredients_IngredientId",
                table: "ShoppingListIngredients",
                column: "IngredientId",
                principalTable: "RecipeIngredients",
                principalColumn: "IngredientId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingListIngredients_Measurements_MeasurementUnitId",
                table: "ShoppingListIngredients",
                column: "MeasurementUnitId",
                principalTable: "Measurements",
                principalColumn: "MeasurementUnitId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
