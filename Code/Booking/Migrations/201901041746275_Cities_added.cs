namespace Booking.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Cities_added : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Longitude = c.Double(nullable: false),
                        Latitude = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Accomodations", "City_Id", c => c.Int());
            CreateIndex("dbo.Accomodations", "City_Id");
            AddForeignKey("dbo.Accomodations", "City_Id", "dbo.Cities", "Id");
            DropColumn("dbo.Accomodations", "City");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Accomodations", "City", c => c.String());
            DropForeignKey("dbo.Accomodations", "City_Id", "dbo.Cities");
            DropIndex("dbo.Accomodations", new[] { "City_Id" });
            DropColumn("dbo.Accomodations", "City_Id");
            DropTable("dbo.Cities");
        }
    }
}
