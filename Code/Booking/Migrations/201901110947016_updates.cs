namespace Booking.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updates : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Images", newName: "ImageModels");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.ImageModels", newName: "Images");
        }
    }
}
