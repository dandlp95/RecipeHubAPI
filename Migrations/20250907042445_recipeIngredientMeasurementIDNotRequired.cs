using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RecipeHubAPI.Migrations
{
    /// <inheritdoc />
    public partial class recipeIngredientMeasurementIDNotRequired : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RecipeIngredients_MeasurementUnits_MeasurementUnitId",
                table: "RecipeIngredients");

            migrationBuilder.AlterColumn<int>(
                name: "MeasurementUnitId",
                table: "RecipeIngredients",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_RecipeIngredients_MeasurementUnits_MeasurementUnitId",
                table: "RecipeIngredients",
                column: "MeasurementUnitId",
                principalTable: "MeasurementUnits",
                principalColumn: "MeasurementUnitId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RecipeIngredients_MeasurementUnits_MeasurementUnitId",
                table: "RecipeIngredients");

            migrationBuilder.AlterColumn<int>(
                name: "MeasurementUnitId",
                table: "RecipeIngredients",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_RecipeIngredients_MeasurementUnits_MeasurementUnitId",
                table: "RecipeIngredients",
                column: "MeasurementUnitId",
                principalTable: "MeasurementUnits",
                principalColumn: "MeasurementUnitId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
