namespace BallingMaskiner.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changedPostalNumbertoPostalCodeinCustomermodel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Customers", "PostalCode", c => c.String());
            DropColumn("dbo.Customers", "PostalNumber");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Customers", "PostalNumber", c => c.String());
            DropColumn("dbo.Customers", "PostalCode");
        }
    }
}
