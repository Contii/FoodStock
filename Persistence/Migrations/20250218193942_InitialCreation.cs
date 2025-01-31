using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    CategoryID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.CategoryID);
                });

            migrationBuilder.CreateTable(
                name: "ConsumptionReports",
                columns: table => new
                {
                    ConsumptionReportID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ReportInitialDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ReportFinalDate = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConsumptionReports", x => x.ConsumptionReportID);
                });

            migrationBuilder.CreateTable(
                name: "ShoppingLists",
                columns: table => new
                {
                    ShoppingListID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    ShoppingDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Status = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShoppingLists", x => x.ShoppingListID);
                });

            migrationBuilder.CreateTable(
                name: "SpoilReports",
                columns: table => new
                {
                    SpoilReportID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ReportType = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpoilReports", x => x.SpoilReportID);
                });

            migrationBuilder.CreateTable(
                name: "StockReports",
                columns: table => new
                {
                    StockReportID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ReportType = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockReports", x => x.StockReportID);
                });

            migrationBuilder.CreateTable(
                name: "Stocks",
                columns: table => new
                {
                    StockID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    Quantity = table.Column<float>(type: "REAL", nullable: false),
                    MinQuantity = table.Column<int>(type: "INTEGER", nullable: false),
                    MaxQuantity = table.Column<int>(type: "INTEGER", nullable: false),
                    MeasureType = table.Column<int>(type: "INTEGER", nullable: false),
                    CategoryID = table.Column<int>(type: "INTEGER", nullable: true),
                    CategoryModelCategoryID = table.Column<int>(type: "INTEGER", nullable: true),
                    StockReportModelStockReportID = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stocks", x => x.StockID);
                    table.ForeignKey(
                        name: "FK_Stocks_Categories_CategoryID",
                        column: x => x.CategoryID,
                        principalTable: "Categories",
                        principalColumn: "CategoryID",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Stocks_Categories_CategoryModelCategoryID",
                        column: x => x.CategoryModelCategoryID,
                        principalTable: "Categories",
                        principalColumn: "CategoryID");
                    table.ForeignKey(
                        name: "FK_Stocks_StockReports_StockReportModelStockReportID",
                        column: x => x.StockReportModelStockReportID,
                        principalTable: "StockReports",
                        principalColumn: "StockReportID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Consumptions",
                columns: table => new
                {
                    ConsumptionID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Quantity = table.Column<float>(type: "REAL", nullable: false),
                    ConsumptionDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    StockID = table.Column<int>(type: "INTEGER", nullable: false),
                    ConsumptionReportModelConsumptionReportID = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Consumptions", x => x.ConsumptionID);
                    table.ForeignKey(
                        name: "FK_Consumptions_ConsumptionReports_ConsumptionReportModelConsumptionReportID",
                        column: x => x.ConsumptionReportModelConsumptionReportID,
                        principalTable: "ConsumptionReports",
                        principalColumn: "ConsumptionReportID");
                    table.ForeignKey(
                        name: "FK_Consumptions_Stocks_StockID",
                        column: x => x.StockID,
                        principalTable: "Stocks",
                        principalColumn: "StockID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    ItemID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ItemDescription = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    SpoilDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Measure = table.Column<float>(type: "REAL", nullable: false),
                    StockID = table.Column<int>(type: "INTEGER", nullable: false),
                    SpoilReportModelSpoilReportID = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.ItemID);
                    table.ForeignKey(
                        name: "FK_Items_SpoilReports_SpoilReportModelSpoilReportID",
                        column: x => x.SpoilReportModelSpoilReportID,
                        principalTable: "SpoilReports",
                        principalColumn: "SpoilReportID");
                    table.ForeignKey(
                        name: "FK_Items_Stocks_StockID",
                        column: x => x.StockID,
                        principalTable: "Stocks",
                        principalColumn: "StockID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ShoppingItens",
                columns: table => new
                {
                    ShoppingItemID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Measure = table.Column<float>(type: "REAL", nullable: false),
                    StockID = table.Column<int>(type: "INTEGER", nullable: false),
                    ShoppingListID = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShoppingItens", x => x.ShoppingItemID);
                    table.ForeignKey(
                        name: "FK_ShoppingItens_ShoppingLists_ShoppingListID",
                        column: x => x.ShoppingListID,
                        principalTable: "ShoppingLists",
                        principalColumn: "ShoppingListID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ShoppingItens_Stocks_StockID",
                        column: x => x.StockID,
                        principalTable: "Stocks",
                        principalColumn: "StockID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Consumptions_ConsumptionReportModelConsumptionReportID",
                table: "Consumptions",
                column: "ConsumptionReportModelConsumptionReportID");

            migrationBuilder.CreateIndex(
                name: "IX_Consumptions_StockID",
                table: "Consumptions",
                column: "StockID");

            migrationBuilder.CreateIndex(
                name: "IX_Items_SpoilReportModelSpoilReportID",
                table: "Items",
                column: "SpoilReportModelSpoilReportID");

            migrationBuilder.CreateIndex(
                name: "IX_Items_StockID",
                table: "Items",
                column: "StockID");

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingItens_ShoppingListID",
                table: "ShoppingItens",
                column: "ShoppingListID");

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingItens_StockID",
                table: "ShoppingItens",
                column: "StockID");

            migrationBuilder.CreateIndex(
                name: "IX_Stocks_CategoryID",
                table: "Stocks",
                column: "CategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_Stocks_CategoryModelCategoryID",
                table: "Stocks",
                column: "CategoryModelCategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_Stocks_StockReportModelStockReportID",
                table: "Stocks",
                column: "StockReportModelStockReportID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Consumptions");

            migrationBuilder.DropTable(
                name: "Items");

            migrationBuilder.DropTable(
                name: "ShoppingItens");

            migrationBuilder.DropTable(
                name: "ConsumptionReports");

            migrationBuilder.DropTable(
                name: "SpoilReports");

            migrationBuilder.DropTable(
                name: "ShoppingLists");

            migrationBuilder.DropTable(
                name: "Stocks");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "StockReports");
        }
    }
}
