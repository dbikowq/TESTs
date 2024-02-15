namespace TESTs.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fio : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "FIO", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "FIO");
        }
    }
}
