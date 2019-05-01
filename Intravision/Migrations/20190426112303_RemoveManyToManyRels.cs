using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace server.Migrations
{
    public partial class RemoveManyToManyRels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DeviceToCoin");

            migrationBuilder.DropTable(
                name: "DeviceToDrink");

            migrationBuilder.AddColumn<int>(
                name: "DeviceId",
                table: "Drinks",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Price",
                table: "Drinks",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "Drinks",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DeviceId",
                table: "Coins",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsBlocked",
                table: "Coins",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "Coins",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Drinks_DeviceId",
                table: "Drinks",
                column: "DeviceId");

            migrationBuilder.CreateIndex(
                name: "IX_Coins_DeviceId",
                table: "Coins",
                column: "DeviceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Coins_Devices_DeviceId",
                table: "Coins",
                column: "DeviceId",
                principalTable: "Devices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Drinks_Devices_DeviceId",
                table: "Drinks",
                column: "DeviceId",
                principalTable: "Devices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Coins_Devices_DeviceId",
                table: "Coins");

            migrationBuilder.DropForeignKey(
                name: "FK_Drinks_Devices_DeviceId",
                table: "Drinks");

            migrationBuilder.DropIndex(
                name: "IX_Drinks_DeviceId",
                table: "Drinks");

            migrationBuilder.DropIndex(
                name: "IX_Coins_DeviceId",
                table: "Coins");

            migrationBuilder.DropColumn(
                name: "DeviceId",
                table: "Drinks");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Drinks");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "Drinks");

            migrationBuilder.DropColumn(
                name: "DeviceId",
                table: "Coins");

            migrationBuilder.DropColumn(
                name: "IsBlocked",
                table: "Coins");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "Coins");

            migrationBuilder.CreateTable(
                name: "DeviceToCoin",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CoinId = table.Column<int>(nullable: true),
                    DeviceId = table.Column<int>(nullable: true),
                    Quantity = table.Column<int>(nullable: false),
                    isBlocked = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeviceToCoin", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DeviceToCoin_Coins_CoinId",
                        column: x => x.CoinId,
                        principalTable: "Coins",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DeviceToCoin_Devices_DeviceId",
                        column: x => x.DeviceId,
                        principalTable: "Devices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DeviceToDrink",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DeviceId = table.Column<int>(nullable: true),
                    DrinkId = table.Column<int>(nullable: true),
                    Price = table.Column<int>(nullable: false),
                    Quantity = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeviceToDrink", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DeviceToDrink_Devices_DeviceId",
                        column: x => x.DeviceId,
                        principalTable: "Devices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DeviceToDrink_Drinks_DrinkId",
                        column: x => x.DrinkId,
                        principalTable: "Drinks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DeviceToCoin_CoinId",
                table: "DeviceToCoin",
                column: "CoinId");

            migrationBuilder.CreateIndex(
                name: "IX_DeviceToCoin_DeviceId",
                table: "DeviceToCoin",
                column: "DeviceId");

            migrationBuilder.CreateIndex(
                name: "IX_DeviceToDrink_DeviceId",
                table: "DeviceToDrink",
                column: "DeviceId");

            migrationBuilder.CreateIndex(
                name: "IX_DeviceToDrink_DrinkId",
                table: "DeviceToDrink",
                column: "DrinkId");
        }
    }
}
