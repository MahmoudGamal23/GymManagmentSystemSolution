﻿using GymManagEmentDAL.Data.Contexts;
using GymManagEmentDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace GymManagementDAL.Data.DataSeed
{
    public static class GymDbContextDataSeeding
    {
        public static bool SeedData (GymDbContext dbContext)
        {
            try
            {
                var HasPlans = dbContext.Plans.Any();
                var HasCategories = dbContext.Categories.Any();
                if (HasPlans && HasCategories) return false;

                if (!HasPlans)
                {
                    var Plans = LoadDataFromJsonFile<Plan>("plans.json");
                    if (Plans.Any())
                        dbContext.Plans.AddRange(Plans);

                }
                if (!HasCategories)
                {
                    var Categories = LoadDataFromJsonFile<Category>("categories.json");
                    if (Categories.Any())
                        dbContext.Categories.AddRange(Categories);
                }
                return dbContext.SaveChanges() > 0;
            }
            catch(Exception ex) 
            { 
                Console.WriteLine($"Seeding Failed{ex}");
                return false;
            
            }
        }
        private static List<T> LoadDataFromJsonFile<T>(string fileName)
        {

            var FilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Files", fileName);

            if(!File.Exists(FilePath)) throw new FileNotFoundException();

            string Data = File.ReadAllText(FilePath);
            var Option = new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true,
            };
            return JsonSerializer.Deserialize<List<T>>(Data, Option) ?? new List<T>();

        }
    }
}
