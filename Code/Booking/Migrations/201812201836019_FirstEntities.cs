namespace Booking.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FirstEntities : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Accomodations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        City = c.String(),
                        Country = c.String(),
                        Address = c.String(),
                        AvailableRooms = c.Int(nullable: false),
                        Description = c.String(),
                        Stars = c.Int(nullable: false),
                        Rating = c.Single(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                        LastUpdate = c.DateTime(nullable: false),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.Rooms",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Capacity = c.Int(nullable: false),
                        Price = c.Single(nullable: false),
                        Descritpion = c.String(),
                        Wifi = c.Boolean(nullable: false),
                        TV = c.Boolean(nullable: false),
                        PetFriendly = c.Boolean(nullable: false),
                        Terrace = c.Boolean(nullable: false),
                        AC = c.Boolean(nullable: false),
                        Parking = c.Boolean(nullable: false),
                        Accomodation_Id = c.Int(),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Accomodations", t => t.Accomodation_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .Index(t => t.Accomodation_Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.Reservations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Price = c.Single(nullable: false),
                        DateStart = c.DateTime(nullable: false),
                        DateEnd = c.DateTime(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                        LastUpdate = c.DateTime(nullable: false),
                        Comment = c.String(),
                        Room_Id = c.Int(),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Rooms", t => t.Room_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .Index(t => t.Room_Id)
                .Index(t => t.User_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Reservations", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Reservations", "Room_Id", "dbo.Rooms");
            DropForeignKey("dbo.Accomodations", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Rooms", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Rooms", "Accomodation_Id", "dbo.Accomodations");
            DropIndex("dbo.Reservations", new[] { "User_Id" });
            DropIndex("dbo.Reservations", new[] { "Room_Id" });
            DropIndex("dbo.Rooms", new[] { "User_Id" });
            DropIndex("dbo.Rooms", new[] { "Accomodation_Id" });
            DropIndex("dbo.Accomodations", new[] { "User_Id" });
            DropTable("dbo.Reservations");
            DropTable("dbo.Rooms");
            DropTable("dbo.Accomodations");
        }
    }
}
