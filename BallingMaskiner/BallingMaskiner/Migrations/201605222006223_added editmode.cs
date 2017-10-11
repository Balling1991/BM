namespace BallingMaskiner.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addededitmode : DbMigration
    {
        public override void Up()
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
        
        public override void Down()
        {
            DropTable("dbo.EditModes");
        }
    }
}
