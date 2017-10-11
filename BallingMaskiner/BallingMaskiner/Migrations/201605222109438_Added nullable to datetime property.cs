namespace BallingMaskiner.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Addednullabletodatetimeproperty : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Machines", "DateCreated", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Machines", "DateCreated", c => c.DateTime(nullable: false));
        }
    }
}
