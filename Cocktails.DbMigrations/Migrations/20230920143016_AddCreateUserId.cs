using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cocktails.DbMigrations.Migrations
{
    /// <inheritdoc />
    public partial class AddCreateUserId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "CreateUserId",
                table: "MeasureUnit",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "ModifyUserId",
                table: "MeasureUnit",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "CreateUserId",
                table: "Ingredient",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "ModifyUserId",
                table: "Ingredient",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "CreateUserId",
                table: "Flavor",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "ModifyUserId",
                table: "Flavor",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "CreateUserId",
                table: "CocktailCategory",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "ModifyUserId",
                table: "CocktailCategory",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "CreateUserId",
                table: "Cocktail",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "ModifyUserId",
                table: "Cocktail",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.UpdateData(
                table: "CocktailCategory",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreateDate", "CreateUserId", "ModifyDate", "ModifyUserId" },
                values: new object[] { new DateTimeOffset(new DateTime(2023, 9, 20, 14, 30, 16, 167, DateTimeKind.Unspecified).AddTicks(7318), new TimeSpan(0, 0, 0, 0, 0)), 0L, new DateTimeOffset(new DateTime(2023, 9, 20, 14, 30, 16, 167, DateTimeKind.Unspecified).AddTicks(7318), new TimeSpan(0, 0, 0, 0, 0)), 0L });

            migrationBuilder.UpdateData(
                table: "CocktailCategory",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "CreateDate", "CreateUserId", "ModifyDate", "ModifyUserId" },
                values: new object[] { new DateTimeOffset(new DateTime(2023, 9, 20, 14, 30, 16, 167, DateTimeKind.Unspecified).AddTicks(7318), new TimeSpan(0, 0, 0, 0, 0)), 0L, new DateTimeOffset(new DateTime(2023, 9, 20, 14, 30, 16, 167, DateTimeKind.Unspecified).AddTicks(7318), new TimeSpan(0, 0, 0, 0, 0)), 0L });

            migrationBuilder.UpdateData(
                table: "CocktailCategory",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "CreateDate", "CreateUserId", "ModifyDate", "ModifyUserId" },
                values: new object[] { new DateTimeOffset(new DateTime(2023, 9, 20, 14, 30, 16, 167, DateTimeKind.Unspecified).AddTicks(7318), new TimeSpan(0, 0, 0, 0, 0)), 0L, new DateTimeOffset(new DateTime(2023, 9, 20, 14, 30, 16, 167, DateTimeKind.Unspecified).AddTicks(7318), new TimeSpan(0, 0, 0, 0, 0)), 0L });

            migrationBuilder.UpdateData(
                table: "CocktailCategory",
                keyColumn: "Id",
                keyValue: 4L,
                columns: new[] { "CreateDate", "CreateUserId", "ModifyDate", "ModifyUserId" },
                values: new object[] { new DateTimeOffset(new DateTime(2023, 9, 20, 14, 30, 16, 167, DateTimeKind.Unspecified).AddTicks(7318), new TimeSpan(0, 0, 0, 0, 0)), 0L, new DateTimeOffset(new DateTime(2023, 9, 20, 14, 30, 16, 167, DateTimeKind.Unspecified).AddTicks(7318), new TimeSpan(0, 0, 0, 0, 0)), 0L });

            migrationBuilder.UpdateData(
                table: "CocktailCategory",
                keyColumn: "Id",
                keyValue: 5L,
                columns: new[] { "CreateDate", "CreateUserId", "ModifyDate", "ModifyUserId" },
                values: new object[] { new DateTimeOffset(new DateTime(2023, 9, 20, 14, 30, 16, 167, DateTimeKind.Unspecified).AddTicks(7318), new TimeSpan(0, 0, 0, 0, 0)), 0L, new DateTimeOffset(new DateTime(2023, 9, 20, 14, 30, 16, 167, DateTimeKind.Unspecified).AddTicks(7318), new TimeSpan(0, 0, 0, 0, 0)), 0L });

            migrationBuilder.UpdateData(
                table: "CocktailCategory",
                keyColumn: "Id",
                keyValue: 6L,
                columns: new[] { "CreateDate", "CreateUserId", "ModifyDate", "ModifyUserId" },
                values: new object[] { new DateTimeOffset(new DateTime(2023, 9, 20, 14, 30, 16, 167, DateTimeKind.Unspecified).AddTicks(7318), new TimeSpan(0, 0, 0, 0, 0)), 0L, new DateTimeOffset(new DateTime(2023, 9, 20, 14, 30, 16, 167, DateTimeKind.Unspecified).AddTicks(7318), new TimeSpan(0, 0, 0, 0, 0)), 0L });

            migrationBuilder.UpdateData(
                table: "Flavor",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreateDate", "CreateUserId", "ModifyDate", "ModifyUserId" },
                values: new object[] { new DateTimeOffset(new DateTime(2023, 9, 20, 14, 30, 16, 167, DateTimeKind.Unspecified).AddTicks(7318), new TimeSpan(0, 0, 0, 0, 0)), 0L, new DateTimeOffset(new DateTime(2023, 9, 20, 14, 30, 16, 167, DateTimeKind.Unspecified).AddTicks(7318), new TimeSpan(0, 0, 0, 0, 0)), 0L });

            migrationBuilder.UpdateData(
                table: "Flavor",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "CreateDate", "CreateUserId", "ModifyDate", "ModifyUserId" },
                values: new object[] { new DateTimeOffset(new DateTime(2023, 9, 20, 14, 30, 16, 167, DateTimeKind.Unspecified).AddTicks(7318), new TimeSpan(0, 0, 0, 0, 0)), 0L, new DateTimeOffset(new DateTime(2023, 9, 20, 14, 30, 16, 167, DateTimeKind.Unspecified).AddTicks(7318), new TimeSpan(0, 0, 0, 0, 0)), 0L });

            migrationBuilder.UpdateData(
                table: "Flavor",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "CreateDate", "CreateUserId", "ModifyDate", "ModifyUserId" },
                values: new object[] { new DateTimeOffset(new DateTime(2023, 9, 20, 14, 30, 16, 167, DateTimeKind.Unspecified).AddTicks(7318), new TimeSpan(0, 0, 0, 0, 0)), 0L, new DateTimeOffset(new DateTime(2023, 9, 20, 14, 30, 16, 167, DateTimeKind.Unspecified).AddTicks(7318), new TimeSpan(0, 0, 0, 0, 0)), 0L });

            migrationBuilder.UpdateData(
                table: "Flavor",
                keyColumn: "Id",
                keyValue: 4L,
                columns: new[] { "CreateDate", "CreateUserId", "ModifyDate", "ModifyUserId" },
                values: new object[] { new DateTimeOffset(new DateTime(2023, 9, 20, 14, 30, 16, 167, DateTimeKind.Unspecified).AddTicks(7318), new TimeSpan(0, 0, 0, 0, 0)), 0L, new DateTimeOffset(new DateTime(2023, 9, 20, 14, 30, 16, 167, DateTimeKind.Unspecified).AddTicks(7318), new TimeSpan(0, 0, 0, 0, 0)), 0L });

            migrationBuilder.UpdateData(
                table: "Ingredient",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreateDate", "CreateUserId", "ModifyDate", "ModifyUserId" },
                values: new object[] { new DateTimeOffset(new DateTime(2023, 9, 20, 14, 30, 16, 167, DateTimeKind.Unspecified).AddTicks(7318), new TimeSpan(0, 0, 0, 0, 0)), 0L, new DateTimeOffset(new DateTime(2023, 9, 20, 14, 30, 16, 167, DateTimeKind.Unspecified).AddTicks(7318), new TimeSpan(0, 0, 0, 0, 0)), 0L });

            migrationBuilder.UpdateData(
                table: "Ingredient",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "CreateDate", "CreateUserId", "ModifyDate", "ModifyUserId" },
                values: new object[] { new DateTimeOffset(new DateTime(2023, 9, 20, 14, 30, 16, 167, DateTimeKind.Unspecified).AddTicks(7318), new TimeSpan(0, 0, 0, 0, 0)), 0L, new DateTimeOffset(new DateTime(2023, 9, 20, 14, 30, 16, 167, DateTimeKind.Unspecified).AddTicks(7318), new TimeSpan(0, 0, 0, 0, 0)), 0L });

            migrationBuilder.UpdateData(
                table: "Ingredient",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "CreateDate", "CreateUserId", "ModifyDate", "ModifyUserId" },
                values: new object[] { new DateTimeOffset(new DateTime(2023, 9, 20, 14, 30, 16, 167, DateTimeKind.Unspecified).AddTicks(7318), new TimeSpan(0, 0, 0, 0, 0)), 0L, new DateTimeOffset(new DateTime(2023, 9, 20, 14, 30, 16, 167, DateTimeKind.Unspecified).AddTicks(7318), new TimeSpan(0, 0, 0, 0, 0)), 0L });

            migrationBuilder.UpdateData(
                table: "Ingredient",
                keyColumn: "Id",
                keyValue: 4L,
                columns: new[] { "CreateDate", "CreateUserId", "ModifyDate", "ModifyUserId" },
                values: new object[] { new DateTimeOffset(new DateTime(2023, 9, 20, 14, 30, 16, 167, DateTimeKind.Unspecified).AddTicks(7318), new TimeSpan(0, 0, 0, 0, 0)), 0L, new DateTimeOffset(new DateTime(2023, 9, 20, 14, 30, 16, 167, DateTimeKind.Unspecified).AddTicks(7318), new TimeSpan(0, 0, 0, 0, 0)), 0L });

            migrationBuilder.UpdateData(
                table: "Ingredient",
                keyColumn: "Id",
                keyValue: 5L,
                columns: new[] { "CreateDate", "CreateUserId", "ModifyDate", "ModifyUserId" },
                values: new object[] { new DateTimeOffset(new DateTime(2023, 9, 20, 14, 30, 16, 167, DateTimeKind.Unspecified).AddTicks(7318), new TimeSpan(0, 0, 0, 0, 0)), 0L, new DateTimeOffset(new DateTime(2023, 9, 20, 14, 30, 16, 167, DateTimeKind.Unspecified).AddTicks(7318), new TimeSpan(0, 0, 0, 0, 0)), 0L });

            migrationBuilder.UpdateData(
                table: "Ingredient",
                keyColumn: "Id",
                keyValue: 6L,
                columns: new[] { "CreateDate", "CreateUserId", "ModifyDate", "ModifyUserId" },
                values: new object[] { new DateTimeOffset(new DateTime(2023, 9, 20, 14, 30, 16, 167, DateTimeKind.Unspecified).AddTicks(7318), new TimeSpan(0, 0, 0, 0, 0)), 0L, new DateTimeOffset(new DateTime(2023, 9, 20, 14, 30, 16, 167, DateTimeKind.Unspecified).AddTicks(7318), new TimeSpan(0, 0, 0, 0, 0)), 0L });

            migrationBuilder.UpdateData(
                table: "Ingredient",
                keyColumn: "Id",
                keyValue: 7L,
                columns: new[] { "CreateDate", "CreateUserId", "ModifyDate", "ModifyUserId" },
                values: new object[] { new DateTimeOffset(new DateTime(2023, 9, 20, 14, 30, 16, 167, DateTimeKind.Unspecified).AddTicks(7318), new TimeSpan(0, 0, 0, 0, 0)), 0L, new DateTimeOffset(new DateTime(2023, 9, 20, 14, 30, 16, 167, DateTimeKind.Unspecified).AddTicks(7318), new TimeSpan(0, 0, 0, 0, 0)), 0L });

            migrationBuilder.UpdateData(
                table: "MeasureUnit",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreateDate", "CreateUserId", "ModifyDate", "ModifyUserId" },
                values: new object[] { new DateTimeOffset(new DateTime(2023, 9, 20, 14, 30, 16, 167, DateTimeKind.Unspecified).AddTicks(7318), new TimeSpan(0, 0, 0, 0, 0)), 0L, new DateTimeOffset(new DateTime(2023, 9, 20, 14, 30, 16, 167, DateTimeKind.Unspecified).AddTicks(7318), new TimeSpan(0, 0, 0, 0, 0)), 0L });

            migrationBuilder.UpdateData(
                table: "MeasureUnit",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "CreateDate", "CreateUserId", "ModifyDate", "ModifyUserId" },
                values: new object[] { new DateTimeOffset(new DateTime(2023, 9, 20, 14, 30, 16, 167, DateTimeKind.Unspecified).AddTicks(7318), new TimeSpan(0, 0, 0, 0, 0)), 0L, new DateTimeOffset(new DateTime(2023, 9, 20, 14, 30, 16, 167, DateTimeKind.Unspecified).AddTicks(7318), new TimeSpan(0, 0, 0, 0, 0)), 0L });

            migrationBuilder.UpdateData(
                table: "MeasureUnit",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "CreateDate", "CreateUserId", "ModifyDate", "ModifyUserId" },
                values: new object[] { new DateTimeOffset(new DateTime(2023, 9, 20, 14, 30, 16, 167, DateTimeKind.Unspecified).AddTicks(7318), new TimeSpan(0, 0, 0, 0, 0)), 0L, new DateTimeOffset(new DateTime(2023, 9, 20, 14, 30, 16, 167, DateTimeKind.Unspecified).AddTicks(7318), new TimeSpan(0, 0, 0, 0, 0)), 0L });

            migrationBuilder.UpdateData(
                table: "MeasureUnit",
                keyColumn: "Id",
                keyValue: 4L,
                columns: new[] { "CreateDate", "CreateUserId", "ModifyDate", "ModifyUserId" },
                values: new object[] { new DateTimeOffset(new DateTime(2023, 9, 20, 14, 30, 16, 167, DateTimeKind.Unspecified).AddTicks(7318), new TimeSpan(0, 0, 0, 0, 0)), 0L, new DateTimeOffset(new DateTime(2023, 9, 20, 14, 30, 16, 167, DateTimeKind.Unspecified).AddTicks(7318), new TimeSpan(0, 0, 0, 0, 0)), 0L });

            migrationBuilder.UpdateData(
                table: "MeasureUnit",
                keyColumn: "Id",
                keyValue: 5L,
                columns: new[] { "CreateDate", "CreateUserId", "ModifyDate", "ModifyUserId" },
                values: new object[] { new DateTimeOffset(new DateTime(2023, 9, 20, 14, 30, 16, 167, DateTimeKind.Unspecified).AddTicks(7318), new TimeSpan(0, 0, 0, 0, 0)), 0L, new DateTimeOffset(new DateTime(2023, 9, 20, 14, 30, 16, 167, DateTimeKind.Unspecified).AddTicks(7318), new TimeSpan(0, 0, 0, 0, 0)), 0L });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreateUserId",
                table: "MeasureUnit");

            migrationBuilder.DropColumn(
                name: "ModifyUserId",
                table: "MeasureUnit");

            migrationBuilder.DropColumn(
                name: "CreateUserId",
                table: "Ingredient");

            migrationBuilder.DropColumn(
                name: "ModifyUserId",
                table: "Ingredient");

            migrationBuilder.DropColumn(
                name: "CreateUserId",
                table: "Flavor");

            migrationBuilder.DropColumn(
                name: "ModifyUserId",
                table: "Flavor");

            migrationBuilder.DropColumn(
                name: "CreateUserId",
                table: "CocktailCategory");

            migrationBuilder.DropColumn(
                name: "ModifyUserId",
                table: "CocktailCategory");

            migrationBuilder.DropColumn(
                name: "CreateUserId",
                table: "Cocktail");

            migrationBuilder.DropColumn(
                name: "ModifyUserId",
                table: "Cocktail");

            migrationBuilder.UpdateData(
                table: "CocktailCategory",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreateDate", "ModifyDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2023, 9, 17, 11, 6, 5, 250, DateTimeKind.Unspecified).AddTicks(7731), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2023, 9, 17, 11, 6, 5, 250, DateTimeKind.Unspecified).AddTicks(7731), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "CocktailCategory",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "CreateDate", "ModifyDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2023, 9, 17, 11, 6, 5, 250, DateTimeKind.Unspecified).AddTicks(7731), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2023, 9, 17, 11, 6, 5, 250, DateTimeKind.Unspecified).AddTicks(7731), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "CocktailCategory",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "CreateDate", "ModifyDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2023, 9, 17, 11, 6, 5, 250, DateTimeKind.Unspecified).AddTicks(7731), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2023, 9, 17, 11, 6, 5, 250, DateTimeKind.Unspecified).AddTicks(7731), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "CocktailCategory",
                keyColumn: "Id",
                keyValue: 4L,
                columns: new[] { "CreateDate", "ModifyDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2023, 9, 17, 11, 6, 5, 250, DateTimeKind.Unspecified).AddTicks(7731), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2023, 9, 17, 11, 6, 5, 250, DateTimeKind.Unspecified).AddTicks(7731), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "CocktailCategory",
                keyColumn: "Id",
                keyValue: 5L,
                columns: new[] { "CreateDate", "ModifyDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2023, 9, 17, 11, 6, 5, 250, DateTimeKind.Unspecified).AddTicks(7731), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2023, 9, 17, 11, 6, 5, 250, DateTimeKind.Unspecified).AddTicks(7731), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "CocktailCategory",
                keyColumn: "Id",
                keyValue: 6L,
                columns: new[] { "CreateDate", "ModifyDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2023, 9, 17, 11, 6, 5, 250, DateTimeKind.Unspecified).AddTicks(7731), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2023, 9, 17, 11, 6, 5, 250, DateTimeKind.Unspecified).AddTicks(7731), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Flavor",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreateDate", "ModifyDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2023, 9, 17, 11, 6, 5, 250, DateTimeKind.Unspecified).AddTicks(7731), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2023, 9, 17, 11, 6, 5, 250, DateTimeKind.Unspecified).AddTicks(7731), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Flavor",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "CreateDate", "ModifyDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2023, 9, 17, 11, 6, 5, 250, DateTimeKind.Unspecified).AddTicks(7731), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2023, 9, 17, 11, 6, 5, 250, DateTimeKind.Unspecified).AddTicks(7731), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Flavor",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "CreateDate", "ModifyDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2023, 9, 17, 11, 6, 5, 250, DateTimeKind.Unspecified).AddTicks(7731), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2023, 9, 17, 11, 6, 5, 250, DateTimeKind.Unspecified).AddTicks(7731), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Flavor",
                keyColumn: "Id",
                keyValue: 4L,
                columns: new[] { "CreateDate", "ModifyDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2023, 9, 17, 11, 6, 5, 250, DateTimeKind.Unspecified).AddTicks(7731), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2023, 9, 17, 11, 6, 5, 250, DateTimeKind.Unspecified).AddTicks(7731), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Ingredient",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreateDate", "ModifyDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2023, 9, 17, 11, 6, 5, 250, DateTimeKind.Unspecified).AddTicks(7731), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2023, 9, 17, 11, 6, 5, 250, DateTimeKind.Unspecified).AddTicks(7731), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Ingredient",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "CreateDate", "ModifyDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2023, 9, 17, 11, 6, 5, 250, DateTimeKind.Unspecified).AddTicks(7731), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2023, 9, 17, 11, 6, 5, 250, DateTimeKind.Unspecified).AddTicks(7731), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Ingredient",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "CreateDate", "ModifyDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2023, 9, 17, 11, 6, 5, 250, DateTimeKind.Unspecified).AddTicks(7731), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2023, 9, 17, 11, 6, 5, 250, DateTimeKind.Unspecified).AddTicks(7731), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Ingredient",
                keyColumn: "Id",
                keyValue: 4L,
                columns: new[] { "CreateDate", "ModifyDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2023, 9, 17, 11, 6, 5, 250, DateTimeKind.Unspecified).AddTicks(7731), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2023, 9, 17, 11, 6, 5, 250, DateTimeKind.Unspecified).AddTicks(7731), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Ingredient",
                keyColumn: "Id",
                keyValue: 5L,
                columns: new[] { "CreateDate", "ModifyDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2023, 9, 17, 11, 6, 5, 250, DateTimeKind.Unspecified).AddTicks(7731), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2023, 9, 17, 11, 6, 5, 250, DateTimeKind.Unspecified).AddTicks(7731), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Ingredient",
                keyColumn: "Id",
                keyValue: 6L,
                columns: new[] { "CreateDate", "ModifyDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2023, 9, 17, 11, 6, 5, 250, DateTimeKind.Unspecified).AddTicks(7731), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2023, 9, 17, 11, 6, 5, 250, DateTimeKind.Unspecified).AddTicks(7731), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Ingredient",
                keyColumn: "Id",
                keyValue: 7L,
                columns: new[] { "CreateDate", "ModifyDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2023, 9, 17, 11, 6, 5, 250, DateTimeKind.Unspecified).AddTicks(7731), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2023, 9, 17, 11, 6, 5, 250, DateTimeKind.Unspecified).AddTicks(7731), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "MeasureUnit",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreateDate", "ModifyDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2023, 9, 17, 11, 6, 5, 250, DateTimeKind.Unspecified).AddTicks(7731), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2023, 9, 17, 11, 6, 5, 250, DateTimeKind.Unspecified).AddTicks(7731), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "MeasureUnit",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "CreateDate", "ModifyDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2023, 9, 17, 11, 6, 5, 250, DateTimeKind.Unspecified).AddTicks(7731), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2023, 9, 17, 11, 6, 5, 250, DateTimeKind.Unspecified).AddTicks(7731), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "MeasureUnit",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "CreateDate", "ModifyDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2023, 9, 17, 11, 6, 5, 250, DateTimeKind.Unspecified).AddTicks(7731), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2023, 9, 17, 11, 6, 5, 250, DateTimeKind.Unspecified).AddTicks(7731), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "MeasureUnit",
                keyColumn: "Id",
                keyValue: 4L,
                columns: new[] { "CreateDate", "ModifyDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2023, 9, 17, 11, 6, 5, 250, DateTimeKind.Unspecified).AddTicks(7731), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2023, 9, 17, 11, 6, 5, 250, DateTimeKind.Unspecified).AddTicks(7731), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "MeasureUnit",
                keyColumn: "Id",
                keyValue: 5L,
                columns: new[] { "CreateDate", "ModifyDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2023, 9, 17, 11, 6, 5, 250, DateTimeKind.Unspecified).AddTicks(7731), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2023, 9, 17, 11, 6, 5, 250, DateTimeKind.Unspecified).AddTicks(7731), new TimeSpan(0, 0, 0, 0, 0)) });
        }
    }
}
