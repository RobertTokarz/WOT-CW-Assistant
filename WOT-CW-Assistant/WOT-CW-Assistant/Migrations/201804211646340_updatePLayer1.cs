namespace WOT_CW_Assistant.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatePLayer1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Players", "hitPercent", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Players", "hitPercent");
        }
    }
}
