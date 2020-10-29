using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Persistance.Migrations
{
    public partial class AppInitMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Recipies",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWID()"),
                    Url = table.Column<string>(maxLength: 250, nullable: true),
                    Title = table.Column<string>(maxLength: 250, nullable: false),
                    Servings = table.Column<int>(nullable: false),
                    AllKcal = table.Column<float>(nullable: false),
                    AllCarbs = table.Column<float>(nullable: false),
                    AllProtein = table.Column<float>(nullable: false),
                    AllFat = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recipies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWID()"),
                    Url = table.Column<string>(maxLength: 250, nullable: false),
                    RecipieId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Images_Recipies_RecipieId",
                        column: x => x.RecipieId,
                        principalTable: "Recipies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "InstructionsSteps",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWID()"),
                    StepOrder = table.Column<int>(nullable: false),
                    Step = table.Column<string>(maxLength: 2500, nullable: false),
                    RecipieId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InstructionsSteps", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InstructionsSteps_Recipies_RecipieId",
                        column: x => x.RecipieId,
                        principalTable: "Recipies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RecipieComponents",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWID()"),
                    SubTitle = table.Column<string>(maxLength: 200, nullable: false),
                    RecipieId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecipieComponents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RecipieComponents_Recipies_RecipieId",
                        column: x => x.RecipieId,
                        principalTable: "Recipies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Tags",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWID()"),
                    Value = table.Column<string>(maxLength: 100, nullable: false),
                    RecipieId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tags", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tags_Recipies_RecipieId",
                        column: x => x.RecipieId,
                        principalTable: "Recipies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Ingredients",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWID()"),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    Amount = table.Column<float>(nullable: false),
                    Unit = table.Column<string>(maxLength: 30, nullable: true),
                    Kcal = table.Column<float>(nullable: false),
                    RecipieComponentId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ingredients", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ingredients_RecipieComponents_RecipieComponentId",
                        column: x => x.RecipieComponentId,
                        principalTable: "RecipieComponents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Images_RecipieId",
                table: "Images",
                column: "RecipieId");

            migrationBuilder.CreateIndex(
                name: "IX_Ingredients_RecipieComponentId",
                table: "Ingredients",
                column: "RecipieComponentId");

            migrationBuilder.CreateIndex(
                name: "IX_InstructionsSteps_RecipieId",
                table: "InstructionsSteps",
                column: "RecipieId");

            migrationBuilder.CreateIndex(
                name: "IX_RecipieComponents_RecipieId",
                table: "RecipieComponents",
                column: "RecipieId");

            migrationBuilder.CreateIndex(
                name: "IX_Tags_RecipieId",
                table: "Tags",
                column: "RecipieId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Images");

            migrationBuilder.DropTable(
                name: "Ingredients");

            migrationBuilder.DropTable(
                name: "InstructionsSteps");

            migrationBuilder.DropTable(
                name: "Tags");

            migrationBuilder.DropTable(
                name: "RecipieComponents");

            migrationBuilder.DropTable(
                name: "Recipies");
        }
    }
}
