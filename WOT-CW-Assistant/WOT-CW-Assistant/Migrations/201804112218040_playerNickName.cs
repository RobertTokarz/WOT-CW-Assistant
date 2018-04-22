namespace WOT_CW_Assistant.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class playerNickName : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AspNetUsers", "PlayerNickName", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AspNetUsers", "PlayerNickName", c => c.String(nullable: false));
        }
    }
}
