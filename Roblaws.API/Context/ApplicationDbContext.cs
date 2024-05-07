using JWTAuthTemplate.Models.Identity;
using JWTAuthTemplate.Models.Loblaws;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace JWTAuthTemplate.Context
{
    public class ApplicationDbContext: IdentityDbContext<ApplicationUser, ApplicationRole, string, ApplicationUserClaim, ApplicationUserRole, ApplicationUserLogin, ApplicationRoleClaim, ApplicationUserToken>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ApplicationUser>(e =>
            {
                e.HasMany(r => r.Roles)
                    .WithOne(u => u.User)
                    .HasForeignKey(u => u.UserId)
                    .IsRequired();
            });

            modelBuilder.Entity<ApplicationRole>(e =>
            {
                e.HasMany(u => u.Users)
                    .WithOne(u => u.Role)
                    .HasForeignKey(u => u.RoleId)
                    .IsRequired();
            });

            modelBuilder.Entity<ApplicationUser>().Navigation(e => e.Roles).AutoInclude();
            modelBuilder.Entity<ApplicationUserRole>().Navigation(e => e.Role).AutoInclude();
            
            modelBuilder.Entity<DbOffer>()
                .HasOne(o => o.MemberOnlyPrimaryPrice)
                .WithOne(m => m.Offer)
                .HasForeignKey<DbOffer>(m => m.DbMemberOnlyPrimaryPriceId); 

            modelBuilder.Entity<DbOffer>()
                .HasOne(o => o.PrimaryPrice)
                .WithOne(m => m.Offer)
                .HasForeignKey<DbOffer>(m => m.DbPrimaryPriceId); 
            
        }
        public DbSet<DbProduct> Products { get; set; } = null!;
        public DbSet<DbPrimaryPrice> Prices { get; set; } = null!;
        public DbSet<DbOffer> Offers { get; set; } = null!;
        public DbSet<DbComparisonPrice> ComparisonPrices { get; set; } = null!;
        public DbSet<DbMemberOnlyPrimaryPrice> MemberOnlyPrimaryPrices { get; set; } = null!;
        public DbSet<DbMemberOnlyComparisonPrice> MemberOnlyComparisonPrices { get; set; } = null!;

    }
}
