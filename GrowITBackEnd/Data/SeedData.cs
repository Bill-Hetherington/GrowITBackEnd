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
                        imageURL = "https://localhost:5000/images/DandelionPlant.jpg"
                    },
                    new Item {
                        Item_Name = "Aloe",
                        Price = 80.60M,
                        Description = "A green succulent with medicinal properties and arguably tasty juices.",
                        Quantity_on_Hand = 20,
                        Category = "Plant",
                        imageURL = "https://localhost:5000/images/AloePlant.jpg"
                    },
                    new Item
                    {
                        Item_Name = "Orchid",
                        Price = 100.25M,
                        Description = "A funeral plant with all the approprate shapes and colors to fit the vibe.",
                        Quantity_on_Hand = 2,
                        Category = "Plant",
                        imageURL = "https://localhost:5000/images/OrchidPlant.png"
                    },
                     new Item
                     {
                         Item_Name = "Brown Soil",
                         Price = 40.25M,
                         Description = "It's brown soil yea not much to be said really you put seeds in it, then at some point a plant pops out",
                         Quantity_on_Hand = 20,
                         Category = "Soil",
                         imageURL = "https://localhost:5000/images/soil.jpg"
                     },
                     new Item
                     {
                         Item_Name = "Browner Soil",
                         Price = 41.25M,
                         Description = "It's browner soil yea not much to be said really you put seeds in it, then at some point a plant pops out",
                         Quantity_on_Hand = 20,
                         Category = "Soil",
                         imageURL = "https://localhost:5000/images/soil.jpg"
                     },
                     new Item
                     {
                         Item_Name = "Brownest Soil",
                         Price = 42.25M,
                         Description = "It's the brownest soil yea not much to be said really you put seeds in it, then at some point a plant pops out",
                         Quantity_on_Hand = 20,
                         Category = "Soil",
                         imageURL = "https://localhost:5000/images/soil.jpg"
                     },
                     new Item
                     {
                         Item_Name = "Spade",
                         Price = 500.99M,
                         Description = "It's like a flatter shovel",
                         Quantity_on_Hand = 1,
                         Category = "Tool",
                         imageURL = "https://localhost:5000/images/spade.png"
                     },
                     new Item
                     {
                         Item_Name = "Shovel",
                         Price = 450.99M,
                         Description = "Some say it's a curvy spade",
                         Quantity_on_Hand = 0,
                         Category = "Tool",
                         imageURL = "https://localhost:5000/images/shovel.jpg"
                     },
                     new Item
                     {
                         Item_Name = "Trowel",
                         Price = 50.99M,
                         Description = "A really small shovel... sort of",
                         Quantity_on_Hand = 6,
                         Category = "Tool",
                         imageURL = "https://localhost:5000/images/trowel.jpg"
                     },
                     new Item
                     {
                         Item_Name = "Round Pot",
                         Price = 125.99M,
                         Description = "A particularly not square pot, do not stack these it'd be bad",
                         Quantity_on_Hand = 9,
                         Category = "Pot",
                         imageURL = "https://localhost:5000/images/roundPot.png"
                     },
                      new Item
                      {
                          Item_Name = "Square Pot",
                          Price = 130.99M,
                          Description = "Beautifully parallel sides and potentially stackable. I wouldn't but don't let me stop you",
                          Quantity_on_Hand = 9,
                          Category = "Pot",
                          imageURL = "https://localhost:5000/images/squarePot.jpg"
                      },
                      new Item
                      {
                          Item_Name = "Weird Pot",
                          Price = 1300.99M,
                          Description = "Look at it, it's weird looking hence the name.",
                          Quantity_on_Hand = 9,
                          Category = "Pot",
                          imageURL = "https://localhost:5000/images/weirdPot.jpg"
                      }
                    ); 
            }

            context.SaveChanges();
        }
    }
}
