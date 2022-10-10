using ApiTemplate.Controllers;
using ApiTemplate.Models.AuthModels;
using GrowITBackEnd.Data;
using GrowITBackEnd.Models.DataModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
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
                  
            List<RegisterModel> userData = new List<RegisterModel>
            {

        new RegisterModel{
            FistName = "Alma",
            LastName = "Workman",
            Username = "Fuller",
            Email = "tincidunt.donec.vitae@google.edu",
            Address = "659-5930 A St."
        },
        new RegisterModel{
            FistName = "Clio",
            LastName = "Whitney",
            Username = "Sonya",
            Email = "bibendum.sed.est@yahoo.net",
            Address = "Ap #380-2539 Ante. St."
        },
        new RegisterModel {
            FistName = "Adara",
            LastName = "Burns",
            Username = "Ulric",
            Email = "nascetur.ridiculus.mus@icloud.org",
            Address = "178-1865 Varius Rd."
        },
        new RegisterModel{
            FistName = "Ira",
            LastName = "King",
            Username = "Clare",
            Email = "mauris@aol.couk",
            Address = "P.O. Box 816, 1711 Magna. Ave"
        },
        new RegisterModel{
            FistName = "Gabriel",
            LastName = "O'brien",
            Username = "Serina",
            Email = "mauris@yahoo.net",
            Address = "539-8752 Risus St."
        },
        new RegisterModel{
        FistName = "Timothy",
        LastName = "Vaughn",
        Username = "Jason",
        Email = "pharetra.nibh.aliquam@google.ca",
        Address = "635-6182 In Avenue"
    },
    new RegisterModel{
        FistName = "Michelle",
        LastName = "Martinez",
        Username = "Melanie",
        Email = "lorem.auctor@google.com",
        Address = "Ap #376-5413 Nec Road"
    },
    new RegisterModel{
        FistName = "Tyler",
        LastName = "Conley",
        Username = "Elmo",
        Email = "donec.porttitor@protonmail.couk",
        Address = "Ap #119-4944 Erat, Avenue"
    },
    new RegisterModel{
        FistName = "Sonia",
        LastName = "Huffman",
        Username = "Nissim",
        Email = "tempus.eu.ligula@icloud.edu",
        Address = "Ap #883-8685 Magna Av."
    },
    new RegisterModel{
        FistName = "Ingrid",
        LastName = "Trevino",
        Username = "Dean",
        Email = "lorem.vitae.odio@google.ca",
        Address = "Ap #659-2249 Pede, St."
    }
        };

            //so you do not need to run update-database
            if (context.Database.GetPendingMigrations().Any())
            {
                context.Database.Migrate();
            }
            //add users
            if (!context.Users.Any())
            {
                foreach (var newUser in userData)
                {
                    ApplicationUser user = new()
                    {
                        Email = newUser.Email,
                        NormalizedEmail = newUser.Email.ToUpper(),
                        SecurityStamp = Guid.NewGuid().ToString(),
                        UserName = newUser.Username,
                        NormalizedUserName = newUser.Username.ToUpper(),
                        FistName = newUser.FistName,
                        LastName = newUser.LastName,
                        Address = newUser.Address,
                        LockoutEnabled = false
                    };
                    PasswordHasher<ApplicationUser> passwordHasher = new PasswordHasher<ApplicationUser>();
                    var now = passwordHasher.HashPassword(user, "Password@123");
                    user.PasswordHash = now;
                    var _userstore = new UserStore<ApplicationUser>(context);
                    _userstore.CreateAsync(user);
                    _userstore.AddToRoleAsync(user, "User");
                }

                // controller.Register(newUser);

                context.SaveChanges();
                // context.Users.Add(user);
            }
            //add some items            
            if (!context.Items.Any())
            {
                context.Items.AddRange(
                    new Item
                    {
                        Item_Name = "Dandelion",
                        Price = 50.60M,
                        Description = "A yellow plant with a most enticing aroma",
                        Quantity_on_Hand = 20,
                        Category = "Plant",
                        imageURL = "https://localhost:5000/images/DandelionPlant.jpg"
                    },
                    new Item
                    {
                        Item_Name = "Aloe",
                        Price = 80.60M,
                        Description = "A green succulent with medicinal properties and arguably tasty juices.",
                        Quantity_on_Hand = 20,
                        Category = "Plant",
                        imageURL = "https://localhost:5000/images/AloePlant.jpg",
                        hotDeal = true
                    },
                    new Item
                    {
                        Item_Name = "Orchid",
                        Price = 100.25M,
                        Description = "A funeral plant with all the approprate shapes and colors to fit the vibe.",
                        Quantity_on_Hand = 20,
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
                         imageURL = "https://localhost:5000/images/soil.jpg",
                         hotDeal = true
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
                         Quantity_on_Hand = 20,
                         Category = "Tool",
                         imageURL = "https://localhost:5000/images/spade.png"
                     },
                     new Item
                     {
                         Item_Name = "Shovel",
                         Price = 450.99M,
                         Description = "Some say it's a curvy spade",
                         Quantity_on_Hand = 20,
                         Category = "Tool",
                         imageURL = "https://localhost:5000/images/shovel.jpg"
                     },
                     new Item
                     {
                         Item_Name = "Trowel",
                         Price = 50.99M,
                         Description = "A really small shovel... sort of",
                         Quantity_on_Hand = 20,
                         Category = "Tool",
                         imageURL = "https://localhost:5000/images/trowel.jpg"
                     },
                     new Item
                     {
                         Item_Name = "Round Pot",
                         Price = 125.99M,
                         Description = "A particularly not square pot, do not stack these it'd be bad",
                         Quantity_on_Hand = 20,
                         Category = "Pot",
                         imageURL = "https://localhost:5000/images/roundPot.png"
                     },
                      new Item
                      {
                          Item_Name = "Square Pot",
                          Price = 130.99M,
                          Description = "Beautifully parallel sides and potentially stackable. I wouldn't but don't let me stop you",
                          Quantity_on_Hand = 20,
                          Category = "Pot",
                          imageURL = "https://localhost:5000/images/squarePot.jpg",
                          hotDeal = true
                      },
                      new Item
                      {
                          Item_Name = "Weird Pot",
                          Price = 1300.99M,
                          Description = "Look at it, it's weird looking hence the name.",
                          Quantity_on_Hand = 20,
                          Category = "Pot",
                          imageURL = "https://localhost:5000/images/weirdPot.jpg"
                      }
                    );
            }

            context.SaveChanges();
        }
    }
}
