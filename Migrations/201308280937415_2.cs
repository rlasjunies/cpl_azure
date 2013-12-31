namespace cpl_azure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AboutMes",
                c => new
                    {
                        AboutMeId = c.Int(nullable: false, identity: true),
                        Biographie = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.AboutMeId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.AboutMes");
        }
    }
}
