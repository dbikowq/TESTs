namespace TESTs.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updtest : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Grades",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Number = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Tests", "CategoryId", c => c.Int(nullable: false));
            AddColumn("dbo.Tests", "GradeId", c => c.Int(nullable: false));
            DropColumn("dbo.Tests", "Category");
            DropColumn("dbo.Tests", "Grade");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Tests", "Grade", c => c.Int(nullable: false));
            AddColumn("dbo.Tests", "Category", c => c.String());
            DropColumn("dbo.Tests", "GradeId");
            DropColumn("dbo.Tests", "CategoryId");
            DropTable("dbo.Grades");
            DropTable("dbo.Categories");
        }
    }
}
