using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.OrderAggragate;

namespace Talabat.Repository.Data
{
    public static class StoreContextSeed
    {
       
        public static async Task SeedAsync(StoreContext db_context)
        {
            if (!db_context.Set<ProductBrand>().Any())
            {
                var BrandData = File.ReadAllText("../Talabat.Repository/Data/DataSeed/brands.json");
                var Brands = JsonSerializer.Deserialize<List<ProductBrand>>(BrandData);
                if (Brands is not null && Brands.Count > 0)
                {
                    foreach (var B in Brands)
                        await db_context.Set<ProductBrand>().AddAsync(B);
                    await db_context.SaveChangesAsync();

                }
            }
         

            if (!db_context.Set<ProductType>().Any())
            {
                var TypeData = File.ReadAllText("../Talabat.Repository/Data/DataSeed/types.json");
                var Type = JsonSerializer.Deserialize<List<ProductType>>(TypeData);
                if (Type is not null && Type.Count > 0)
                {
                    foreach (var T in Type)
                        await db_context.Set<ProductType>().AddAsync(T);
                    await db_context.SaveChangesAsync();
                }
            }

            if (!db_context.Set<Product>().Any())
            {
                var ProductData = File.ReadAllText("../Talabat.Repository/Data/DataSeed/products.json");
                var Product = JsonSerializer.Deserialize<List<Product>>(ProductData);
                if (Product is not null && Product.Count > 0)
                {
                    foreach (var P in Product)
                        await db_context.Set<Product>().AddAsync(P);
                    await db_context.SaveChangesAsync();

                }
            }


            if (!db_context.Set<DeliveryMethod>().Any())
            {
                var DeliveryData = File.ReadAllText("../Talabat.Repository/Data/DataSeed/delivery.json");
                var Delivery = JsonSerializer.Deserialize<List<DeliveryMethod>>(DeliveryData);
                if (Delivery is not null && Delivery.Count > 0)
                {
                    foreach (var P in Delivery)
                        await db_context.Set<DeliveryMethod>().AddAsync(P);
                    await db_context.SaveChangesAsync();

                }
            }
        }
    }
}
