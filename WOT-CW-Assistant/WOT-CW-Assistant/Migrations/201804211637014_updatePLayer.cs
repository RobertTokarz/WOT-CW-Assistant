namespace WOT_CW_Assistant.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatePLayer : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Players", "battles", c => c.Int(nullable: false));
            AddColumn("dbo.Players", "avgExperience", c => c.Int(nullable: false));
            AddColumn("dbo.Players", "personalRating", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Players", "personalRating");
            DropColumn("dbo.Players", "avgExperience");
            DropColumn("dbo.Players", "battles");
        }
    }
}
