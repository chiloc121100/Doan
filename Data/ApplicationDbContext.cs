using AuctionProject.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AuctionProject.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser,ApplicationRole,string>
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<AuctionProject.Models.Product> Product { get; set; } = default!;
        public DbSet<AuctionProject.Models.ApplicationUser> User { get; set; } = default!;

    }
}