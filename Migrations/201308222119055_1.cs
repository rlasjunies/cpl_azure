namespace cpl_azure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class _1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.News",
                c => new
                {
                    NewsId = c.Int(nullable: false, identity: true),
                    Title = c.String(nullable: false, maxLength: 100),
                    Summary = c.String(maxLength: 255),
                    Content = c.String(),
                })
                .PrimaryKey(t => t.NewsId);

            CreateTable(
                "dbo.Paints",
                c => new
                {
                    PaintId = c.Int(nullable: false, identity: true),
                    Name = c.String(nullable: false, maxLength: 100),
                    Description = c.String(maxLength: 255),
                    Year = c.String(maxLength: 4),
                    Picture = c.String(maxLength: 255),
                })
                .PrimaryKey(t => t.PaintId);

        }

        public override void Down()
        {
            DropTable("dbo.News");
            DropTable("dbo.Paints");
        }
    }
}