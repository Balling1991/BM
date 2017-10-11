namespace BallingMaskiner.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Serviceandmanualmodel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Manuals",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        FileName = c.String(nullable: false),
                        FileData_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.FileDatas", t => t.FileData_Id, cascadeDelete: true)
                .Index(t => t.FileData_Id);
            
            CreateTable(
                "dbo.Services",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        FileName = c.String(nullable: false),
                        FileData_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.FileDatas", t => t.FileData_Id, cascadeDelete: true)
                .Index(t => t.FileData_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Services", "FileData_Id", "dbo.FileDatas");
            DropForeignKey("dbo.Manuals", "FileData_Id", "dbo.FileDatas");
            DropIndex("dbo.Services", new[] { "FileData_Id" });
            DropIndex("dbo.Manuals", new[] { "FileData_Id" });
            DropTable("dbo.Services");
            DropTable("dbo.Manuals");
        }
    }
}
