namespace BallingMaskiner.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedlistofservicestoMachineModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Services", "Machine_Id", c => c.Int());
            CreateIndex("dbo.Services", "Machine_Id");
            AddForeignKey("dbo.Services", "Machine_Id", "dbo.Machines", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Services", "Machine_Id", "dbo.Machines");
            DropIndex("dbo.Services", new[] { "Machine_Id" });
            DropColumn("dbo.Services", "Machine_Id");
        }
    }
}
