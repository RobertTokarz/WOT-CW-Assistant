namespace WOT_CW_Assistant.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class player1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Players",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        playerNo = c.String(),
                        playerNickName = c.String(),
                        clanId = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Players");
        }
    }
}
