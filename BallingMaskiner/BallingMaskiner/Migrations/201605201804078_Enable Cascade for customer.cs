namespace BallingMaskiner.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EnableCascadeforcustomer : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Contacts", "Customer_Id", "dbo.Customers");
            AddForeignKey("dbo.Contacts", "Customer_Id", "dbo.Customers", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Contacts", "Customer_Id", "dbo.Customers");
            AddForeignKey("dbo.Contacts", "Customer_Id", "dbo.Customers", "Id");
        }
    }
}
