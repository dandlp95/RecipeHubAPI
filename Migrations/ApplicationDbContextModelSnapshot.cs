﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RecipeHubAPI.Database;

#nullable disable

namespace RecipeHubAPI.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("RecipeHubAPI.Models.Category", b =>
                {
                    b.Property<int>("CategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CategoryId"));

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CategoryId");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("RecipeHubAPI.Models.Group", b =>
                {
                    b.Property<int>("GroupId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("GroupId"));

                    b.Property<DateTime>("CreatedDateOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("GroupId");

                    b.HasIndex("UserId");

                    b.ToTable("Groups");
                });

            modelBuilder.Entity("RecipeHubAPI.Models.GroupRecipe", b =>
                {
                    b.Property<int>("GroupRecipeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("GroupRecipeId"));

                    b.Property<int>("GroupId")
                        .HasColumnType("int");

                    b.Property<int>("RecipeId")
                        .HasColumnType("int");

                    b.HasKey("GroupRecipeId");

                    b.HasIndex("RecipeId");

                    b.HasIndex("GroupId", "RecipeId")
                        .IsUnique();

                    b.ToTable("GroupRecipes");
                });

            modelBuilder.Entity("RecipeHubAPI.Models.MeasurementUnit", b =>
                {
                    b.Property<int>("MeasurementUnitId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MeasurementUnitId"));

                    b.Property<string>("Abbreviation")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("MeasurementUnitId");

                    b.ToTable("MeasurementUnits");

                    b.HasData(
                        new
                        {
                            MeasurementUnitId = 1,
                            Abbreviation = "mg",
                            Name = "Milligram"
                        },
                        new
                        {
                            MeasurementUnitId = 2,
                            Abbreviation = "g",
                            Name = "Gram"
                        },
                        new
                        {
                            MeasurementUnitId = 3,
                            Abbreviation = "kg",
                            Name = "Kilogram"
                        },
                        new
                        {
                            MeasurementUnitId = 4,
                            Abbreviation = "t",
                            Name = "Metric ton"
                        },
                        new
                        {
                            MeasurementUnitId = 5,
                            Abbreviation = "oz",
                            Name = "Ounce"
                        },
                        new
                        {
                            MeasurementUnitId = 6,
                            Abbreviation = "lb",
                            Name = "Pound"
                        },
                        new
                        {
                            MeasurementUnitId = 7,
                            Abbreviation = "st",
                            Name = "Stone"
                        },
                        new
                        {
                            MeasurementUnitId = 8,
                            Abbreviation = "ton",
                            Name = "Ton (US)"
                        },
                        new
                        {
                            MeasurementUnitId = 9,
                            Abbreviation = "ton",
                            Name = "Ton (UK)"
                        },
                        new
                        {
                            MeasurementUnitId = 10,
                            Abbreviation = "mL",
                            Name = "Milliliter"
                        },
                        new
                        {
                            MeasurementUnitId = 11,
                            Abbreviation = "cL",
                            Name = "Centiliter"
                        },
                        new
                        {
                            MeasurementUnitId = 12,
                            Abbreviation = "L",
                            Name = "Liter"
                        },
                        new
                        {
                            MeasurementUnitId = 13,
                            Abbreviation = "cm³",
                            Name = "Cubic centimeter"
                        },
                        new
                        {
                            MeasurementUnitId = 14,
                            Abbreviation = "m³",
                            Name = "Cubic meter"
                        },
                        new
                        {
                            MeasurementUnitId = 15,
                            Abbreviation = "tsp",
                            Name = "Teaspoon"
                        },
                        new
                        {
                            MeasurementUnitId = 16,
                            Abbreviation = "tbsp",
                            Name = "Tablespoon"
                        },
                        new
                        {
                            MeasurementUnitId = 17,
                            Abbreviation = "fl oz",
                            Name = "Fluid ounce"
                        },
                        new
                        {
                            MeasurementUnitId = 18,
                            Abbreviation = "c",
                            Name = "Cup"
                        },
                        new
                        {
                            MeasurementUnitId = 19,
                            Abbreviation = "pt",
                            Name = "Pint"
                        },
                        new
                        {
                            MeasurementUnitId = 20,
                            Abbreviation = "qt",
                            Name = "Quart"
                        },
                        new
                        {
                            MeasurementUnitId = 21,
                            Abbreviation = "gal",
                            Name = "Gallon"
                        },
                        new
                        {
                            MeasurementUnitId = 22,
                            Abbreviation = "bbl",
                            Name = "Barrel"
                        },
                        new
                        {
                            MeasurementUnitId = 23,
                            Abbreviation = "mm",
                            Name = "Millimeter"
                        },
                        new
                        {
                            MeasurementUnitId = 24,
                            Abbreviation = "cm",
                            Name = "Centimeter"
                        },
                        new
                        {
                            MeasurementUnitId = 25,
                            Abbreviation = "m",
                            Name = "Meter"
                        },
                        new
                        {
                            MeasurementUnitId = 26,
                            Abbreviation = "km",
                            Name = "Kilometer"
                        },
                        new
                        {
                            MeasurementUnitId = 27,
                            Abbreviation = "in",
                            Name = "Inch"
                        },
                        new
                        {
                            MeasurementUnitId = 28,
                            Abbreviation = "ft",
                            Name = "Foot"
                        },
                        new
                        {
                            MeasurementUnitId = 29,
                            Abbreviation = "yd",
                            Name = "Yard"
                        },
                        new
                        {
                            MeasurementUnitId = 30,
                            Abbreviation = "mi",
                            Name = "Mile"
                        },
                        new
                        {
                            MeasurementUnitId = 31,
                            Abbreviation = "nmi",
                            Name = "Nautical mile"
                        },
                        new
                        {
                            MeasurementUnitId = 32,
                            Abbreviation = "°C",
                            Name = "Celsius"
                        },
                        new
                        {
                            MeasurementUnitId = 33,
                            Abbreviation = "°F",
                            Name = "Fahrenheit"
                        },
                        new
                        {
                            MeasurementUnitId = 34,
                            Abbreviation = "K",
                            Name = "Kelvin"
                        },
                        new
                        {
                            MeasurementUnitId = 35,
                            Abbreviation = "ms",
                            Name = "Millisecond"
                        },
                        new
                        {
                            MeasurementUnitId = 36,
                            Abbreviation = "s",
                            Name = "Second"
                        },
                        new
                        {
                            MeasurementUnitId = 37,
                            Abbreviation = "min",
                            Name = "Minute"
                        },
                        new
                        {
                            MeasurementUnitId = 38,
                            Abbreviation = "h",
                            Name = "Hour"
                        },
                        new
                        {
                            MeasurementUnitId = 39,
                            Abbreviation = "d",
                            Name = "Day"
                        },
                        new
                        {
                            MeasurementUnitId = 40,
                            Abbreviation = "wk",
                            Name = "Week"
                        },
                        new
                        {
                            MeasurementUnitId = 41,
                            Abbreviation = "mo",
                            Name = "Month"
                        },
                        new
                        {
                            MeasurementUnitId = 42,
                            Abbreviation = "yr",
                            Name = "Year"
                        },
                        new
                        {
                            MeasurementUnitId = 43,
                            Abbreviation = "J",
                            Name = "Joule"
                        },
                        new
                        {
                            MeasurementUnitId = 44,
                            Abbreviation = "kJ",
                            Name = "Kilojoule"
                        },
                        new
                        {
                            MeasurementUnitId = 45,
                            Abbreviation = "cal",
                            Name = "Calorie"
                        },
                        new
                        {
                            MeasurementUnitId = 46,
                            Abbreviation = "kcal",
                            Name = "Kilocalorie"
                        },
                        new
                        {
                            MeasurementUnitId = 47,
                            Abbreviation = "Wh",
                            Name = "Watt-hour"
                        },
                        new
                        {
                            MeasurementUnitId = 48,
                            Abbreviation = "kWh",
                            Name = "Kilowatt-hour"
                        },
                        new
                        {
                            MeasurementUnitId = 49,
                            Abbreviation = "m/s",
                            Name = "Meters per second"
                        },
                        new
                        {
                            MeasurementUnitId = 50,
                            Abbreviation = "km/h",
                            Name = "Kilometers per hour"
                        },
                        new
                        {
                            MeasurementUnitId = 51,
                            Abbreviation = "mph",
                            Name = "Miles per hour"
                        },
                        new
                        {
                            MeasurementUnitId = 52,
                            Abbreviation = "kt",
                            Name = "Knots"
                        },
                        new
                        {
                            MeasurementUnitId = 53,
                            Abbreviation = "Pa",
                            Name = "Pascal"
                        },
                        new
                        {
                            MeasurementUnitId = 54,
                            Abbreviation = "kPa",
                            Name = "Kilopascal"
                        },
                        new
                        {
                            MeasurementUnitId = 55,
                            Abbreviation = "bar",
                            Name = "Bar"
                        },
                        new
                        {
                            MeasurementUnitId = 56,
                            Abbreviation = "atm",
                            Name = "Atmosphere"
                        },
                        new
                        {
                            MeasurementUnitId = 57,
                            Abbreviation = "psi",
                            Name = "Pound per square inch"
                        });
                });

            modelBuilder.Entity("RecipeHubAPI.Models.Recipe", b =>
                {
                    b.Property<int>("RecipeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RecipeId"));

                    b.Property<string>("CookingTime")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("RecipeId");

                    b.HasIndex("UserId");

                    b.ToTable("Recipes");
                });

            modelBuilder.Entity("RecipeHubAPI.Models.RecipeCategory", b =>
                {
                    b.Property<int>("RecipeCategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RecipeCategoryId"));

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<int>("RecipeId")
                        .HasColumnType("int");

                    b.Property<int>("SortOrder")
                        .HasColumnType("int");

                    b.HasKey("RecipeCategoryId");

                    b.HasIndex("CategoryId");

                    b.HasIndex("RecipeId", "CategoryId")
                        .IsUnique();

                    b.ToTable("RecipeCategories");
                });

            modelBuilder.Entity("RecipeHubAPI.Models.RecipeIngredient", b =>
                {
                    b.Property<int>("RecipeIngredientId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RecipeIngredientId"));

                    b.Property<string>("IngredientName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("MeasurementUnitId")
                        .HasColumnType("int");

                    b.Property<int>("QuantityNumber")
                        .HasColumnType("int");

                    b.Property<int>("RecipeId")
                        .HasColumnType("int");

                    b.Property<int>("SortOrder")
                        .HasColumnType("int");

                    b.HasKey("RecipeIngredientId");

                    b.HasIndex("MeasurementUnitId");

                    b.HasIndex("RecipeId");

                    b.ToTable("RecipeIngredients");
                });

            modelBuilder.Entity("RecipeHubAPI.Models.ShoppingList", b =>
                {
                    b.Property<int>("ShoppingListId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ShoppingListId"));

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ShoppingListId");

                    b.ToTable("ShoppingList");
                });

            modelBuilder.Entity("RecipeHubAPI.Models.ShoppingListIngredients", b =>
                {
                    b.Property<int>("ShoppingListIngredientsId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ShoppingListIngredientsId"));

                    b.Property<string>("Ingredient")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("MeasurementUnitId")
                        .HasColumnType("int");

                    b.Property<int>("QuantityNumber")
                        .HasColumnType("int");

                    b.Property<int>("ShoppingListId")
                        .HasColumnType("int");

                    b.HasKey("ShoppingListIngredientsId");

                    b.HasIndex("MeasurementUnitId");

                    b.HasIndex("ShoppingListId");

                    b.ToTable("ShoppingListIngredients");
                });

            modelBuilder.Entity("RecipeHubAPI.Models.Step", b =>
                {
                    b.Property<int>("StepId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("StepId"));

                    b.Property<int>("RecipeId")
                        .HasColumnType("int");

                    b.Property<int>("SortOrder")
                        .HasColumnType("int");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("StepId");

                    b.HasIndex("RecipeId");

                    b.ToTable("Steps");
                });

            modelBuilder.Entity("RecipeHubAPI.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserId"));

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("EmailAddress")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("PasswordSalt")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.HasKey("UserId");

                    b.HasIndex("EmailAddress")
                        .IsUnique();

                    b.HasIndex("UserName")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("RecipeHubAPI.Models.Group", b =>
                {
                    b.HasOne("RecipeHubAPI.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("RecipeHubAPI.Models.GroupRecipe", b =>
                {
                    b.HasOne("RecipeHubAPI.Models.Group", "Group")
                        .WithMany("GroupRecipes")
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RecipeHubAPI.Models.Recipe", "Recipe")
                        .WithMany("GroupRecipes")
                        .HasForeignKey("RecipeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Group");

                    b.Navigation("Recipe");
                });

            modelBuilder.Entity("RecipeHubAPI.Models.Recipe", b =>
                {
                    b.HasOne("RecipeHubAPI.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("RecipeHubAPI.Models.RecipeCategory", b =>
                {
                    b.HasOne("RecipeHubAPI.Models.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RecipeHubAPI.Models.Recipe", "Recipe")
                        .WithMany()
                        .HasForeignKey("RecipeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("Recipe");
                });

            modelBuilder.Entity("RecipeHubAPI.Models.RecipeIngredient", b =>
                {
                    b.HasOne("RecipeHubAPI.Models.MeasurementUnit", "MeasurementUnit")
                        .WithMany()
                        .HasForeignKey("MeasurementUnitId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RecipeHubAPI.Models.Recipe", "Recipe")
                        .WithMany()
                        .HasForeignKey("RecipeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("MeasurementUnit");

                    b.Navigation("Recipe");
                });

            modelBuilder.Entity("RecipeHubAPI.Models.ShoppingListIngredients", b =>
                {
                    b.HasOne("RecipeHubAPI.Models.MeasurementUnit", "MeasurementUnit")
                        .WithMany()
                        .HasForeignKey("MeasurementUnitId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RecipeHubAPI.Models.ShoppingList", "ShoppingList")
                        .WithMany()
                        .HasForeignKey("ShoppingListId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("MeasurementUnit");

                    b.Navigation("ShoppingList");
                });

            modelBuilder.Entity("RecipeHubAPI.Models.Step", b =>
                {
                    b.HasOne("RecipeHubAPI.Models.Recipe", "Recipe")
                        .WithMany()
                        .HasForeignKey("RecipeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Recipe");
                });

            modelBuilder.Entity("RecipeHubAPI.Models.Group", b =>
                {
                    b.Navigation("GroupRecipes");
                });

            modelBuilder.Entity("RecipeHubAPI.Models.Recipe", b =>
                {
                    b.Navigation("GroupRecipes");
                });
#pragma warning restore 612, 618
        }
    }
}
