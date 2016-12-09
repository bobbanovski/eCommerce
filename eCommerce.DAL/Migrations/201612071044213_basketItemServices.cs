namespace eCommerce.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class basketItemServices : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OrderItems", "Description", c => c.String());
            DropColumn("dbo.OrderItems", "Qantity");
        }
        
        public override void Down()
        {
            AddColumn("dbo.OrderItems", "Qantity", c => c.Int(nullable: false));
            DropColumn("dbo.OrderItems", "Description");
        }
    }
}
