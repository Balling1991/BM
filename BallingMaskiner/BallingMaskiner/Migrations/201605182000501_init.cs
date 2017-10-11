namespace BallingMaskiner.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Contacts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        PhoneNumber = c.Int(nullable: false),
                        Email = c.String(),
                        Customer_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Customers", t => t.Customer_Id)
                .Index(t => t.Customer_Id);
            
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Address = c.String(),
                        PhoneNumber = c.String(),
                        Email = c.String(),
                        Homepage = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Machines",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SerialNumber = c.Int(nullable: false),
                        Name = c.String(),
                        IsSold = c.Boolean(nullable: false),
                        Customer_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Customers", t => t.Customer_Id)
                .Index(t => t.Customer_Id);
            
            CreateTable(
                "dbo.SpareParts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OrderNumber = c.String(),
                        Name = c.String(),
                        Quantity = c.Int(nullable: false),
                        Machine_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Machines", t => t.Machine_Id)
                .Index(t => t.Machine_Id);
            
            CreateTable(
                "dbo.Quotations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Customer_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Customers", t => t.Customer_Id)
                .Index(t => t.Customer_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Quotations", "Customer_Id", "dbo.Customers");
            DropForeignKey("dbo.Machines", "Customer_Id", "dbo.Customers");
            DropForeignKey("dbo.SpareParts", "Machine_Id", "dbo.Machines");
            DropForeignKey("dbo.Contacts", "Customer_Id", "dbo.Customers");
            DropIndex("dbo.Quotations", new[] { "Customer_Id" });
            DropIndex("dbo.SpareParts", new[] { "Machine_Id" });
            DropIndex("dbo.Machines", new[] { "Customer_Id" });
            DropIndex("dbo.Contacts", new[] { "Customer_Id" });
            DropTable("dbo.Quotations");
            DropTable("dbo.SpareParts");
            DropTable("dbo.Machines");
            DropTable("dbo.Customers");
            DropTable("dbo.Contacts");
        }
    }
}
