namespace Booking.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CityChangebuilder : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Accomodations", "City_Id", "dbo.Cities");
            DropIndex("dbo.Accomodations", new[] { "City_Id" });
            AddColumn("dbo.Accomodations", "CityId", c => c.Int(nullable: false));
            DropColumn("dbo.Accomodations", "City_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Accomodations", "City_Id", c => c.Int());
            DropColumn("dbo.Accomodations", "CityId");
            CreateIndex("dbo.Accomodations", "City_Id");
            AddForeignKey("dbo.Accomodations", "City_Id", "dbo.Cities", "Id");
        }
    }
}
