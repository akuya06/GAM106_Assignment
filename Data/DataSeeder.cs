using WebApplication1.Models;
using System;
using System.Linq;
using System.Globalization;

namespace WebApplication1.Data
{
    public static class DataSeeder
    {
        public static void Seed(ApplicationDbContext db)
        {
            Console.WriteLine("DataSeeder: start");

            DateTime ParseTs(string s)
            {
                if (DateTime.TryParse(s, CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out var dt))
                    return dt.ToUniversalTime();
                return DateTime.UtcNow;
            }

            // ACCOUNTS (idempotent upsert)
            var seedAccounts = new[]
            {
                new Account { Id = "1", Username = "steve123", Email = "steve@example.com", PasswordHash = "hash1", CreatedAt = ParseTs("2025-11-01 10:00:00") },
                new Account { Id = "2", Username = "alex456", Email = "alex@example.com", PasswordHash = "hash2", CreatedAt = ParseTs("2025-11-02 11:00:00") },
                new Account { Id = "3", Username = "hero99", Email = "hero@example.com", PasswordHash = "hash3", CreatedAt = ParseTs("2025-11-03 12:00:00") },
                new Account { Id = "4", Username = "enderx", Email = "ender@example.com", PasswordHash = "hash4", CreatedAt = ParseTs("2025-11-04 13:00:00") },
                new Account { Id = "5", Username = "creepz", Email = "creep@example.com", PasswordHash = "hash5", CreatedAt = ParseTs("2025-11-05 14:00:00") }
            };

            foreach (var acct in seedAccounts)
            {
                // tìm theo Id hoặc Username (unique)
                var exist = db.Accounts.FirstOrDefault(a => a.Id == acct.Id || a.Username == acct.Username);
                if (exist == null)
                {
                    db.Accounts.Add(acct);
                }
                else
                {
                    // cập nhật các trường nếu cần
                    exist.Email = acct.Email;
                    exist.PasswordHash = acct.PasswordHash;
                    exist.CreatedAt ??= acct.CreatedAt;
                }
            }
            db.SaveChanges();

            // ITEMS
            if (!db.Items.Any(i => i.Id == "101"))
            {
                var items = new[]
                {
                    new Item { Id = "101", Name = "Iron Sword", Description = "Weapon - Common", BaseValue = 100, MaxStackSize = 5, IsVehicle = false },
                    new Item { Id = "102", Name = "Golden Apple", Description = "Consumable - Rare", BaseValue = 500, MaxStackSize = 10, IsVehicle = false },
                    new Item { Id = "103", Name = "Blaze Rod", Description = "Crafting - Uncommon", BaseValue = 150, MaxStackSize = 8, IsVehicle = false },
                    new Item { Id = "104", Name = "Ender Pearl", Description = "Teleport - Rare", BaseValue = 300, MaxStackSize = 12, IsVehicle = false },
                    new Item { Id = "105", Name = "Totem of Undying", Description = "Artifact - Legendary", BaseValue = 1000, MaxStackSize = 20, IsVehicle = false },
                    // items referenced by recipes (106-110) - create minimal entries
                    new Item { Id = "106", Name = "Potion of Healing", BaseValue = 0, MaxStackSize = 16 },
                    new Item { Id = "107", Name = "Enchanted Sword", BaseValue = 0, MaxStackSize = 1 },
                    new Item { Id = "108", Name = "Fire Resistance Potion", BaseValue = 0, MaxStackSize = 16 },
                    new Item { Id = "109", Name = "Ender Chest", BaseValue = 0, MaxStackSize = 1 },
                    new Item { Id = "110", Name = "Beacon", BaseValue = 0, MaxStackSize = 1 }
                };
                db.Items.AddRange(items);
                db.SaveChanges();
            }

            // PLAYERS (map player -> account by id)
            if (!db.Players.Any(p => p.Id == "1"))
            {
                var players = new[]
                {
                    new Player { Id = "1", AccountId = "1", Realm = 10, Hunger = 80, Xp = 100, CreatedAt = DateTime.UtcNow },
                    new Player { Id = "2", AccountId = "2", Realm = 12, Hunger = 90, Xp = 100, CreatedAt = DateTime.UtcNow },
                    new Player { Id = "3", AccountId = "3", Realm = 99, Hunger = 999, Xp = 999, CreatedAt = DateTime.UtcNow },
                    new Player { Id = "4", AccountId = "4", Realm = 20, Hunger = 100, Xp = 120, CreatedAt = DateTime.UtcNow },
                    new Player { Id = "5", AccountId = "5", Realm = 15, Hunger = 85, Xp = 100, CreatedAt = DateTime.UtcNow }
                };
                db.Players.AddRange(players);
                db.SaveChanges();
            }

            // MONSTERS
            if (!db.Monsters.Any(m => m.Id == "1"))
            {
                var monsters = new[]
                {
                    new Monster { Id = "1", Name = "Zombie", RewardXp = 5, RewardQuantity = 1, RewardItemId = "101" },
                    new Monster { Id = "2", Name = "Skeleton", RewardXp = 6, RewardQuantity = 1, RewardItemId = "102" },
                    new Monster { Id = "3", Name = "Blaze", RewardXp = 10, RewardQuantity = 2, RewardItemId = "103" },
                    new Monster { Id = "4", Name = "Enderman", RewardXp = 15, RewardQuantity = 3, RewardItemId = "104" },
                    new Monster { Id = "5", Name = "Warden", RewardXp = 50, RewardQuantity = 4, RewardItemId = "105" }
                };
                db.Monsters.AddRange(monsters);
                db.SaveChanges();
            }

            // RESOURCES
            if (!db.Resources.Any(r => r.Id == "1"))
            {
                var resources = new[]
                {
                    new Resource { Id = "1", Name = "Coal", Description = "Mineral - Mountains" },
                    new Resource { Id = "2", Name = "Iron Ore", Description = "Mineral - Caves" },
                    new Resource { Id = "3", Name = "Diamond", Description = "Mineral - Deep Slate" },
                    new Resource { Id = "4", Name = "Redstone", Description = "Mineral - Underground" },
                    new Resource { Id = "5", Name = "Lapis Lazuli", Description = "Mineral - Ocean Ruins" }
                };
                db.Resources.AddRange(resources);
                db.SaveChanges();
            }

            // PLAYER INVENTORY (player_id, item_id, qty)
            // using ItemId property (AssetId renamed -> ItemId)
            if (!db.PlayerInventory.Any(pi => pi.PlayerId == "1" && pi.ItemId == "101"))
            {
                var inv = new[]
                {
                    new PlayerInventory { Id = Guid.NewGuid().ToString(), PlayerId = "1", Kind = InventoryKind.Item, ItemId = "101", Quantity = 1, SlotIndex = 0 },
                    new PlayerInventory { Id = Guid.NewGuid().ToString(), PlayerId = "1", Kind = InventoryKind.Item, ItemId = "102", Quantity = 3, SlotIndex = 1 },
                    new PlayerInventory { Id = Guid.NewGuid().ToString(), PlayerId = "2", Kind = InventoryKind.Item, ItemId = "103", Quantity = 2, SlotIndex = 0 },
                    new PlayerInventory { Id = Guid.NewGuid().ToString(), PlayerId = "3", Kind = InventoryKind.Item, ItemId = "104", Quantity = 5, SlotIndex = 0 },
                    new PlayerInventory { Id = Guid.NewGuid().ToString(), PlayerId = "4", Kind = InventoryKind.Item, ItemId = "105", Quantity = 1, SlotIndex = 0 }
                };
                db.PlayerInventory.AddRange(inv);
                db.SaveChanges();
            }

            // RECIPES
            if (!db.Recipes.Any(r => r.Id == "1"))
            {
                var recipes = new[]
                {
                    new Recipe { Id = "1", ItemId = "106", OutputQuantity = 1 },
                    new Recipe { Id = "2", ItemId = "107", OutputQuantity = 1 },
                    new Recipe { Id = "3", ItemId = "108", OutputQuantity = 1 },
                    new Recipe { Id = "4", ItemId = "109", OutputQuantity = 1 },
                    new Recipe { Id = "5", ItemId = "110", OutputQuantity = 1 }
                };
                db.Recipes.AddRange(recipes);
                db.SaveChanges();
            }

            // RECIPE INGREDIENTS (some entries)
            // Use db.Set<RecipeIngredient>() in case DbSet property is absent
            var recipeIngredientSet = db.Set<RecipeIngredient>();
            if (!recipeIngredientSet.Any(ri => ri.RecipeId == "1"))
            {
                // Map ingredients to existing resources (ids "1".."5") to avoid FK violations.
                var recipeIngredients = new[]
                {
                    new RecipeIngredient { Id = Guid.NewGuid().ToString(), RecipeId = "1", ResourceId = "1", Quantity = 1 },
                    new RecipeIngredient { Id = Guid.NewGuid().ToString(), RecipeId = "1", ResourceId = "2", Quantity = 1 },
                    new RecipeIngredient { Id = Guid.NewGuid().ToString(), RecipeId = "2", ResourceId = "3", Quantity = 1 },
                    new RecipeIngredient { Id = Guid.NewGuid().ToString(), RecipeId = "2", ResourceId = "4", Quantity = 1 },
                    new RecipeIngredient { Id = Guid.NewGuid().ToString(), RecipeId = "3", ResourceId = "2", Quantity = 2 }
                };
                recipeIngredientSet.AddRange(recipeIngredients);
                db.SaveChanges();
            }

            // QUESTS
            if (!db.Quests.Any(q => q.Id == "201"))
            {
                var quests = new[]
                {
                    new Quest { Id = "201", Name = "Find the Nether Portal", Description = "Locate and activate a portal to the Nether.", RewardItemId = "103", RewardXp = 0, RewardQuantity = 1 },
                    new Quest { Id = "202", Name = "Defeat the Ender Dragon", Description = "Slay the final boss in the End.", RewardItemId = "104", RewardXp = 0, RewardQuantity = 1 },
                    new Quest { Id = "203", Name = "Collect Blaze Rods", Description = "Gather 10 Blaze Rods from the Nether Fortress.", RewardItemId = "103", RewardXp = 0, RewardQuantity = 10 },
                    new Quest { Id = "204", Name = "Build a Beacon", Description = "Craft and activate a beacon.", RewardItemId = "110", RewardXp = 0, RewardQuantity = 1 },
                    new Quest { Id = "205", Name = "Survive the Warden", Description = "Escape the Deep Dark after encountering the Warden.", RewardItemId = "105", RewardXp = 0, RewardQuantity = 1 }
                };
                db.Quests.AddRange(quests);
                db.SaveChanges();
            }

            // PLAYER QUESTS
            if (!db.PlayerQuests.Any(pq => pq.PlayerId == "1" && pq.QuestId == "201"))
            {
                var playerQuests = new[]
                {
                    new PlayerQuest { Id = Guid.NewGuid().ToString(), PlayerId = "1", QuestId = "201", CompletedAt = null },
                    new PlayerQuest { Id = Guid.NewGuid().ToString(), PlayerId = "2", QuestId = "202", CompletedAt = DateTime.UtcNow },
                    new PlayerQuest { Id = Guid.NewGuid().ToString(), PlayerId = "3", QuestId = "203", CompletedAt = null },
                    new PlayerQuest { Id = Guid.NewGuid().ToString(), PlayerId = "4", QuestId = "204", CompletedAt = null },
                    new PlayerQuest { Id = Guid.NewGuid().ToString(), PlayerId = "5", QuestId = "205", CompletedAt = DateTime.UtcNow }
                };
                db.PlayerQuests.AddRange(playerQuests);
                db.SaveChanges();
            }

            // GAME MODES
            if (!db.GameModes.Any(gm => gm.Id == "1"))
            {
                var gms = new[]
                {
                    new GameMode { Id = "1", Name = "Survival", Description = "Gather resources and survive.", IsSurvival = true },
                    new GameMode { Id = "2", Name = "Creative", Description = "Unlimited resources and flight.", IsCreative = true },
                    new GameMode { Id = "3", Name = "Adventure", Description = "Play custom maps with rules.", IsAdventure = true },
                    new GameMode { Id = "4", Name = "Hardcore", Description = "Survival with permadeath.", IsSurvival = false, IsCreative = false },
                    new GameMode { Id = "5", Name = "PvP Arena", Description = "Battle other players.", IsSurvival = true, IsCreative = true }
                };
                db.GameModes.AddRange(gms);
                db.SaveChanges();
            }

            // assign player current mode from provided player_game_modes mapping (1->1 etc)
            var p1m = db.Players.Find("1");
            if (p1m != null && string.IsNullOrEmpty(p1m.CurrentModeId))
            {
                p1m.CurrentModeId = "1";
            }
            var p2m = db.Players.Find("2");
            if (p2m != null && string.IsNullOrEmpty(p2m.CurrentModeId))
            {
                p2m.CurrentModeId = "2";
            }
            var p3m = db.Players.Find("3");
            if (p3m != null && string.IsNullOrEmpty(p3m.CurrentModeId))
            {
                p3m.CurrentModeId = "5";
            }
            var p4m = db.Players.Find("4");
            if (p4m != null && string.IsNullOrEmpty(p4m.CurrentModeId))
            {
                p4m.CurrentModeId = "3";
            }
            var p5m = db.Players.Find("5");
            if (p5m != null && string.IsNullOrEmpty(p5m.CurrentModeId))
            {
                p5m.CurrentModeId = "1";
            }
            db.SaveChanges();

            // PLAYER MONSTER KILLS
            if (!db.PlayerMonsterKills.Any(k => k.Id == "1"))
            {
                var kills = new[]
                {
                    new PlayerMonsterKill { Id = "1", PlayerId = "1", MonsterId = "1", KilledAt = ParseTs("2025-11-10 10:00:00") },
                    new PlayerMonsterKill { Id = "2", PlayerId = "2", MonsterId = "2", KilledAt = ParseTs("2025-11-10 10:05:00") },
                    new PlayerMonsterKill { Id = "3", PlayerId = "3", MonsterId = "3", KilledAt = ParseTs("2025-11-10 10:10:00") },
                    new PlayerMonsterKill { Id = "4", PlayerId = "4", MonsterId = "4", KilledAt = ParseTs("2025-11-10 10:15:00") },
                    new PlayerMonsterKill { Id = "5", PlayerId = "5", MonsterId = "5", KilledAt = ParseTs("2025-11-10 10:20:00") }
                };
                db.PlayerMonsterKills.AddRange(kills);
                db.SaveChanges();
            }

            Console.WriteLine($"DataSeeder: accounts = {db.Accounts.Count()}");
        }
    }
}