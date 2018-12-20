namespace Booking.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ExampleEdit : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Examples", "LastName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Examples", "LastName");
        }
    }
}
