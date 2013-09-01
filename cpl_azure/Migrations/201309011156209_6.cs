namespace cpl_azure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _6 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Pictures",
                c => new
                    {
                        PictureId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Size = c.Int(nullable: false),
                        url = c.String(),
                        delete_url = c.String(),
                        thumbnail_url = c.String(),
                        savedFileName = c.String(),
                    })
                .PrimaryKey(t => t.PictureId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Pictures");
        }
    }
}
