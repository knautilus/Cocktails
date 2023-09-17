using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Cocktails.DbMigrations.Migrations
{
    /// <inheritdoc />
    public partial class FillData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "CocktailCategory",
                columns: new[] { "Id", "CreateDate", "ModifyDate", "Name" },
                values: new object[,]
                {
                    { 1L, new DateTimeOffset(new DateTime(2023, 9, 17, 11, 6, 5, 250, DateTimeKind.Unspecified).AddTicks(7731), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2023, 9, 17, 11, 6, 5, 250, DateTimeKind.Unspecified).AddTicks(7731), new TimeSpan(0, 0, 0, 0, 0)), "Classic" },
                    { 2L, new DateTimeOffset(new DateTime(2023, 9, 17, 11, 6, 5, 250, DateTimeKind.Unspecified).AddTicks(7731), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2023, 9, 17, 11, 6, 5, 250, DateTimeKind.Unspecified).AddTicks(7731), new TimeSpan(0, 0, 0, 0, 0)), "Modern classic" },
                    { 3L, new DateTimeOffset(new DateTime(2023, 9, 17, 11, 6, 5, 250, DateTimeKind.Unspecified).AddTicks(7731), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2023, 9, 17, 11, 6, 5, 250, DateTimeKind.Unspecified).AddTicks(7731), new TimeSpan(0, 0, 0, 0, 0)), "Coffee & Dessert" },
                    { 4L, new DateTimeOffset(new DateTime(2023, 9, 17, 11, 6, 5, 250, DateTimeKind.Unspecified).AddTicks(7731), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2023, 9, 17, 11, 6, 5, 250, DateTimeKind.Unspecified).AddTicks(7731), new TimeSpan(0, 0, 0, 0, 0)), "Shots" },
                    { 5L, new DateTimeOffset(new DateTime(2023, 9, 17, 11, 6, 5, 250, DateTimeKind.Unspecified).AddTicks(7731), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2023, 9, 17, 11, 6, 5, 250, DateTimeKind.Unspecified).AddTicks(7731), new TimeSpan(0, 0, 0, 0, 0)), "Tropical" },
                    { 6L, new DateTimeOffset(new DateTime(2023, 9, 17, 11, 6, 5, 250, DateTimeKind.Unspecified).AddTicks(7731), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2023, 9, 17, 11, 6, 5, 250, DateTimeKind.Unspecified).AddTicks(7731), new TimeSpan(0, 0, 0, 0, 0)), "Nonalcoholic" }
                });

            migrationBuilder.InsertData(
                table: "Flavor",
                columns: new[] { "Id", "CreateDate", "ModifyDate", "Name" },
                values: new object[,]
                {
                    { 1L, new DateTimeOffset(new DateTime(2023, 9, 17, 11, 6, 5, 250, DateTimeKind.Unspecified).AddTicks(7731), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2023, 9, 17, 11, 6, 5, 250, DateTimeKind.Unspecified).AddTicks(7731), new TimeSpan(0, 0, 0, 0, 0)), "Bitter" },
                    { 2L, new DateTimeOffset(new DateTime(2023, 9, 17, 11, 6, 5, 250, DateTimeKind.Unspecified).AddTicks(7731), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2023, 9, 17, 11, 6, 5, 250, DateTimeKind.Unspecified).AddTicks(7731), new TimeSpan(0, 0, 0, 0, 0)), "Sweet" },
                    { 3L, new DateTimeOffset(new DateTime(2023, 9, 17, 11, 6, 5, 250, DateTimeKind.Unspecified).AddTicks(7731), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2023, 9, 17, 11, 6, 5, 250, DateTimeKind.Unspecified).AddTicks(7731), new TimeSpan(0, 0, 0, 0, 0)), "Sour" },
                    { 4L, new DateTimeOffset(new DateTime(2023, 9, 17, 11, 6, 5, 250, DateTimeKind.Unspecified).AddTicks(7731), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2023, 9, 17, 11, 6, 5, 250, DateTimeKind.Unspecified).AddTicks(7731), new TimeSpan(0, 0, 0, 0, 0)), "Fruity" }
                });

            migrationBuilder.InsertData(
                table: "Ingredient",
                columns: new[] { "Id", "CreateDate", "ModifyDate", "Name" },
                values: new object[,]
                {
                    { 1L, new DateTimeOffset(new DateTime(2023, 9, 17, 11, 6, 5, 250, DateTimeKind.Unspecified).AddTicks(7731), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2023, 9, 17, 11, 6, 5, 250, DateTimeKind.Unspecified).AddTicks(7731), new TimeSpan(0, 0, 0, 0, 0)), "Vodka" },
                    { 2L, new DateTimeOffset(new DateTime(2023, 9, 17, 11, 6, 5, 250, DateTimeKind.Unspecified).AddTicks(7731), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2023, 9, 17, 11, 6, 5, 250, DateTimeKind.Unspecified).AddTicks(7731), new TimeSpan(0, 0, 0, 0, 0)), "Gin" },
                    { 3L, new DateTimeOffset(new DateTime(2023, 9, 17, 11, 6, 5, 250, DateTimeKind.Unspecified).AddTicks(7731), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2023, 9, 17, 11, 6, 5, 250, DateTimeKind.Unspecified).AddTicks(7731), new TimeSpan(0, 0, 0, 0, 0)), "Rum" },
                    { 4L, new DateTimeOffset(new DateTime(2023, 9, 17, 11, 6, 5, 250, DateTimeKind.Unspecified).AddTicks(7731), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2023, 9, 17, 11, 6, 5, 250, DateTimeKind.Unspecified).AddTicks(7731), new TimeSpan(0, 0, 0, 0, 0)), "Tequila" },
                    { 5L, new DateTimeOffset(new DateTime(2023, 9, 17, 11, 6, 5, 250, DateTimeKind.Unspecified).AddTicks(7731), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2023, 9, 17, 11, 6, 5, 250, DateTimeKind.Unspecified).AddTicks(7731), new TimeSpan(0, 0, 0, 0, 0)), "Tomato juice" },
                    { 6L, new DateTimeOffset(new DateTime(2023, 9, 17, 11, 6, 5, 250, DateTimeKind.Unspecified).AddTicks(7731), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2023, 9, 17, 11, 6, 5, 250, DateTimeKind.Unspecified).AddTicks(7731), new TimeSpan(0, 0, 0, 0, 0)), "Lemon juice" },
                    { 7L, new DateTimeOffset(new DateTime(2023, 9, 17, 11, 6, 5, 250, DateTimeKind.Unspecified).AddTicks(7731), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2023, 9, 17, 11, 6, 5, 250, DateTimeKind.Unspecified).AddTicks(7731), new TimeSpan(0, 0, 0, 0, 0)), "Coffee liqueur" }
                });

            migrationBuilder.InsertData(
                table: "MeasureUnit",
                columns: new[] { "Id", "CreateDate", "ModifyDate", "Name" },
                values: new object[,]
                {
                    { 1L, new DateTimeOffset(new DateTime(2023, 9, 17, 11, 6, 5, 250, DateTimeKind.Unspecified).AddTicks(7731), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2023, 9, 17, 11, 6, 5, 250, DateTimeKind.Unspecified).AddTicks(7731), new TimeSpan(0, 0, 0, 0, 0)), "Oz" },
                    { 2L, new DateTimeOffset(new DateTime(2023, 9, 17, 11, 6, 5, 250, DateTimeKind.Unspecified).AddTicks(7731), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2023, 9, 17, 11, 6, 5, 250, DateTimeKind.Unspecified).AddTicks(7731), new TimeSpan(0, 0, 0, 0, 0)), "Piece" },
                    { 3L, new DateTimeOffset(new DateTime(2023, 9, 17, 11, 6, 5, 250, DateTimeKind.Unspecified).AddTicks(7731), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2023, 9, 17, 11, 6, 5, 250, DateTimeKind.Unspecified).AddTicks(7731), new TimeSpan(0, 0, 0, 0, 0)), "Dash" },
                    { 4L, new DateTimeOffset(new DateTime(2023, 9, 17, 11, 6, 5, 250, DateTimeKind.Unspecified).AddTicks(7731), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2023, 9, 17, 11, 6, 5, 250, DateTimeKind.Unspecified).AddTicks(7731), new TimeSpan(0, 0, 0, 0, 0)), "Cup" },
                    { 5L, new DateTimeOffset(new DateTime(2023, 9, 17, 11, 6, 5, 250, DateTimeKind.Unspecified).AddTicks(7731), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2023, 9, 17, 11, 6, 5, 250, DateTimeKind.Unspecified).AddTicks(7731), new TimeSpan(0, 0, 0, 0, 0)), "Shot" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "CocktailCategory",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "CocktailCategory",
                keyColumn: "Id",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                table: "CocktailCategory",
                keyColumn: "Id",
                keyValue: 3L);

            migrationBuilder.DeleteData(
                table: "CocktailCategory",
                keyColumn: "Id",
                keyValue: 4L);

            migrationBuilder.DeleteData(
                table: "CocktailCategory",
                keyColumn: "Id",
                keyValue: 5L);

            migrationBuilder.DeleteData(
                table: "CocktailCategory",
                keyColumn: "Id",
                keyValue: 6L);

            migrationBuilder.DeleteData(
                table: "Flavor",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "Flavor",
                keyColumn: "Id",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                table: "Flavor",
                keyColumn: "Id",
                keyValue: 3L);

            migrationBuilder.DeleteData(
                table: "Flavor",
                keyColumn: "Id",
                keyValue: 4L);

            migrationBuilder.DeleteData(
                table: "Ingredient",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "Ingredient",
                keyColumn: "Id",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                table: "Ingredient",
                keyColumn: "Id",
                keyValue: 3L);

            migrationBuilder.DeleteData(
                table: "Ingredient",
                keyColumn: "Id",
                keyValue: 4L);

            migrationBuilder.DeleteData(
                table: "Ingredient",
                keyColumn: "Id",
                keyValue: 5L);

            migrationBuilder.DeleteData(
                table: "Ingredient",
                keyColumn: "Id",
                keyValue: 6L);

            migrationBuilder.DeleteData(
                table: "Ingredient",
                keyColumn: "Id",
                keyValue: 7L);

            migrationBuilder.DeleteData(
                table: "MeasureUnit",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "MeasureUnit",
                keyColumn: "Id",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                table: "MeasureUnit",
                keyColumn: "Id",
                keyValue: 3L);

            migrationBuilder.DeleteData(
                table: "MeasureUnit",
                keyColumn: "Id",
                keyValue: 4L);

            migrationBuilder.DeleteData(
                table: "MeasureUnit",
                keyColumn: "Id",
                keyValue: 5L);
        }
    }
}
