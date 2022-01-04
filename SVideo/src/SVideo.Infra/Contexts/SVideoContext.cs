using Microsoft.EntityFrameworkCore;
using SVideo.Domain.Entities;
using SVideo.Infra.Mappings;

namespace SVideo.Infra.Contexts
{
    public class SVideoContext : DbContext
    {
        public SVideoContext(DbContextOptions<SVideoContext> options) : base(options)
        {
            ChangeTracker.LazyLoadingEnabled = false;
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        public DbSet<Server> Server { get; set; }
        public DbSet<Video> Video { get; set; }
        public DbSet<Recycler> Recycler { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.ApplyConfiguration(new ServerMap());
            modelBuilder.ApplyConfiguration(new VideoMap());
            modelBuilder.ApplyConfiguration(new RecyclerMap());
        }
    }
}
