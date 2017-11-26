namespace SalesAPI_POC.Migrations
{
    using SalesAPI_POC.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<SalesAPI_POC.Models.SalesAPI_POCContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(SalesAPI_POC.Models.SalesAPI_POCContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            var date = DateTime.Now;

            context.Accounts.AddOrUpdate(
                  p => p.Id,
                  new Account { Id = 1, Name = "Andrew Peters", CreatedDate = date, ModifiedDate = date },
                  new Account { Id = 2, Name = "Brice Lambson", CreatedDate = date, ModifiedDate = date },
                  new Account { Id = 3, Name = "Rowan Miller", CreatedDate = date, ModifiedDate = date }
                );

            context.Products.AddOrUpdate(
                p => p.Id,
                new Product { Id = 1, Name = "Table", Description = "Sample description for Table", Price = 200000, Quantity = 10, CreatedDate = date, ModifiedDate = date },
                new Product { Id = 2, Name = "Chair", Description = "Sample description for Chair", Price = 100000, Quantity = 10, CreatedDate = date, ModifiedDate = date },
                new Product { Id = 3, Name = "Monitor", Description = "Sample description for Monitor", Price = 300000, Quantity = 10, CreatedDate = date, ModifiedDate = date },
                new Product { Id = 4, Name = "Notebook", Description = "Sample description for Notebook", Price = 1800000, Quantity = 10, CreatedDate = date, ModifiedDate = date },
                new Product { Id = 5, Name = "Keyboard", Description = "Sample description for Keyboard", Price = 100000, Quantity = 10, CreatedDate = date, ModifiedDate = date },
                new Product { Id = 6, Name = "Mouse", Description = "Sample description for Mouse", Price = 70000, Quantity = 10, CreatedDate = date, ModifiedDate = date },
                new Product { Id = 7, Name = "Tablet", Description = "Sample description for Tablet", Price = 50000, Quantity = 10, CreatedDate = date, ModifiedDate = date }
                );

            context.SaveChanges();
            context.Purchases.AddOrUpdate(
                p => p.Id,
                new Purchase { Id = 1, AccountId = 1, Products= context.Products.Take(3).ToList(), CreatedDate = date, ModifiedDate = date },
                new Purchase { Id = 2, AccountId = 2, Products = context.Products.OrderByDescending(a => a.Price).Take(3).ToList(), CreatedDate = date, ModifiedDate = date },
                new Purchase { Id = 3, AccountId = 3, Products = context.Products.OrderBy(a => a.Price).Take(3).ToList(), CreatedDate = date, ModifiedDate = date },
                new Purchase { Id = 4, AccountId = 3, Products = context.Products.OrderBy(a => a.Name).Take(3).ToList(), CreatedDate = date, ModifiedDate = date }
                );
        }
    }
}
