namespace BallingMaskiner.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changedsparepartsmodel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SpareParts", "FileName", c => c.String(nullable: false));
            AddColumn("dbo.SpareParts", "FileData_Id", c => c.Int());
            CreateIndex("dbo.SpareParts", "FileData_Id");
            AddForeignKey("dbo.SpareParts", "FileData_Id", "dbo.FileDatas", "Id");
            DropColumn("dbo.SpareParts", "OrderNumber");
            DropColumn("dbo.SpareParts", "Quantity");
        }
        
        public override void Down()
        {
            AddColumn("dbo.SpareParts", "Quantity", c => c.Int(nullable: false));
            AddColumn("dbo.SpareParts", "OrderNumber", c => c.String());
            DropForeignKey("dbo.SpareParts", "FileData_Id", "dbo.FileDatas");
            DropIndex("dbo.SpareParts", new[] { "FileData_Id" });
            DropColumn("dbo.SpareParts", "FileData_Id");
            DropColumn("dbo.SpareParts", "FileName");
        }
    }
}
