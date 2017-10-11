namespace BallingMaskiner.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addcascadingforfiledata : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.SpareParts", "FileData_Id", "dbo.FileDatas");
            DropForeignKey("dbo.Prospects", "FileData_Id", "dbo.FileDatas");
            DropIndex("dbo.SpareParts", new[] { "FileData_Id" });
            DropIndex("dbo.Prospects", new[] { "FileData_Id" });
            AlterColumn("dbo.SpareParts", "FileData_Id", c => c.Int(nullable: false));
            AlterColumn("dbo.Prospects", "FileData_Id", c => c.Int(nullable: false));
            CreateIndex("dbo.SpareParts", "FileData_Id");
            CreateIndex("dbo.Prospects", "FileData_Id");
            AddForeignKey("dbo.SpareParts", "FileData_Id", "dbo.FileDatas", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Prospects", "FileData_Id", "dbo.FileDatas", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Prospects", "FileData_Id", "dbo.FileDatas");
            DropForeignKey("dbo.SpareParts", "FileData_Id", "dbo.FileDatas");
            DropIndex("dbo.Prospects", new[] { "FileData_Id" });
            DropIndex("dbo.SpareParts", new[] { "FileData_Id" });
            AlterColumn("dbo.Prospects", "FileData_Id", c => c.Int());
            AlterColumn("dbo.SpareParts", "FileData_Id", c => c.Int());
            CreateIndex("dbo.Prospects", "FileData_Id");
            CreateIndex("dbo.SpareParts", "FileData_Id");
            AddForeignKey("dbo.Prospects", "FileData_Id", "dbo.FileDatas", "Id");
            AddForeignKey("dbo.SpareParts", "FileData_Id", "dbo.FileDatas", "Id");
        }
    }
}
