namespace BallingMaskiner.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addmissingcascadeonquotations : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Quotations", "FileData_Id", "dbo.FileDatas");
            DropIndex("dbo.Quotations", new[] { "FileData_Id" });
            AlterColumn("dbo.Quotations", "FileData_Id", c => c.Int(nullable: false));
            CreateIndex("dbo.Quotations", "FileData_Id");
            AddForeignKey("dbo.Quotations", "FileData_Id", "dbo.FileDatas", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Quotations", "FileData_Id", "dbo.FileDatas");
            DropIndex("dbo.Quotations", new[] { "FileData_Id" });
            AlterColumn("dbo.Quotations", "FileData_Id", c => c.Int());
            CreateIndex("dbo.Quotations", "FileData_Id");
            AddForeignKey("dbo.Quotations", "FileData_Id", "dbo.FileDatas", "Id");
        }
    }
}
