using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.OrderAggragate;

namespace Talabat.Repository.Data
{
    public class StoreContext :DbContext
    {

        public StoreContext(DbContextOptions<StoreContext>options):base(options)
        {
            
        } 
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }
        DbSet<Product> Products { get; set; }
        DbSet<ProductBrand> ProductBrands { get; set; }
        DbSet<ProductType> ProductTypes { get; set; }
        DbSet<Order> Orders { get; set; }
        DbSet<DeliveryMethod> DeliveryMethods { get; set;}
        DbSet<OrderIteml> Items { get; set; }
       
    }
}
