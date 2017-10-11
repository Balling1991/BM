namespace BallingMaskiner.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedcascadeforspareparts : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.SpareParts", "Machine_Id", "dbo.Machines");
            AddForeignKey("dbo.SpareParts", "Machine_Id", "dbo.Machines", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SpareParts", "Machine_Id", "dbo.Machines");
            AddForeignKey("dbo.SpareParts", "Machine_Id", "dbo.Machines", "Id");
        }
    }
}
