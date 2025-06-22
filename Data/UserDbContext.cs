using WebApplication2.Model;
using Microsoft.EntityFrameworkCore;

namespace WebApplication2.Data
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
