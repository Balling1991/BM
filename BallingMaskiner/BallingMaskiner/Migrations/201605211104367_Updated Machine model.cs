namespace BallingMaskiner.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatedMachinemodel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Machines", "DateCreated", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Machines", "SerialNumber", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Machines", "SerialNumber", c => c.Int(nullable: false));
            DropColumn("dbo.Machines", "DateCreated");
        }
    }
}
