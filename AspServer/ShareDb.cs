using Microsoft.EntityFrameworkCore;

namespace AspServer
{
    public class ShareDb : DbContext
    {
        public ShareDb(DbContextOptions<ShareDb> options) : base(options) { }
        public DbSet<User> Users => Set<User>();
    }
}