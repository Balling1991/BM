namespace BallingMaskiner.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedcvrtocustomer : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Customers", "Cvr", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Customers", "Cvr");
        }
    }
}
