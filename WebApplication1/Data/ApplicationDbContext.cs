using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<HelpRequest> HelpRequests => Set<HelpRequest>();
        public DbSet<HelpOffer> HelpOffers => Set<HelpOffer>();
        public DbSet<User> Users => Set<User>();
    }
}