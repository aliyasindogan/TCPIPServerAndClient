using Entities.Concrete.Entity;
using System.Data.Entity;

namespace DataAccess
{
    public class ChatDbContext : DbContext
    {
        public ChatDbContext() : base("ChatDbContext")
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<UserType> UserTypes { get; set; }
    }
}