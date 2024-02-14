using csharp_game.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace csharp_game.Data
{
    public class GameContext : IdentityDbContext<ApplicationUser>
    {
        public GameContext(DbContextOptions<GameContext> options)
            : base(options)
        {
        }

        public DbSet<Token> Tokens { get; set; }
        public DbSet<Character> Characters { get; set; }
        public DbSet<Class> Classes { get; set; }
        public DbSet<Stats> Stats { get; set; }
        public DbSet<Monster> Monsters { get; set; }
        public DbSet<Inventory> Inventories { get; set; }
        public DbSet<Item> Items { get; set; }
    }
}
