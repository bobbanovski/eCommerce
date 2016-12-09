namespace eCommerce.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class baskets1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Baskets",
                c => new
                    {
                        BasketId = c.Guid(nullable: false),
                        date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.BasketId);
            
            CreateIndex("dbo.BasketItems", "BasketId");
            AddForeignKey("dbo.BasketItems", "BasketId", "dbo.Baskets", "BasketId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BasketItems", "BasketId", "dbo.Baskets");
            DropIndex("dbo.BasketItems", new[] { "BasketId" });
            DropTable("dbo.Baskets");
        }
    }
}
