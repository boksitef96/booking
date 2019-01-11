namespace Booking.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Imageupdate2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Images", "Accomodation_Id", c => c.Int());
            CreateIndex("dbo.Images", "Accomodation_Id");
            AddForeignKey("dbo.Images", "Accomodation_Id", "dbo.Accomodations", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Images", "Accomodation_Id", "dbo.Accomodations");
            DropIndex("dbo.Images", new[] { "Accomodation_Id" });
            DropColumn("dbo.Images", "Accomodation_Id");
        }
    }
}
