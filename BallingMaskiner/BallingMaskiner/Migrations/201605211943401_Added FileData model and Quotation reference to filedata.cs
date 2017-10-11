namespace BallingMaskiner.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedFileDatamodelandQuotationreferencetofiledata : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FileDatas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Data = c.Binary(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Quotations", "FileName", c => c.String(nullable: false));
            AddColumn("dbo.Quotations", "FileData_Id", c => c.Int());
            CreateIndex("dbo.Quotations", "FileData_Id");
            AddForeignKey("dbo.Quotations", "FileData_Id", "dbo.FileDatas", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Quotations", "FileData_Id", "dbo.FileDatas");
            DropIndex("dbo.Quotations", new[] { "FileData_Id" });
            DropColumn("dbo.Quotations", "FileData_Id");
            DropColumn("dbo.Quotations", "FileName");
            DropTable("dbo.FileDatas");
        }
    }
}
