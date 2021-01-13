namespace TechExpo.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedPrinterStatusFlag : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Registrants", "PrintPassStatus", c => c.String());
            DropColumn("dbo.Registrants", "PrintPass");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Registrants", "PrintPass", c => c.Boolean(nullable: false));
            DropColumn("dbo.Registrants", "PrintPassStatus");
        }
    }
}
