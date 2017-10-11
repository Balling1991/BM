namespace BallingMaskiner.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Addedprospectsmodel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Prospects",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        FileName = c.String(nullable: false),
                        FileData_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.FileDatas", t => t.FileData_Id)
                .Index(t => t.FileData_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Prospects", "FileData_Id", "dbo.FileDatas");
            DropIndex("dbo.Prospects", new[] { "FileData_Id" });
            DropTable("dbo.Prospects");
        }
    }
}
