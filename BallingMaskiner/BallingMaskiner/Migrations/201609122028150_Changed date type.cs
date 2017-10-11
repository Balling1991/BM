namespace BallingMaskiner.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Changeddatetype : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Visits", "Date", c => c.String());
            AlterColumn("dbo.Visits", "NextAppointment", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Visits", "NextAppointment", c => c.DateTime());
            AlterColumn("dbo.Visits", "Date", c => c.DateTime());
        }
    }
}
