namespace WOT_CW_Assistant.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tankStatisctic1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.TankStatistics", "spotPerBattle", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.TankStatistics", "spotPerBattle", c => c.Int(nullable: false));
        }
    }
}
