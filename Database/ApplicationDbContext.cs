using RecipeHubAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace RecipeHubAPI.Database
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<GroupRecipe> GroupRecipes { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<MeasurementUnit> Measurements { get; set; }
        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<RecipeCategory> RecipeCategories { get; set; }
        public DbSet<RecipeIngredient> RecipeIngredients { get; set; }
        public DbSet<ShoppingList> ShoppingList { get; set; }
        public DbSet<ShoppingListIngredients> ShoppingListIngredients { get; set; }
        public DbSet<Step> Steps { get; set; }
    }
}
