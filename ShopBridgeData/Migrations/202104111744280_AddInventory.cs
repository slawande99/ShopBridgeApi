namespace ShopBridgeData.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddInventory : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Inventories",
                c => new
                    {
                        InventoryID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Description = c.String(nullable: false),
                        Price = c.Single(nullable: false),
                        ImagePath = c.String(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        DateIn = c.DateTime(nullable: false),
                        InventoryTypeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.InventoryID)
                .ForeignKey("dbo.InventoryTypes", t => t.InventoryTypeId, cascadeDelete: true)
                .Index(t => t.InventoryTypeId);
            
            CreateTable(
                "dbo.InventoryTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Inventories", "InventoryTypeId", "dbo.InventoryTypes");
            DropIndex("dbo.Inventories", new[] { "InventoryTypeId" });
            DropTable("dbo.InventoryTypes");
            DropTable("dbo.Inventories");
        }
    }
}
