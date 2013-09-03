namespace cpl_azure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _11 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Paints", "Taille", c => c.String(maxLength: 255));
            AddColumn("dbo.Paints", "Order", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Paints", "Order");
            DropColumn("dbo.Paints", "Taille");
        }
    }
}
