namespace BallingMaskiner.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedVisitsmodel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Visits",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Comment = c.String(),
                        ContactPerson = c.String(),
                        Date = c.DateTime(nullable: false),
                        NextAppointment = c.DateTime(nullable: false),
                        Customer_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Customers", t => t.Customer_Id, cascadeDelete: true)
                .Index(t => t.Customer_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Visits", "Customer_Id", "dbo.Customers");
            DropIndex("dbo.Visits", new[] { "Customer_Id" });
            DropTable("dbo.Visits");
        }
    }
}
