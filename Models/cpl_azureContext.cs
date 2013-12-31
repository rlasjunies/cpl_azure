using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace cpl_azure.Models
{
    public class cpl_azureContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, add the following
        // code to the Application_Start method in your Global.asax file.
        // Note: this will destroy and re-create your database with every model change.
        // 
        // System.Data.Entity.Database.SetInitializer(new System.Data.Entity.DropCreateDatabaseIfModelChanges<cpl_azure.Models.cpl_azureContext>());

        public cpl_azureContext() : base("name=cpl_azureContext")
        {
        }

        public DbSet<Paints> Paints { get; set; }
        public DbSet<News> News { get; set; }

        public DbSet<AboutMe> AboutMes { get; set; }

        public DbSet<Pictures> Pictures { get; set; }

        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Paints>().Property(p => p.PaintID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
        //    modelBuilder.Entity<News>().Property(p => p.NewsId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

        //} 

    }
}
