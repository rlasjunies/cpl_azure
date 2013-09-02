namespace cpl_azure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _10 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Pictures", "FullFileName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Pictures", "FullFileName");
        }
    }
}
