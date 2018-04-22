namespace WOT_CW_Assistant.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateTanks : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TankStatistics", "tank", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.TankStatistics", "tank");
        }
    }
}
