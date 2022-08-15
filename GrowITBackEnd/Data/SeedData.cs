using GrowITBackEnd.Data;
using GrowITBackEnd.Models.DataModels;
using Microsoft.EntityFrameworkCore;
using PeoplAPV2.Models.AuthModels;

namespace PeoplAPV2.Data
{
    public static class SeedData
    {
        public static void EnsurePopulated(IApplicationBuilder app)
        {
            ApplicationDbContext context = app.ApplicationServices
               .CreateScope().ServiceProvider.GetRequiredService<ApplicationDbContext>();

            //so you do not need to run update-database
            if (context.Database.GetPendingMigrations().Any())
            {
                context.Database.Migrate();
            }           
            //add some items
            if (!context.Items.Any())
            {
                context.Items.AddRange(
                    new Item { Item_Name = "Dandelion", Price = 50.60M, Description = "A yellow plant with a most enticing aroma",
                        Quantity_on_Hand = 10, Category = "Plant" ,
                        imageURL = "https://localhost:5000/images/default.png"
                    },
                    new Item {
                        Item_Name = "Aloe",
                        Price = 80.60M,
                        Description = "A green succulent with medicinal properties and arguably tasty juices.",
                        Quantity_on_Hand = 20,
                        Category = "Plant",
                        imageURL = "https://localhost:5000/images/default.png"
                    },
                    new Item
                    {
                        Item_Name = "Orchid",
                        Price = 100.25M,
                        Description = "A funeral plant with all the approprate shapes and colors to fit the vibe.",
                        Quantity_on_Hand = 2,
                        Category = "Plant",
                        imageURL = "https://localhost:5000/images/default.png"
                    }

                    ); 
            }

            context.SaveChanges();
        }
    }
}
