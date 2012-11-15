using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using SitecoreSuperchargers.GenericItemProvider.AW.Models.Mapping;

namespace SitecoreSuperchargers.GenericItemProvider.AW.Models
{
    public class ProductsContext : DbContext
    {
        static ProductsContext()
        {
            Database.SetInitializer<ProductsContext>(null);
        }

		public ProductsContext()
			: base("Name=ProductsContext")
		{
		}

        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new ProductMap());
        }
    }
}
