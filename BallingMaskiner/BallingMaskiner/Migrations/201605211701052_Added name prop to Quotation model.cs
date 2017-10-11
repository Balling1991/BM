namespace BallingMaskiner.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddednameproptoQuotationmodel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Quotations", "Name", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Quotations", "Name");
        }
    }
}
