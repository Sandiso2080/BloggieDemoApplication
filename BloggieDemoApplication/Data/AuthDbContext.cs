using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BloggieDemoApplication.Data
{
    public class AuthDbContext : IdentityDbContext
    {
        public AuthDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // seed Roles (User, Admin, SuperAdmin)
            var AdminRoleId = "be79db9c-a7a3-4070-a360-a1b9ca4c8f78";
            var superAdminRoleId = "fa0dfa15-1259-4083-8cfc-36cd405b1a01";
            var userRoleId = "00a28609-045a-42e3-89e2-bdc44fca7857";
            

         
            var roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Name = "admin",
                    NormalizedName = "admin",
                    Id = AdminRoleId,
                    ConcurrencyStamp = AdminRoleId
                },
                new IdentityRole 
                {
                    Name="admin",
                    NormalizedName = "admin",
                    Id = superAdminRoleId,
                    ConcurrencyStamp = superAdminRoleId
                },
                new IdentityRole
                {
                    Name ="User",
                    NormalizedName="User",
                    Id = userRoleId,
                    ConcurrencyStamp = userRoleId
                }
            };

            builder.Entity<IdentityRole>().HasData(roles);

            // Seed Super AdminiUser
            var superAdminId = "b14eac31-1884-4c60-bd5a-0bcad85ccc4e";
            var superAdminUser = new IdentityUser
            {
                UserName = "sandiso2msane2@gmail.com",
                Email = "sandiso2msane2@gmail.com",
                NormalizedEmail = "sandiso2msane2@gmail.com".ToUpper(),
                NormalizedUserName = "sandiso2msane2@gmail.com".ToUpper(),
                Id = superAdminId

            };

            superAdminUser.PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(superAdminUser, "Superadmin@123");
            builder.Entity<IdentityUser>().HasData(superAdminUser);


            // Add all roles to SuperAdminUser

            var superAdminRoles = new List<IdentityUserRole<string>>
            {
                new IdentityUserRole<string> 
                {
                    RoleId = AdminRoleId,
                    UserId = superAdminId,
                },
                 new IdentityUserRole<string>
                {
                    RoleId = superAdminRoleId,
                    UserId = superAdminId,
                },
                  new IdentityUserRole<string>
                {
                    RoleId = userRoleId,
                    UserId = superAdminId,
                }
            };

            builder.Entity<IdentityUserRole<string>>().HasData(superAdminRoles);
        }
    }
}
