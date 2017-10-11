namespace BallingMaskiner.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedCategoryfieldtoQuotation : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Quotations", "Category", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Quotations", "Category");
        }
    }
}
