namespace WOT_CW_Assistant.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tanks1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tanks", "nation", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Tanks", "nation");
        }
    }
}
