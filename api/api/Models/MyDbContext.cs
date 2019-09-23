namespace api.Models
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class MyDbContext : DbContext
    {
        
        public MyDbContext()
            : base("name=Angular_MVC_DB")
        {
        }

        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Feature> Features { get; set; }
        public virtual DbSet<CategoryFeature> CategoryFeatures { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ProductFeatureValue> ProductFeatureValues { get; set; }
    }


}