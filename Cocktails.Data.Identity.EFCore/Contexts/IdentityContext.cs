using Microsoft.EntityFrameworkCore;

namespace Cocktails.Data.Identity.EFCore.Contexts
{
    public class IdentityContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<UserClaim> UserClaims { get; set; }

        public DbSet<UserLogin> UserLogins { get; set; }

        public DbSet<UserRole> UserRoles { get; set; }

        public DbSet<UserToken> UserTokens { get; set; }

        public DbSet<Role> Roles { get; set; }

        public DbSet<RoleClaim> RoleClaims { get; set; }

        public DbSet<UserProfile> UserProfiles { get; set; }

        public IdentityContext(DbContextOptions<IdentityContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>(b =>
            {
                b.HasKey(u => u.Id);

                b.Property(u => u.UserName).HasMaxLength(256);
                b.Property(u => u.Email).HasMaxLength(256);
                b.HasMany(u => u.Claims).WithOne().HasForeignKey(uc => uc.UserId).IsRequired();
                b.HasMany(u => u.Logins).WithOne().HasForeignKey(ul => ul.UserId).IsRequired();
                b.HasMany(u => u.Roles).WithOne().HasForeignKey(ur => ur.UserId).IsRequired();
                b.HasMany(u => u.Tokens).WithOne().HasForeignKey(ut => ut.UserId).IsRequired();
                b.HasOne(u => u.UserProfile).WithOne().HasForeignKey<UserProfile>(up => up.Id).IsRequired();
            });

            modelBuilder.Entity<Role>(b =>
            {
                b.HasKey(r => r.Id);

                b.Property(u => u.Name).HasMaxLength(256);

                b.HasMany(r => r.Users).WithOne().HasForeignKey(ur => ur.RoleId).IsRequired();
                b.HasMany(r => r.Claims).WithOne().HasForeignKey(rc => rc.RoleId).IsRequired();
            });

            modelBuilder.Entity<UserClaim>(b =>
            {
                b.HasKey(uc => uc.Id);
            });

            modelBuilder.Entity<RoleClaim>(b =>
            {
                b.HasKey(rc => rc.Id);
            });

            modelBuilder.Entity<UserRole>(b =>
            {
                b.HasKey(r => new { r.UserId, r.RoleId });
            });

            modelBuilder.Entity<UserLogin>(b =>
            {
                b.HasKey(l => new { l.LoginProvider, l.ProviderKey });
            });

            modelBuilder.Entity<UserToken>(b =>
            {
                b.HasKey(l => new { l.UserId, l.LoginProvider, l.Name });
            });

            modelBuilder.Entity<UserProfile>(b =>
            {
                b.HasKey(u => u.Id);
            });
        }
    }
}
