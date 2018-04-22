namespace WOT_CW_Assistant.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatePLayerstats : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TankStatistics", "playerNickName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.TankStatistics", "playerNickName");
        }
    }
}
