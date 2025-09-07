using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RecipeHubAPI.Migrations
{
    /// <inheritdoc />
    public partial class recipeIngredientQuantityDoubleFieldConversion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "QuantityNumber",
                table: "RecipeIngredients",
                type: "float",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "QuantityNumber",
                table: "RecipeIngredients",
                type: "int",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float",
                oldNullable: true);
        }
    }
}
