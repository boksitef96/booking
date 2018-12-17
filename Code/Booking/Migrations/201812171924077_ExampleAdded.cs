namespace Booking.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ExampleAdded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Examples",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Examples");
        }
    }
}
