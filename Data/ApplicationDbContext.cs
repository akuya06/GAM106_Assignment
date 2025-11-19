using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<GameMode> GameModes { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Shop> Shop { get; set; }
        public DbSet<Monster> Monsters { get; set; }
        public DbSet<Quest> Quests { get; set; }
        public DbSet<Resource> Resources { get; set; }
        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<PlayerMonsterKill> PlayerMonsterKills { get; set; }
        public DbSet<PlayerInventory> PlayerInventory { get; set; }
        public DbSet<PlayerQuest> PlayerQuests { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<PlayerMonsterKill>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasOne(e => e.Player)
                      .WithMany(p => p.MonsterKills)
                      .HasForeignKey(e => e.PlayerId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.Monster)
                      .WithMany()
                      .HasForeignKey(e => e.MonsterId)
                      .OnDelete(DeleteBehavior.SetNull);
            });

            modelBuilder.Entity<Account>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Username).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Email).IsRequired().HasMaxLength(200);
                entity.Property(e => e.PasswordHash).IsRequired();
            });

            // PlayerInventory uses Id as PK (model defines Id)
            modelBuilder.Entity<PlayerInventory>(entity =>
            {
                entity.HasKey(pi => pi.Id);
                entity.Property(pi => pi.Quantity).IsRequired();
                entity.HasOne(pi => pi.Player)
                      .WithMany(p => p.PlayerInventory)
                      .HasForeignKey(pi => pi.PlayerId)
                      .OnDelete(DeleteBehavior.Cascade);
                // optional navigations to Item/Resource left as-is (nullable)
            });

            // PlayerQuest uses Id as PK (model defines Id)
            modelBuilder.Entity<PlayerQuest>(entity =>
            {
                entity.HasKey(pq => pq.Id);
                entity.HasOne(pq => pq.Player)
                      .WithMany(p => p.PlayerQuests)
                      .HasForeignKey(pq => pq.PlayerId)
                      .OnDelete(DeleteBehavior.Cascade);
                entity.HasOne(pq => pq.Quest)
                      .WithMany()
                      .HasForeignKey(pq => pq.QuestId)
                      .OnDelete(DeleteBehavior.SetNull);
            });

            // Additional configurations can be added here
        }
    }
}
