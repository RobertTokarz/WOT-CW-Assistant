namespace WOT_CW_Assistant.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tanks2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tanks", "selected", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Tanks", "selected");
        }
    }
}
