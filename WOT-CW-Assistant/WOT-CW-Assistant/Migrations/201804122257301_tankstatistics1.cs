namespace WOT_CW_Assistant.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tankstatistics1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TankStatistics",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        tankNo = c.String(),
                        tankName = c.String(),
                        tankImage = c.String(),
                        battles = c.Int(nullable: false),
                        damageDealt = c.Int(nullable: false),
                        avgExperience = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.TankStatistics");
        }
    }
}
