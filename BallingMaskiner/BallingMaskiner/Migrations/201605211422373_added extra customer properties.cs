namespace BallingMaskiner.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedextracustomerproperties : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Customers", "PostalNumber", c => c.String());
            AddColumn("dbo.Customers", "City", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Customers", "City");
            DropColumn("dbo.Customers", "PostalNumber");
        }
    }
}
