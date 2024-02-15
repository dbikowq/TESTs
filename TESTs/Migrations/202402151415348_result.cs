namespace TESTs.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class result : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Results",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FIO = c.String(),
                        DateResult = c.DateTime(nullable: false),
                        NameTest = c.String(),
                        ResultTest = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Results");
        }
    }
}
