namespace cpl_azure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _12 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Paints", "Size", c => c.String(maxLength: 255));
            DropColumn("dbo.Paints", "Taille");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Paints", "Taille", c => c.String(maxLength: 255));
            DropColumn("dbo.Paints", "Size");
        }
    }
}
