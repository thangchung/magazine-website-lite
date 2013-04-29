using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using Cik.MagazineWeb.Domain.MagazineMgmt;

namespace Cik.MagazineWeb.EntityFrameworkProvider
{
    public class CoreDbContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<ItemContent> ItemContents { get; set; }
        //public DbSet<User> Users { get; set; }

        public CoreDbContext()
            : this("DefaultDb")
        {
        }

        public CoreDbContext(string connStringName) :
            base(connStringName)
        {
            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false;
            Configuration.AutoDetectChangesEnabled = false;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            

            // modelBuilder.Configurations.Add(new CategoryMapping());
            //modelBuilder.Configurations.Add(new ItemMapping());
            //modelBuilder.Configurations.Add(new ItemContentMapping());
            //modelBuilder.Configurations.Add(new UserMapping());
        }
    }
}