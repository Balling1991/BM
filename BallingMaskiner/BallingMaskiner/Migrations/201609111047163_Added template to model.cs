namespace BallingMaskiner.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Addedtemplatetomodel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Quotations", "IsTemplate", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Quotations", "IsTemplate");
        }
    }
}
