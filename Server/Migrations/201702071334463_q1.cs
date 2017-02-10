namespace Server.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class q1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Balances", "Sum", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Balances", "Sum", c => c.Int(nullable: false));
        }
    }
}
