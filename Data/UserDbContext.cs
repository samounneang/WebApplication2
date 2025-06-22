using AgriAuth.Model;
using Microsoft.EntityFrameworkCore;

namespace AgriAuth.Data
{
    public class UserDbContext : DbContext
    {
        public UserDbContext(DbContextOptions<UserDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Otpcode> Otpcodes { get; set; }

        public DbSet<FailedUser> FailedUsers { get; set; }

        public DbSet<OtpCodeforPhone> otpCodeforPhones { get; set; }
    }
}
