namespace SalesAPI_POC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Accounts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Purchases",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedDate = c.DateTime(nullable: false),
                        AccountId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Accounts", t => t.AccountId, cascadeDelete: true)
                .Index(t => t.AccountId);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Quantity = c.Int(nullable: false),
                        Description = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ProductPurchases",
                c => new
                    {
                        Product_Id = c.Int(nullable: false),
                        Purchase_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Product_Id, t.Purchase_Id })
                .ForeignKey("dbo.Products", t => t.Product_Id, cascadeDelete: true)
                .ForeignKey("dbo.Purchases", t => t.Purchase_Id, cascadeDelete: true)
                .Index(t => t.Product_Id)
                .Index(t => t.Purchase_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProductPurchases", "Purchase_Id", "dbo.Purchases");
            DropForeignKey("dbo.ProductPurchases", "Product_Id", "dbo.Products");
            DropForeignKey("dbo.Purchases", "AccountId", "dbo.Accounts");
            DropIndex("dbo.ProductPurchases", new[] { "Purchase_Id" });
            DropIndex("dbo.ProductPurchases", new[] { "Product_Id" });
            DropIndex("dbo.Purchases", new[] { "AccountId" });
            DropTable("dbo.ProductPurchases");
            DropTable("dbo.Products");
            DropTable("dbo.Purchases");
            DropTable("dbo.Accounts");
        }
    }
}
