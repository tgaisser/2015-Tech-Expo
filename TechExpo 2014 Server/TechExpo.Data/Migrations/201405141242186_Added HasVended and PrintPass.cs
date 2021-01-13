namespace TechExpo.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedHasVendedandPrintPass : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Registrants", "PrintPass", c => c.Boolean(nullable: false));
            AddColumn("dbo.Registrants", "HasVended", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Registrants", "HasVended");
            DropColumn("dbo.Registrants", "PrintPass");
        }
    }
}
