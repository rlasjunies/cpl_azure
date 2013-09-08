namespace cpl_azure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _13 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.News", "Content", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.News", "Content", c => c.String(maxLength: 255));
        }
    }
}
