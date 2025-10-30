﻿using DomainLayer.Contracts;
using DomainLayer.Models;
using DomainLayer.Models.IdentityModule;
using DomainLayer.Models.OrderModule;
using Microsoft.AspNetCore.Identity;
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
    public class DataSeeding(StoreDbContext _dbContext,
                              UserManager<ApplicationUser> _userManager,
                              RoleManager<IdentityRole> _roleManager) : IDataSeeding
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

                if (!_dbContext.Set<DeliveryMethod>().Any())
                {
                    var DeliveryMethodData = File.OpenRead(@"../Infrastructure/Persistence/Data/DataSeed/delivery.json");
                    var DeliveryMethods = await JsonSerializer.DeserializeAsync<List<DeliveryMethod>>(DeliveryMethodData);
                    if (DeliveryMethods is not null && DeliveryMethods.Any())
                    {
                        await _dbContext.Set<DeliveryMethod>().AddRangeAsync(DeliveryMethods);
                    }

                }
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                //to do
            }
        }

        public async Task IdentityDataSeedAsync()
        {
            try
            {
                if (!_roleManager.Roles.Any())
                {
                    await _roleManager.CreateAsync(new IdentityRole("Admin"));
                    await _roleManager.CreateAsync(new IdentityRole("SuperAdmin"));
                }
                if (!_userManager.Users.Any())
                {
                    var user01 = new ApplicationUser()
                    {
                        Email = "youssef@gmail.com",
                        DisplayName = "youssef",
                        UserName = "youssefahmed",
                        PhoneNumber = "01012345678",
                    };
                    var user02 = new ApplicationUser()
                    {
                        Email = "ahmed@gmail.com",
                        DisplayName = "ahmed",
                        UserName = "ahmedyousef",
                        PhoneNumber = "01112345678",
                    };

                    await _userManager.CreateAsync(user01, "Pa$$w0rd");
                    await _userManager.CreateAsync(user02, "Pa$$w0rd");

                    await _userManager.AddToRoleAsync(user01, "Admin");
                    await _userManager.AddToRoleAsync(user02, "SuperAdmin");
                }
            }
            catch (Exception ex)
            {

                //to do
            }
        }
    }
}
