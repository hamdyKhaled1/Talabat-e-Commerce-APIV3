using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Talabat.Core.Entites;
using Talabat.Core.Entites.Order_Aggregate;

namespace Talabat.Repository.Data
{
    public static class StoreContextSeed
    {
        public static async Task SeedAsync(StoreDbContext dbContext)
        {
            if (!dbContext.ProductBrands.Any())
            {
                var brandsData = File.ReadAllText("../Talabat.Repository/Data/DataSeed/brands.json");
                var brands=JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);
                if (brands?.Count > 0)
                {
                    foreach (var brand in brands) 
                        await dbContext.Set<ProductBrand>().AddAsync(brand);
                    await dbContext.SaveChangesAsync();
                }
            }

            if (!dbContext.ProductTypes.Any())
            {
                var typesData = File.ReadAllText("../Talabat.Repository/Data/DataSeed/types.json");
                var types = JsonSerializer.Deserialize<List<ProductType>>(typesData);
                if (types?.Count > 0)
                {
                    foreach (var type in types)
                        await dbContext.Set<ProductType>().AddAsync(type);
                    await dbContext.SaveChangesAsync();
                }
            }

            if (!dbContext.Products.Any())
            {
                var productData = File.ReadAllText("../Talabat.Repository/Data/DataSeed/products.json");
                var products = JsonSerializer.Deserialize<List<Product>>(productData);
                if (products?.Count > 0)
                {
                    foreach (var product in products)
                        await dbContext.Set<Product>().AddAsync(product);
                    await dbContext.SaveChangesAsync();
                }
            }
            if (!dbContext.DeliveryMethods.Any())
            {
                var deliveryData = File.ReadAllText("../Talabat.Repository/Data/DataSeed/delivery.json");
                var deliverys = JsonSerializer.Deserialize<List<DeliveryMethod>>(deliveryData);
                if (deliverys?.Count > 0)
                {
                    foreach (var delivery in deliverys)
                        await dbContext.Set<DeliveryMethod>().AddAsync(delivery);
                    await dbContext.SaveChangesAsync();
                }
            }
        }
    }
}
