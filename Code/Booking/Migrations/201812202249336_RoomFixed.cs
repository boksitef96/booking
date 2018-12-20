namespace Booking.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RoomFixed : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Rooms", "CreationDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Rooms", "LastUpdate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Rooms", "LastUpdate");
            DropColumn("dbo.Rooms", "CreationDate");
        }
    }
}
