using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace RRInventory.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Account",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Address1 = table.Column<string>(nullable: true),
                    Address2 = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    State = table.Column<string>(nullable: true),
                    SubscriptionEndDate = table.Column<DateTime>(nullable: false),
                    Zipcode = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Account", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Image",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Jpg = table.Column<string>(nullable: true),
                    Thumbnail = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Image", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Item",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    AccountID = table.Column<Guid>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    ImageID = table.Column<Guid>(nullable: false),
                    Manufacturer = table.Column<string>(nullable: true),
                    ModelNumber = table.Column<string>(nullable: true),
                    Railroad = table.Column<string>(nullable: true),
                    Type = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Item", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Item_Account_AccountID",
                        column: x => x.AccountID,
                        principalTable: "Account",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Item_Image_ImageID",
                        column: x => x.ImageID,
                        principalTable: "Image",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Item_AccountID",
                table: "Item",
                column: "AccountID");

            migrationBuilder.CreateIndex(
                name: "IX_Item_ImageID",
                table: "Item",
                column: "ImageID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Item");

            migrationBuilder.DropTable(
                name: "Account");

            migrationBuilder.DropTable(
                name: "Image");
        }
    }
}
