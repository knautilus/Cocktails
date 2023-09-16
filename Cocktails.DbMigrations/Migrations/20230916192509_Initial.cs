using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Cocktails.DbMigrations.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CocktailCategory",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    CreateDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    ModifyDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CocktailCategory", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Flavor",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    CreateDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    ModifyDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Flavor", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Ingredient",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    CreateDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    ModifyDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ingredient", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MeasureUnit",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreateDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    ModifyDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MeasureUnit", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cocktail",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    FlavorId = table.Column<long>(type: "bigint", nullable: false),
                    CocktailCategoryId = table.Column<long>(type: "bigint", nullable: false),
                    CreateDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    ModifyDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cocktail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cocktail_CocktailCategory_CocktailCategoryId",
                        column: x => x.CocktailCategoryId,
                        principalTable: "CocktailCategory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Cocktail_Flavor_FlavorId",
                        column: x => x.FlavorId,
                        principalTable: "Flavor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Mix",
                columns: table => new
                {
                    CocktailId = table.Column<long>(type: "bigint", nullable: false),
                    IngredientId = table.Column<long>(type: "bigint", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MeasureUnitId = table.Column<long>(type: "bigint", nullable: false),
                    CreateDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    ModifyDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mix", x => new { x.CocktailId, x.IngredientId });
                    table.ForeignKey(
                        name: "FK_Mix_Cocktail_CocktailId",
                        column: x => x.CocktailId,
                        principalTable: "Cocktail",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Mix_Ingredient_IngredientId",
                        column: x => x.IngredientId,
                        principalTable: "Ingredient",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Mix_MeasureUnit_MeasureUnitId",
                        column: x => x.MeasureUnitId,
                        principalTable: "MeasureUnit",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "CocktailCategory",
                columns: new[] { "Id", "CreateDate", "ModifyDate", "Name" },
                values: new object[,]
                {
                    { 1L, new DateTimeOffset(new DateTime(2023, 9, 16, 19, 25, 9, 92, DateTimeKind.Unspecified).AddTicks(4592), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2023, 9, 16, 19, 25, 9, 92, DateTimeKind.Unspecified).AddTicks(4592), new TimeSpan(0, 0, 0, 0, 0)), "Classic" },
                    { 2L, new DateTimeOffset(new DateTime(2023, 9, 16, 19, 25, 9, 92, DateTimeKind.Unspecified).AddTicks(4592), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2023, 9, 16, 19, 25, 9, 92, DateTimeKind.Unspecified).AddTicks(4592), new TimeSpan(0, 0, 0, 0, 0)), "Modern classic" },
                    { 3L, new DateTimeOffset(new DateTime(2023, 9, 16, 19, 25, 9, 92, DateTimeKind.Unspecified).AddTicks(4592), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2023, 9, 16, 19, 25, 9, 92, DateTimeKind.Unspecified).AddTicks(4592), new TimeSpan(0, 0, 0, 0, 0)), "Coffee & Dessert" },
                    { 4L, new DateTimeOffset(new DateTime(2023, 9, 16, 19, 25, 9, 92, DateTimeKind.Unspecified).AddTicks(4592), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2023, 9, 16, 19, 25, 9, 92, DateTimeKind.Unspecified).AddTicks(4592), new TimeSpan(0, 0, 0, 0, 0)), "Shots" },
                    { 5L, new DateTimeOffset(new DateTime(2023, 9, 16, 19, 25, 9, 92, DateTimeKind.Unspecified).AddTicks(4592), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2023, 9, 16, 19, 25, 9, 92, DateTimeKind.Unspecified).AddTicks(4592), new TimeSpan(0, 0, 0, 0, 0)), "Tropical" },
                    { 6L, new DateTimeOffset(new DateTime(2023, 9, 16, 19, 25, 9, 92, DateTimeKind.Unspecified).AddTicks(4592), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2023, 9, 16, 19, 25, 9, 92, DateTimeKind.Unspecified).AddTicks(4592), new TimeSpan(0, 0, 0, 0, 0)), "Nonalcoholic" }
                });

            migrationBuilder.InsertData(
                table: "Flavor",
                columns: new[] { "Id", "CreateDate", "ModifyDate", "Name" },
                values: new object[,]
                {
                    { 1L, new DateTimeOffset(new DateTime(2023, 9, 16, 19, 25, 9, 92, DateTimeKind.Unspecified).AddTicks(4592), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2023, 9, 16, 19, 25, 9, 92, DateTimeKind.Unspecified).AddTicks(4592), new TimeSpan(0, 0, 0, 0, 0)), "Bitter" },
                    { 2L, new DateTimeOffset(new DateTime(2023, 9, 16, 19, 25, 9, 92, DateTimeKind.Unspecified).AddTicks(4592), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2023, 9, 16, 19, 25, 9, 92, DateTimeKind.Unspecified).AddTicks(4592), new TimeSpan(0, 0, 0, 0, 0)), "Sweet" },
                    { 3L, new DateTimeOffset(new DateTime(2023, 9, 16, 19, 25, 9, 92, DateTimeKind.Unspecified).AddTicks(4592), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2023, 9, 16, 19, 25, 9, 92, DateTimeKind.Unspecified).AddTicks(4592), new TimeSpan(0, 0, 0, 0, 0)), "Sour" },
                    { 4L, new DateTimeOffset(new DateTime(2023, 9, 16, 19, 25, 9, 92, DateTimeKind.Unspecified).AddTicks(4592), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2023, 9, 16, 19, 25, 9, 92, DateTimeKind.Unspecified).AddTicks(4592), new TimeSpan(0, 0, 0, 0, 0)), "Fruity" }
                });

            migrationBuilder.InsertData(
                table: "Ingredient",
                columns: new[] { "Id", "CreateDate", "ModifyDate", "Name" },
                values: new object[,]
                {
                    { 1L, new DateTimeOffset(new DateTime(2023, 9, 16, 19, 25, 9, 92, DateTimeKind.Unspecified).AddTicks(4592), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2023, 9, 16, 19, 25, 9, 92, DateTimeKind.Unspecified).AddTicks(4592), new TimeSpan(0, 0, 0, 0, 0)), "Vodka" },
                    { 2L, new DateTimeOffset(new DateTime(2023, 9, 16, 19, 25, 9, 92, DateTimeKind.Unspecified).AddTicks(4592), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2023, 9, 16, 19, 25, 9, 92, DateTimeKind.Unspecified).AddTicks(4592), new TimeSpan(0, 0, 0, 0, 0)), "Gin" },
                    { 3L, new DateTimeOffset(new DateTime(2023, 9, 16, 19, 25, 9, 92, DateTimeKind.Unspecified).AddTicks(4592), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2023, 9, 16, 19, 25, 9, 92, DateTimeKind.Unspecified).AddTicks(4592), new TimeSpan(0, 0, 0, 0, 0)), "Rum" },
                    { 4L, new DateTimeOffset(new DateTime(2023, 9, 16, 19, 25, 9, 92, DateTimeKind.Unspecified).AddTicks(4592), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2023, 9, 16, 19, 25, 9, 92, DateTimeKind.Unspecified).AddTicks(4592), new TimeSpan(0, 0, 0, 0, 0)), "Tequila" },
                    { 5L, new DateTimeOffset(new DateTime(2023, 9, 16, 19, 25, 9, 92, DateTimeKind.Unspecified).AddTicks(4592), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2023, 9, 16, 19, 25, 9, 92, DateTimeKind.Unspecified).AddTicks(4592), new TimeSpan(0, 0, 0, 0, 0)), "Tomato juice" },
                    { 6L, new DateTimeOffset(new DateTime(2023, 9, 16, 19, 25, 9, 92, DateTimeKind.Unspecified).AddTicks(4592), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2023, 9, 16, 19, 25, 9, 92, DateTimeKind.Unspecified).AddTicks(4592), new TimeSpan(0, 0, 0, 0, 0)), "Lemon juice" }
                });

            migrationBuilder.InsertData(
                table: "MeasureUnit",
                columns: new[] { "Id", "CreateDate", "ModifyDate", "Name" },
                values: new object[,]
                {
                    { 1L, new DateTimeOffset(new DateTime(2023, 9, 16, 19, 25, 9, 92, DateTimeKind.Unspecified).AddTicks(4592), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2023, 9, 16, 19, 25, 9, 92, DateTimeKind.Unspecified).AddTicks(4592), new TimeSpan(0, 0, 0, 0, 0)), "Oz" },
                    { 2L, new DateTimeOffset(new DateTime(2023, 9, 16, 19, 25, 9, 92, DateTimeKind.Unspecified).AddTicks(4592), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2023, 9, 16, 19, 25, 9, 92, DateTimeKind.Unspecified).AddTicks(4592), new TimeSpan(0, 0, 0, 0, 0)), "Piece" },
                    { 3L, new DateTimeOffset(new DateTime(2023, 9, 16, 19, 25, 9, 92, DateTimeKind.Unspecified).AddTicks(4592), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2023, 9, 16, 19, 25, 9, 92, DateTimeKind.Unspecified).AddTicks(4592), new TimeSpan(0, 0, 0, 0, 0)), "Dash" },
                    { 4L, new DateTimeOffset(new DateTime(2023, 9, 16, 19, 25, 9, 92, DateTimeKind.Unspecified).AddTicks(4592), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2023, 9, 16, 19, 25, 9, 92, DateTimeKind.Unspecified).AddTicks(4592), new TimeSpan(0, 0, 0, 0, 0)), "Cup" },
                    { 5L, new DateTimeOffset(new DateTime(2023, 9, 16, 19, 25, 9, 92, DateTimeKind.Unspecified).AddTicks(4592), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2023, 9, 16, 19, 25, 9, 92, DateTimeKind.Unspecified).AddTicks(4592), new TimeSpan(0, 0, 0, 0, 0)), "Shot" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cocktail_CocktailCategoryId",
                table: "Cocktail",
                column: "CocktailCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Cocktail_FlavorId",
                table: "Cocktail",
                column: "FlavorId");

            migrationBuilder.CreateIndex(
                name: "IX_Mix_IngredientId",
                table: "Mix",
                column: "IngredientId");

            migrationBuilder.CreateIndex(
                name: "IX_Mix_MeasureUnitId",
                table: "Mix",
                column: "MeasureUnitId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Mix");

            migrationBuilder.DropTable(
                name: "Cocktail");

            migrationBuilder.DropTable(
                name: "Ingredient");

            migrationBuilder.DropTable(
                name: "MeasureUnit");

            migrationBuilder.DropTable(
                name: "CocktailCategory");

            migrationBuilder.DropTable(
                name: "Flavor");
        }
    }
}
