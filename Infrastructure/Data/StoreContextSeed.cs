using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class StoreContextSeed
    {
        public static async Task SeedAsync(StoreContext context)
        {
            // if(! context.ProductBrands.Any())
            // {
            //     var brandsData = File.ReadAllText("../Infrastructure/Data/SeedData/brands.json");
            //     var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);
            //     //await context.Database.($"SET IDENTITY_INSERT [dbo].[ProductBrands] ON");
            //     context.ProductBrands.AddRange(brands);
            //     //await context.Database.ExecuteSqlRawAsync($"SET IDENTITY_INSERT [ProductBrands] OFF");
            // }
            //  if(! context.ProductTypes.Any())
            // {
            //     var typesData = File.ReadAllText("../Infrastructure/Data/SeedData/types.json");
            //     var types = JsonSerializer.Deserialize<List<ProductType>>(typesData);
            //    // await context.Database.ExecuteSqlRawAsync($"SET IDENTITY_INSERT [ProductTypes] ON");
            //     context.ProductTypes.AddRange(types);
            // }
             if(! context.Products.Any())
            {
                var productsData = File.ReadAllText("../Infrastructure/Data/SeedData/products.json");
                var products = JsonSerializer.Deserialize<List<Product>>(productsData);
                //await context.Database.ExecuteSqlRawAsync($"SET IDENTITY_INSERT [Products] ON");
                if (products == null) return;
                context.Products.AddRange(products);
                await context.SaveChangesAsync();
            }

            if (!context.DeliveryMethods.Any())
            {
                var dmData = await File
                    .ReadAllTextAsync("../Infrastructure/Data/SeedData/delivery.json");

                var methods = JsonSerializer.Deserialize<List<DeliveryMethod>>(dmData);
                if (methods == null) return;
                context.DeliveryMethods.AddRange(methods);
                await context.SaveChangesAsync();
            }
            //if(context.ChangeTracker.HasChanges()) await context.SaveChangesAsync();

           // await context.Database.ExecuteSqlRawAsync($"SET IDENTITY_INSERT [ProductBrands] OFF");
            // await context.Database.ExecuteSqlRawAsync($"SET IDENTITY_INSERT [ProductTypes] OFF");
            // await context.Database.ExecuteSqlRawAsync($"SET IDENTITY_INSERT [Products] OFF");
        }
    }
}