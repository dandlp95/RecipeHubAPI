using RecipeHubAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace RecipeHubAPI.Database
{
    /*
    To perform a database migration using .NET Core, you can use the following steps:

    Open a terminal/command prompt and navigate to the project directory where your .NET Core project is located.

    Ensure that the Entity Framework Core tool is installed by running the following command:

    dotnet tool install --global dotnet-ef

    Ensure that your project has a reference to the Entity Framework Core package. You can check this by looking at the project's .csproj file.

    Next, you will need to add a migration to your project. To do this, run the following command, replacing YourMigrationName with a name of your choice:

    dotnet ef migrations add YourMigrationName
        
    This command will generate a new migration file in the project's Migrations directory.

    Finally, to apply the migration and update your database, run the following command:

    dotnet ef database update

    If you encounter any errors during the migration process, you may need to troubleshoot the issue by reviewing error messages or updating your project configuration. 
    Additionally, you may want to consider backing up your database before performing any migrations to avoid data loss.
    */
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<GroupRecipe> GroupRecipes { get; set; }
        //public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<MeasurementUnit> MeasurementUnits { get; set; }
        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<RecipeCategory> RecipeCategories { get; set; }
        public DbSet<RecipeIngredient> RecipeIngredients { get; set; }
        public DbSet<ShoppingList> ShoppingList { get; set; }
        public DbSet<ShoppingListIngredients> ShoppingListIngredients { get; set; }
        public DbSet<Step> Steps { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Category>();
            modelBuilder.Entity<GroupRecipe>();
            modelBuilder.Entity<Recipe>();
            modelBuilder.Entity<RecipeIngredient>();
            modelBuilder.Entity<ShoppingList>();
            modelBuilder.Entity<ShoppingListIngredients>();
            modelBuilder.Entity<Step>();
            modelBuilder.Entity<User>()
                .HasIndex(u => u.EmailAddress)
                .IsUnique();

            modelBuilder.Entity<User>()
                .HasIndex(u => u.UserName)
                .IsUnique();

            modelBuilder.Entity<RecipeCategory>()
                .HasIndex(rc => new {rc.RecipeId, rc.CategoryId})
                .IsUnique();

            modelBuilder.Entity<Group>()
                .HasOne(g => g.User)
                .WithMany()
                .HasForeignKey(g => g.UserId)
                .OnDelete(DeleteBehavior.Restrict); // Prevents auto-deletion of Groups when a User is deleted

            modelBuilder.Entity<Recipe>()
                .HasOne(r => r.User)
                .WithMany()
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.Restrict); // Prevents auto-deletion of Groups when a User is deleted

            modelBuilder.Entity<GroupRecipe>()
                .HasIndex(gr => new { gr.GroupId, gr.RecipeId })  // Create unique index
                .IsUnique(); // Ensures no duplicate GroupId + RecipeId

            modelBuilder.Entity<GroupRecipe>()
                .HasOne(gr => gr.Group)
                .WithMany(g => g.GroupRecipes)
                .HasForeignKey(gr => gr.GroupId)
                .OnDelete(DeleteBehavior.Cascade); // Deleting a Group deletes its GroupRecipes

            modelBuilder.Entity<GroupRecipe>()
                .HasOne(gr => gr.Recipe)
                .WithMany(r => r.GroupRecipes)
                .HasForeignKey(gr => gr.RecipeId)
                .OnDelete(DeleteBehavior.Cascade); // Deleting a Recipe deletes its GroupRecipes

            modelBuilder.Entity<MeasurementUnit>().HasData(
                new MeasurementUnit { MeasurementUnitId = 1, Name = "Milligram", Abbreviation = "mg" },
                new MeasurementUnit { MeasurementUnitId = 2, Name = "Gram", Abbreviation = "g" },
                new MeasurementUnit { MeasurementUnitId = 3, Name = "Kilogram", Abbreviation = "kg" },
                new MeasurementUnit { MeasurementUnitId = 4, Name = "Metric ton", Abbreviation = "t" },
                new MeasurementUnit { MeasurementUnitId = 5, Name = "Ounce", Abbreviation = "oz" },
                new MeasurementUnit { MeasurementUnitId = 6, Name = "Pound", Abbreviation = "lb" },
                new MeasurementUnit { MeasurementUnitId = 7, Name = "Stone", Abbreviation = "st" },
                new MeasurementUnit { MeasurementUnitId = 8, Name = "Ton (US)", Abbreviation = "ton" },
                new MeasurementUnit { MeasurementUnitId = 9, Name = "Ton (UK)", Abbreviation = "ton" },

                new MeasurementUnit { MeasurementUnitId = 10, Name = "Milliliter", Abbreviation = "mL" },
                new MeasurementUnit { MeasurementUnitId = 11, Name = "Centiliter", Abbreviation = "cL" },
                new MeasurementUnit { MeasurementUnitId = 12, Name = "Liter", Abbreviation = "L" },
                new MeasurementUnit { MeasurementUnitId = 13, Name = "Cubic centimeter", Abbreviation = "cm³" },
                new MeasurementUnit { MeasurementUnitId = 14, Name = "Cubic meter", Abbreviation = "m³" },
                new MeasurementUnit { MeasurementUnitId = 15, Name = "Teaspoon", Abbreviation = "tsp" },
                new MeasurementUnit { MeasurementUnitId = 16, Name = "Tablespoon", Abbreviation = "tbsp" },
                new MeasurementUnit { MeasurementUnitId = 17, Name = "Fluid ounce", Abbreviation = "fl oz" },
                new MeasurementUnit { MeasurementUnitId = 18, Name = "Cup", Abbreviation = "c" },
                new MeasurementUnit { MeasurementUnitId = 19, Name = "Pint", Abbreviation = "pt" },
                new MeasurementUnit { MeasurementUnitId = 20, Name = "Quart", Abbreviation = "qt" },
                new MeasurementUnit { MeasurementUnitId = 21, Name = "Gallon", Abbreviation = "gal" },
                new MeasurementUnit { MeasurementUnitId = 22, Name = "Barrel", Abbreviation = "bbl" },

                new MeasurementUnit { MeasurementUnitId = 23, Name = "Millimeter", Abbreviation = "mm" },
                new MeasurementUnit { MeasurementUnitId = 24, Name = "Centimeter", Abbreviation = "cm" },
                new MeasurementUnit { MeasurementUnitId = 25, Name = "Meter", Abbreviation = "m" },
                new MeasurementUnit { MeasurementUnitId = 26, Name = "Kilometer", Abbreviation = "km" },
                new MeasurementUnit { MeasurementUnitId = 27, Name = "Inch", Abbreviation = "in" },
                new MeasurementUnit { MeasurementUnitId = 28, Name = "Foot", Abbreviation = "ft" },
                new MeasurementUnit { MeasurementUnitId = 29, Name = "Yard", Abbreviation = "yd" },
                new MeasurementUnit { MeasurementUnitId = 30, Name = "Mile", Abbreviation = "mi" },
                new MeasurementUnit { MeasurementUnitId = 31, Name = "Nautical mile", Abbreviation = "nmi" },

                new MeasurementUnit { MeasurementUnitId = 32, Name = "Celsius", Abbreviation = "°C" },
                new MeasurementUnit { MeasurementUnitId = 33, Name = "Fahrenheit", Abbreviation = "°F" },
                new MeasurementUnit { MeasurementUnitId = 34, Name = "Kelvin", Abbreviation = "K" },

                new MeasurementUnit { MeasurementUnitId = 35, Name = "Millisecond", Abbreviation = "ms" },
                new MeasurementUnit { MeasurementUnitId = 36, Name = "Second", Abbreviation = "s" },
                new MeasurementUnit { MeasurementUnitId = 37, Name = "Minute", Abbreviation = "min" },
                new MeasurementUnit { MeasurementUnitId = 38, Name = "Hour", Abbreviation = "h" },
                new MeasurementUnit { MeasurementUnitId = 39, Name = "Day", Abbreviation = "d" },
                new MeasurementUnit { MeasurementUnitId = 40, Name = "Week", Abbreviation = "wk" },
                new MeasurementUnit { MeasurementUnitId = 41, Name = "Month", Abbreviation = "mo" },
                new MeasurementUnit { MeasurementUnitId = 42, Name = "Year", Abbreviation = "yr" },

                new MeasurementUnit { MeasurementUnitId = 43, Name = "Joule", Abbreviation = "J" },
                new MeasurementUnit { MeasurementUnitId = 44, Name = "Kilojoule", Abbreviation = "kJ" },
                new MeasurementUnit { MeasurementUnitId = 45, Name = "Calorie", Abbreviation = "cal" },
                new MeasurementUnit { MeasurementUnitId = 46, Name = "Kilocalorie", Abbreviation = "kcal" },
                new MeasurementUnit { MeasurementUnitId = 47, Name = "Watt-hour", Abbreviation = "Wh" },
                new MeasurementUnit { MeasurementUnitId = 48, Name = "Kilowatt-hour", Abbreviation = "kWh" },

                new MeasurementUnit { MeasurementUnitId = 49, Name = "Meters per second", Abbreviation = "m/s" },
                new MeasurementUnit { MeasurementUnitId = 50, Name = "Kilometers per hour", Abbreviation = "km/h" },
                new MeasurementUnit { MeasurementUnitId = 51, Name = "Miles per hour", Abbreviation = "mph" },
                new MeasurementUnit { MeasurementUnitId = 52, Name = "Knots", Abbreviation = "kt" },

                new MeasurementUnit { MeasurementUnitId = 53, Name = "Pascal", Abbreviation = "Pa" },
                new MeasurementUnit { MeasurementUnitId = 54, Name = "Kilopascal", Abbreviation = "kPa" },
                new MeasurementUnit { MeasurementUnitId = 55, Name = "Bar", Abbreviation = "bar" },
                new MeasurementUnit { MeasurementUnitId = 56, Name = "Atmosphere", Abbreviation = "atm" },
                new MeasurementUnit { MeasurementUnitId = 57, Name = "Pound per square inch", Abbreviation = "psi" }
            );

        }
    }
}
