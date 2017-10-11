namespace BallingMaskiner.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedcascadingandtimeout : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Machines", "Customer_Id", "dbo.Customers");
            DropForeignKey("dbo.Quotations", "Customer_Id", "dbo.Customers");
            AddForeignKey("dbo.Machines", "Customer_Id", "dbo.Customers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Quotations", "Customer_Id", "dbo.Customers", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Quotations", "Customer_Id", "dbo.Customers");
            DropForeignKey("dbo.Machines", "Customer_Id", "dbo.Customers");
            AddForeignKey("dbo.Quotations", "Customer_Id", "dbo.Customers", "Id");
            AddForeignKey("dbo.Machines", "Customer_Id", "dbo.Customers", "Id");
        }
    }
}
