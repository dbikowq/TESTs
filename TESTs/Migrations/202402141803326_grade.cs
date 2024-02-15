namespace TESTs.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class grade : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tests", "Category", c => c.String());
            AddColumn("dbo.Tests", "Grade", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Tests", "Grade");
            DropColumn("dbo.Tests", "Category");
        }
    }
}
