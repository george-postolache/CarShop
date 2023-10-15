using CarShop.Models;
using System.Data.Entity;

namespace CarShop.Contexts
{
    public class CarShopContext : DbContext
    {
        public DbSet<Client> Clients { get; set; }
        public DbSet<Car> Cars { get; set; }

        //public CarShopContext() : base("CarShopContext")
        //{
        //    // Configurare pentru a activa Lazy Loading (opțional)
        //    this.Configuration.LazyLoadingEnabled = true;
        //}

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Car>()
                .HasOptional(c => c.Client)
                .WithMany(client => client.Cars)
                .HasForeignKey(car => car.ClientId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Car>()
                .HasIndex(c => c.VIN)
                .IsUnique();

            modelBuilder.Entity<Client>()
                .HasIndex(c => c.PIN)
                .IsUnique();
            //.HasForeignKey(car => car.ClientId)
            //base.OnModelCreating(modelBuilder);
        }
    }
}
