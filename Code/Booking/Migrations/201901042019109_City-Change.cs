namespace Booking.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CityChange : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Accomodations", "CityIdNumber", c => c.Int(nullable: false));
            AddColumn("dbo.Accomodations", "City_Id", c => c.Int());
            CreateIndex("dbo.Accomodations", "City_Id");
            AddForeignKey("dbo.Accomodations", "City_Id", "dbo.Cities", "Id");
            DropColumn("dbo.Accomodations", "CityId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Accomodations", "CityId", c => c.Int(nullable: false));
            DropForeignKey("dbo.Accomodations", "City_Id", "dbo.Cities");
            DropIndex("dbo.Accomodations", new[] { "City_Id" });
            DropColumn("dbo.Accomodations", "City_Id");
            DropColumn("dbo.Accomodations", "CityIdNumber");
        }
    }
}
