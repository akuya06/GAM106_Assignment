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

            // ACCOUNTS
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
                var exist = db.Accounts.FirstOrDefault(a => a.Id == acct.Id || a.Username == acct.Username);
                if (exist == null)
                {
                    db.Accounts.Add(acct);
                }
                else
                {
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
                    new Item { Id = "106", Name = "Potion of Healing", Description = "Restores health", BaseValue = 50, MaxStackSize = 16, IsVehicle = false },
                    new Item { Id = "107", Name = "Enchanted Sword", Description = "Powerful weapon", BaseValue = 200, MaxStackSize = 1, IsVehicle = false },
                    new Item { Id = "108", Name = "Fire Resistance Potion", Description = "Protects from fire", BaseValue = 75, MaxStackSize = 16, IsVehicle = false },
                    new Item { Id = "109", Name = "Ender Chest", Description = "Portable storage", BaseValue = 300, MaxStackSize = 1, IsVehicle = false },
                    new Item { Id = "110", Name = "Beacon", Description = "Provides buffs", BaseValue = 500, MaxStackSize = 1, IsVehicle = false },
                    // Vehicles
                    new Item { Id = "201", Name = "Minecart", Description = "Basic transportation vehicle", BaseValue = 100, MaxStackSize = 1, IsVehicle = true },
                    new Item { Id = "202", Name = "Horse", Description = "Fast ground mount", BaseValue = 300, MaxStackSize = 1, IsVehicle = true },
                    new Item { Id = "203", Name = "Elytra", Description = "Flying wings", BaseValue = 1000, MaxStackSize = 1, IsVehicle = true },
                    new Item { Id = "204", Name = "Boat", Description = "Water transportation", BaseValue = 50, MaxStackSize = 1, IsVehicle = true },
                    new Item { Id = "205", Name = "Strider", Description = "Lava mount", BaseValue = 500, MaxStackSize = 1, IsVehicle = true }
                };
                db.Items.AddRange(items);
                db.SaveChanges();
                Console.WriteLine($"DataSeeder: Added {items.Length} items");
            }

            // PLAYERS
            if (!db.Players.Any(p => p.Id == 1))
            {
                var players = new[]
                {
                    new Player { /* KHÔNG gán Id, để tự động tăng */ AccountId = "1", Realm = 10, Hunger = 80, Xp = 5000, CreatedAt = DateTime.UtcNow },
                    new Player { /* KHÔNG gán Id, để tự động tăng */ AccountId = "2", Realm = 12, Hunger = 90, Xp = 3000, CreatedAt = DateTime.UtcNow },
                    new Player { /* KHÔNG gán Id, để tự động tăng */ AccountId = "3", Realm = 99, Hunger = 999, Xp = 10000, CreatedAt = DateTime.UtcNow },
                    new Player { /* KHÔNG gán Id, để tự động tăng */ AccountId = "4", Realm = 20, Hunger = 100, Xp = 1500, CreatedAt = DateTime.UtcNow },
                    new Player { /* KHÔNG gán Id, để tự động tăng */ AccountId = "5", Realm = 15, Hunger = 85, Xp = 2000, CreatedAt = DateTime.UtcNow }
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

            // RECIPES
            if (!db.Recipes.Any(r => r.Id == "1"))
            {
                var recipes = new[]
                {
                   new Recipe { Id = "1", Name = "Potion of Healing", ResultItemId = "106", ResultQuantity = 1 },
                   new Recipe { Id = "2", Name = "Enchanted Sword", ResultItemId = "107", ResultQuantity = 1 },
                   new Recipe { Id = "3", Name = "Fire Resistance Potion", ResultItemId = "108", ResultQuantity = 1 },
                   new Recipe { Id = "4", Name = "Ender Chest", ResultItemId = "109", ResultQuantity = 1 },
                   new Recipe { Id = "5", Name = "Beacon", ResultItemId = "110", ResultQuantity = 1 }
                };
                db.Recipes.AddRange(recipes);
                db.SaveChanges();
            }

            // QUESTS
            if (!db.Quests.Any(q => q.Id == "201"))
            {
                var quests = new[]
                {
                   new Quest { Id = "201", Name = "Find the Nether Portal", Description = "Locate and activate a portal to the Nether.", RewardItemId = "103", RewardXp = 50 },
                   new Quest { Id = "202", Name = "Defeat the Ender Dragon", Description = "Slay the final boss in the End.", RewardItemId = "104", RewardXp = 200 },
                   new Quest { Id = "203", Name = "Collect Blaze Rods", Description = "Gather 10 Blaze Rods from the Nether Fortress.", RewardItemId = "103", RewardXp = 75 },
                   new Quest { Id = "204", Name = "Build a Beacon", Description = "Craft and activate a beacon.", RewardItemId = "110", RewardXp = 150 },
                   new Quest { Id = "205", Name = "Survive the Warden", Description = "Escape the Deep Dark after encountering the Warden.", RewardItemId = "105", RewardXp = 300 }
                };
                db.Quests.AddRange(quests);
                db.SaveChanges();
            }

            // GAME MODES
            if (!db.GameModes.Any(gm => gm.Id == "1"))
            {
                var gms = new[]
                {
                   new GameMode { Id = "1", Name = "Survival", Description = "Gather resources and survive.", CanInteractWithShop = true, CanUseRecipes = true },
                   new GameMode { Id = "2", Name = "Creative", Description = "Unlimited resources and flight.", CanInteractWithShop = false, CanUseRecipes = false },
                   new GameMode { Id = "3", Name = "Adventure", Description = "Play custom maps with rules.", CanInteractWithShop = true, CanUseRecipes = true },
                   new GameMode { Id = "4", Name = "Hardcore", Description = "Survival with permadeath.", CanInteractWithShop = true, CanUseRecipes = true },
                   new GameMode { Id = "5", Name = "PvP Arena", Description = "Battle other players.", CanInteractWithShop = true, CanUseRecipes = false }
                };
                db.GameModes.AddRange(gms);
                db.SaveChanges();
            }

            // SHOP ITEMS - SAU KHI ĐÃ CÓ ITEMS
            if (!db.Shops.Any(s => s.Id == "S1"))
            {
                var shopItems = new[]
                {
                    // Items thường
                    new Shop { Id = "S1", ItemId = "101", PriceXp = 100, AllowedModes = "Survival,Adventure,Hardcore", IsVehicle = false },
                    new Shop { Id = "S2", ItemId = "102", PriceXp = 500, AllowedModes = "Survival,Adventure", IsVehicle = false },
                    new Shop { Id = "S3", ItemId = "103", PriceXp = 150, AllowedModes = "Survival,Hardcore", IsVehicle = false },
                    new Shop { Id = "S4", ItemId = "104", PriceXp = 300, AllowedModes = "Survival,Adventure,Hardcore", IsVehicle = false },
                    new Shop { Id = "S5", ItemId = "105", PriceXp = 1000, AllowedModes = "Survival,Hardcore", IsVehicle = false },
                    new Shop { Id = "S6", ItemId = "106", PriceXp = 50, AllowedModes = "Survival,Adventure", IsVehicle = false },
                    new Shop { Id = "S7", ItemId = "107", PriceXp = 200, AllowedModes = "Survival,Adventure,Hardcore", IsVehicle = false },
                    new Shop { Id = "S8", ItemId = "108", PriceXp = 75, AllowedModes = "Survival,Hardcore", IsVehicle = false },
                    new Shop { Id = "S9", ItemId = "109", PriceXp = 300, AllowedModes = "Survival,Adventure", IsVehicle = false },
                    new Shop { Id = "S10", ItemId = "110", PriceXp = 500, AllowedModes = "Survival", IsVehicle = false },
                    
                    // Vehicles
                    new Shop { Id = "V1", ItemId = "201", PriceXp = 100, AllowedModes = "Survival,Adventure", IsVehicle = true },
                    new Shop { Id = "V2", ItemId = "202", PriceXp = 300, AllowedModes = "Survival,Adventure,Hardcore", IsVehicle = true },
                    new Shop { Id = "V3", ItemId = "203", PriceXp = 1000, AllowedModes = "Survival", IsVehicle = true },
                    new Shop { Id = "V4", ItemId = "204", PriceXp = 50, AllowedModes = "Survival,Adventure", IsVehicle = true },
                    new Shop { Id = "V5", ItemId = "205", PriceXp = 500, AllowedModes = "Survival,Hardcore", IsVehicle = true }
                };
                db.Shops.AddRange(shopItems);
                db.SaveChanges();
                Console.WriteLine($"DataSeeder: Added {shopItems.Length} shop items");
            }

            // PLAYER INVENTORY
            if (!db.PlayerInventory.Any(pi => pi.PlayerId == 1 && pi.ItemId == "101"))
            {
                var inv = new[]
                {
                    new PlayerInventory { PlayerId = 1, Kind = InventoryKind.Item, ItemId = "101", Quantity = 1, SlotIndex = 0 },
                    new PlayerInventory { PlayerId = 1, Kind = InventoryKind.Item, ItemId = "102", Quantity = 3, SlotIndex = 1 },
                    new PlayerInventory { PlayerId = 2, Kind = InventoryKind.Item, ItemId = "103", Quantity = 2, SlotIndex = 0 },
                    new PlayerInventory { PlayerId = 3, Kind = InventoryKind.Item, ItemId = "104", Quantity = 5, SlotIndex = 0 },
                    new PlayerInventory { PlayerId = 4, Kind = InventoryKind.Item, ItemId = "105", Quantity = 1, SlotIndex = 0 },
                    new PlayerInventory { PlayerId = 1, Kind = InventoryKind.Vehicle, ItemId = "201", Quantity = 1, SlotIndex = null },
                    new PlayerInventory { PlayerId = 3, Kind = InventoryKind.Vehicle, ItemId = "203", Quantity = 1, SlotIndex = null }
                };
                db.PlayerInventory.AddRange(inv);
                db.SaveChanges();
            }

            // SHOP TRANSACTIONS
            if (!db.ShopTransactions.Any(st => st.Id == "T1"))
            {
                var transactions = new[]
                {
                    new ShopTransaction { Id = "T1", PlayerId = 1, ShopId = "V1", PricePaid = 100, PurchasedAt = ParseTs("2025-11-15 10:00:00") },
                    new ShopTransaction { Id = "T2", PlayerId = 1, ShopId = "S2", PricePaid = 500, PurchasedAt = ParseTs("2025-11-15 11:00:00") },
                    new ShopTransaction { Id = "T3", PlayerId = 2, ShopId = "S3", PricePaid = 150, PurchasedAt = ParseTs("2025-11-16 09:00:00") },
                    new ShopTransaction { Id = "T4", PlayerId = 3, ShopId = "V3", PricePaid = 1000, PurchasedAt = ParseTs("2025-11-17 14:00:00") },
                    new ShopTransaction { Id = "T5", PlayerId = 3, ShopId = "S5", PricePaid = 1000, PurchasedAt = ParseTs("2025-11-17 15:00:00") },
                    new ShopTransaction { Id = "T6", PlayerId = 4, ShopId = "V4", PricePaid = 50, PurchasedAt = ParseTs("2025-11-18 10:30:00") },
                    new ShopTransaction { Id = "T7", PlayerId = 5, ShopId = "S1", PricePaid = 100, PurchasedAt = ParseTs("2025-11-19 12:00:00") }
                };
                db.ShopTransactions.AddRange(transactions);
                db.SaveChanges();
            }

            // assign player current mode
            var p1m = db.Players.Find(1);
            if (p1m != null && string.IsNullOrEmpty(p1m.CurrentModeId))
            {
                p1m.CurrentModeId = "1";
            }
            var p2m = db.Players.Find(2);
            if (p2m != null && string.IsNullOrEmpty(p2m.CurrentModeId))
            {
                p2m.CurrentModeId = "2";
            }
            var p3m = db.Players.Find(3);
            if (p3m != null && string.IsNullOrEmpty(p3m.CurrentModeId))
            {
                p3m.CurrentModeId = "5";
            }
            var p4m = db.Players.Find(4);
            if (p4m != null && string.IsNullOrEmpty(p4m.CurrentModeId))
            {
                p4m.CurrentModeId = "3";
            }
            var p5m = db.Players.Find(5);
            if (p5m != null && string.IsNullOrEmpty(p5m.CurrentModeId))
            {
                p5m.CurrentModeId = "1";
            }
            db.SaveChanges();

            // PLAYER MONSTER KILLS
            if (!db.PlayerMonsterKills.Any(k => k.Id == 1))
            {
                var kills = new[]
                {
                    new PlayerMonsterKill { Id = 1, PlayerId = 1, MonsterId = "1", KilledAt = ParseTs("2025-11-10 10:00:00") },
                    new PlayerMonsterKill { Id = 2, PlayerId = 2, MonsterId = "2", KilledAt = ParseTs("2025-11-10 10:05:00") },
                    new PlayerMonsterKill { Id = 3, PlayerId = 3, MonsterId = "3", KilledAt = ParseTs("2025-11-10 10:10:00") },
                    new PlayerMonsterKill { Id = 4, PlayerId = 4, MonsterId = "4", KilledAt = ParseTs("2025-11-10 10:15:00") },
                    new PlayerMonsterKill { Id = 5, PlayerId = 5, MonsterId = "5", KilledAt = ParseTs("2025-11-10 10:20:00") }
                };
                db.PlayerMonsterKills.AddRange(kills);
                db.SaveChanges();
            }

            // PLAYER QUESTS
            if (!db.PlayerQuests.Any(q => q.Id == "Q1"))
            {
                var quests = new[]
                {
                    new PlayerQuest { Id = "Q1", PlayerId = 1, QuestId = "201", CompletedAt = ParseTs("2025-11-20 10:00:00") },
                    new PlayerQuest { Id = "Q2", PlayerId = 2, QuestId = "202", CompletedAt = ParseTs("2025-11-21 11:00:00") }
                };
                db.PlayerQuests.AddRange(quests);
                db.SaveChanges();
            }

            Console.WriteLine($"DataSeeder: accounts = {db.Accounts.Count()}");
            Console.WriteLine($"DataSeeder: items = {db.Items.Count()}");
            Console.WriteLine($"DataSeeder: shops = {db.Shops.Count()}");
            Console.WriteLine($"DataSeeder: transactions = {db.ShopTransactions.Count()}");
            Console.WriteLine("DataSeeder: complete");
        }
    }
}