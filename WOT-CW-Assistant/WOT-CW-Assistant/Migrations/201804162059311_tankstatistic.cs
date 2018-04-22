namespace WOT_CW_Assistant.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tankstatistic : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TankStatistics", "tankId", c => c.String());
            AddColumn("dbo.TankStatistics", "playerNo", c => c.String());
            AddColumn("dbo.TankStatistics", "battlesCount", c => c.Int(nullable: false));
            AddColumn("dbo.TankStatistics", "avgExperiencePerBattle", c => c.Int(nullable: false));
            AddColumn("dbo.TankStatistics", "lastUpdate", c => c.DateTime(nullable: false));
            AddColumn("dbo.TankStatistics", "avgDamagePerBattle", c => c.Int(nullable: false));
            DropColumn("dbo.TankStatistics", "tankNo");
            DropColumn("dbo.TankStatistics", "tankName");
            DropColumn("dbo.TankStatistics", "tankImage");
            DropColumn("dbo.TankStatistics", "battles");
            DropColumn("dbo.TankStatistics", "avgExperience");
        }
        
        public override void Down()
        {
            AddColumn("dbo.TankStatistics", "avgExperience", c => c.Int(nullable: false));
            AddColumn("dbo.TankStatistics", "battles", c => c.Int(nullable: false));
            AddColumn("dbo.TankStatistics", "tankImage", c => c.String());
            AddColumn("dbo.TankStatistics", "tankName", c => c.String());
            AddColumn("dbo.TankStatistics", "tankNo", c => c.String());
            DropColumn("dbo.TankStatistics", "avgDamagePerBattle");
            DropColumn("dbo.TankStatistics", "lastUpdate");
            DropColumn("dbo.TankStatistics", "avgExperiencePerBattle");
            DropColumn("dbo.TankStatistics", "battlesCount");
            DropColumn("dbo.TankStatistics", "playerNo");
            DropColumn("dbo.TankStatistics", "tankId");
        }
    }
}
