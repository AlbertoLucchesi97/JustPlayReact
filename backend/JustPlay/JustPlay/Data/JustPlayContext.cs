using JustPlay.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace JustPlay.Data
{
    public class JustPlayContext : DbContext
    {
        public JustPlayContext(DbContextOptions<JustPlayContext> options) : base(options)
        {
        }

        public DbSet<Videogame> Videogames { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<VideogameOwned> VideogamesOwned { get; set; }
        public DbSet<VideogameWishlist> VideogamesWishlist { get; set; }
    }
}
