using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication1.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Username = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    PasswordHash = table.Column<string>(type: "TEXT", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: true),
                    CreatedIp = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GameModes",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    CanInteractWithShop = table.Column<bool>(type: "INTEGER", nullable: false),
                    CanUseRecipes = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameModes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    ImageUrl = table.Column<string>(type: "TEXT", nullable: true),
                    BaseValue = table.Column<int>(type: "INTEGER", nullable: true),
                    MaxStackSize = table.Column<int>(type: "INTEGER", nullable: true),
                    IsSpecial = table.Column<bool>(type: "INTEGER", nullable: false),
                    AllowedModes = table.Column<string>(type: "TEXT", nullable: true),
                    IsVehicle = table.Column<bool>(type: "INTEGER", nullable: false),
                    IconUrl = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Resources",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    IconUrl = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Resources", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Players",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    AccountId = table.Column<string>(type: "TEXT", nullable: true),
                    CurrentModeId = table.Column<string>(type: "TEXT", nullable: true),
                    Realm = table.Column<int>(type: "INTEGER", nullable: true),
                    Hunger = table.Column<int>(type: "INTEGER", nullable: true),
                    Xp = table.Column<int>(type: "INTEGER", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Players", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Players_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Players_GameModes_CurrentModeId",
                        column: x => x.CurrentModeId,
                        principalTable: "GameModes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Monsters",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    RewardItemId = table.Column<string>(type: "TEXT", nullable: true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    RewardXp = table.Column<int>(type: "INTEGER", nullable: true),
                    RewardQuantity = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Monsters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Monsters_Items_RewardItemId",
                        column: x => x.RewardItemId,
                        principalTable: "Items",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Quests",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    RewardXp = table.Column<int>(type: "INTEGER", nullable: true),
                    RewardItemId = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Quests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Quests_Items_RewardItemId",
                        column: x => x.RewardItemId,
                        principalTable: "Items",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Recipes",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    ResultItemId = table.Column<string>(type: "TEXT", nullable: true),
                    ResultQuantity = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recipes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Recipes_Items_ResultItemId",
                        column: x => x.ResultItemId,
                        principalTable: "Items",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Shops",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    PriceXp = table.Column<int>(type: "INTEGER", nullable: true),
                    AllowedModes = table.Column<string>(type: "TEXT", nullable: true),
                    IsVehicle = table.Column<bool>(type: "INTEGER", nullable: false),
                    ItemId = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shops", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Shops_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PlayerInventory",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    PlayerId = table.Column<string>(type: "TEXT", nullable: true),
                    Kind = table.Column<int>(type: "INTEGER", nullable: false),
                    ItemId = table.Column<string>(type: "TEXT", nullable: true),
                    Quantity = table.Column<int>(type: "INTEGER", nullable: false),
                    SlotIndex = table.Column<int>(type: "INTEGER", nullable: true),
                    ResourceId = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerInventory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlayerInventory_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PlayerInventory_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlayerInventory_Resources_ResourceId",
                        column: x => x.ResourceId,
                        principalTable: "Resources",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PlayerMonsterKills",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    PlayerId = table.Column<string>(type: "TEXT", nullable: true),
                    MonsterId = table.Column<string>(type: "TEXT", nullable: true),
                    KilledAt = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerMonsterKills", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlayerMonsterKills_Monsters_MonsterId",
                        column: x => x.MonsterId,
                        principalTable: "Monsters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_PlayerMonsterKills_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PlayerQuests",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    PlayerId = table.Column<string>(type: "TEXT", nullable: true),
                    QuestId = table.Column<string>(type: "TEXT", nullable: true),
                    CompletedAt = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerQuests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlayerQuests_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlayerQuests_Quests_QuestId",
                        column: x => x.QuestId,
                        principalTable: "Quests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "RecipeIngredient",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    RecipeId = table.Column<string>(type: "TEXT", nullable: true),
                    ResourceId = table.Column<string>(type: "TEXT", nullable: true),
                    Quantity = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecipeIngredient", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RecipeIngredient_Recipes_RecipeId",
                        column: x => x.RecipeId,
                        principalTable: "Recipes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RecipeIngredient_Resources_ResourceId",
                        column: x => x.ResourceId,
                        principalTable: "Resources",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ShopTransactions",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    PlayerId = table.Column<string>(type: "TEXT", nullable: true),
                    ShopId = table.Column<string>(type: "TEXT", nullable: true),
                    PricePaid = table.Column<int>(type: "INTEGER", nullable: false),
                    PurchasedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShopTransactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShopTransactions_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ShopTransactions_Shops_ShopId",
                        column: x => x.ShopId,
                        principalTable: "Shops",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Monsters_RewardItemId",
                table: "Monsters",
                column: "RewardItemId");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerInventory_ItemId",
                table: "PlayerInventory",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerInventory_PlayerId",
                table: "PlayerInventory",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerInventory_ResourceId",
                table: "PlayerInventory",
                column: "ResourceId");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerMonsterKills_MonsterId",
                table: "PlayerMonsterKills",
                column: "MonsterId");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerMonsterKills_PlayerId",
                table: "PlayerMonsterKills",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerQuests_PlayerId",
                table: "PlayerQuests",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerQuests_QuestId",
                table: "PlayerQuests",
                column: "QuestId");

            migrationBuilder.CreateIndex(
                name: "IX_Players_AccountId",
                table: "Players",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Players_CurrentModeId",
                table: "Players",
                column: "CurrentModeId");

            migrationBuilder.CreateIndex(
                name: "IX_Quests_RewardItemId",
                table: "Quests",
                column: "RewardItemId");

            migrationBuilder.CreateIndex(
                name: "IX_RecipeIngredient_RecipeId",
                table: "RecipeIngredient",
                column: "RecipeId");

            migrationBuilder.CreateIndex(
                name: "IX_RecipeIngredient_ResourceId",
                table: "RecipeIngredient",
                column: "ResourceId");

            migrationBuilder.CreateIndex(
                name: "IX_Recipes_ResultItemId",
                table: "Recipes",
                column: "ResultItemId");

            migrationBuilder.CreateIndex(
                name: "IX_Shops_ItemId",
                table: "Shops",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_ShopTransactions_PlayerId",
                table: "ShopTransactions",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_ShopTransactions_ShopId",
                table: "ShopTransactions",
                column: "ShopId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PlayerInventory");

            migrationBuilder.DropTable(
                name: "PlayerMonsterKills");

            migrationBuilder.DropTable(
                name: "PlayerQuests");

            migrationBuilder.DropTable(
                name: "RecipeIngredient");

            migrationBuilder.DropTable(
                name: "ShopTransactions");

            migrationBuilder.DropTable(
                name: "Monsters");

            migrationBuilder.DropTable(
                name: "Quests");

            migrationBuilder.DropTable(
                name: "Recipes");

            migrationBuilder.DropTable(
                name: "Resources");

            migrationBuilder.DropTable(
                name: "Players");

            migrationBuilder.DropTable(
                name: "Shops");

            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropTable(
                name: "GameModes");

            migrationBuilder.DropTable(
                name: "Items");
        }
    }
}
