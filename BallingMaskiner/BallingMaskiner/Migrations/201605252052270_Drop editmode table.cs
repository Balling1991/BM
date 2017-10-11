namespace BallingMaskiner.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Dropeditmodetable : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.EditModes");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.EditModes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Edit = c.Boolean(nullable: false),
                        UserId = c.String(),
                    })
                .PrimaryKey(t => t.Id);
        }
    }
}
