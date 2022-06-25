using GrowITBackEnd.Data;
using GrowITBackEnd.Models;
using Microsoft.EntityFrameworkCore;
using PeoplAPV2.Models;

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

            if (!context.Items.Any())
            {
                context.Items.AddRange(
                    new Item { Item_Name="Dandelion",Price=50.60M,Description="A yellow plant with a most enticing aroma",
                        Quantity_on_Hand=10,Category="Plant" }

                    );
            }

            context.SaveChanges();
        }
    }
}
