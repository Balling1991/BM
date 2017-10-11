namespace BallingMaskiner.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddednullabletoDateTimeproperties : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Visits", "Date", c => c.DateTime());
            AlterColumn("dbo.Visits", "NextAppointment", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Visits", "NextAppointment", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Visits", "Date", c => c.DateTime(nullable: false));
        }
    }
}
