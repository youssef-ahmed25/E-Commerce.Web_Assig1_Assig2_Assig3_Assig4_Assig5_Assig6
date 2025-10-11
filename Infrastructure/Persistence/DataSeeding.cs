﻿using DomainLayer.Contracts;
using DomainLayer.Models;
using Microsoft.EntityFrameworkCore;
using Persistence.Data.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Persistence
{
    public class DataSeeding(StoreDbContext _dbContext) : IDataSeeding
    {
        public async Task DataSeedAsync()
        {
            try
            {
                if ((await _dbContext.Database.GetPendingMigrationsAsync()).Any())
                {
                    await _dbContext.Database.MigrateAsync();
                }
                if (!_dbContext.ProductBrands.Any())
                {
                    //var productBrandData = await File.ReadAllTextAsync(@"..\Infrastructure\Persistence\Data\DataSeed\brands.json");
                    var productBrandData = File.OpenRead(@"..\Infrastructure\Persistence\Data\DataSeed\brands.json");
                    var productBrands =await JsonSerializer.DeserializeAsync<List<ProductBrand>>(productBrandData);
                    if (productBrands is not null && productBrands.Any())
                    {
                        await _dbContext.ProductBrands.AddRangeAsync(productBrands);
                    }

                }
                if (!_dbContext.ProductTypes.Any())
                {
                    var productTypeData = File.OpenRead(@"../Infrastructure/Persistence/Data/DataSeed/types.json");
                    var productTypes =await JsonSerializer.DeserializeAsync<List<ProductType>>(productTypeData);
                    if (productTypes is not null && productTypes.Any())
                    {
                       await _dbContext.ProductTypes.AddRangeAsync(productTypes);
                    }

                }
                if (!_dbContext.Products.Any())
                {
                    var productsData = File.OpenRead(@"../Infrastructure/Persistence/Data/DataSeed/products.json");
                    var products = await JsonSerializer.DeserializeAsync<List<Product>>(productsData);
                    if (products is not null && products.Any())
                    {
                        await _dbContext.Products.AddRangeAsync(products);
                    }

                }
               await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                //to do
            }
        }
    }
}
