using System.Collections.Generic;
using SportsStore.Domain.Entities;
using System.Data.Entity.Migrations;
using System.Linq;

namespace SportsStore.Domain.Migrations
{
    

    internal sealed class Configuration : DbMigrationsConfiguration<Concrete.EFDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Concrete.EFDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
            if (!context.Products.Any())
            {
                var products = new List<Product>
                {
                    new Product { Name = "Shoulder pads",Category = "Football",Description = ""},
                    new Product { Name = "Shoulder pads",Category = "Tennis",Description = ""},
                    new Product { Name = "Gloves",Category = "Football",Description = ""},
                    new Product { Name = "Neck collars/neck roll",Category = "Football",Description = ""}
                };
                context.Products.AddRange(products);

                base.Seed(context);
            }
        }
    }
}
