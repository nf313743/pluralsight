namespace PTC.Models
{
    using System.Data.Entity;

    public partial class PTCData : DbContext
    {
        public PTCData()
            : base("name=PTCData")
        {
            this.Configuration.ProxyCreationEnabled = false;
        }

        public virtual DbSet<Category> Categories { get; set; }

        public virtual DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .Property(e => e.Price)
                .HasPrecision(19, 4);
        }
    }
}