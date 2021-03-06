namespace HospitalProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class reportfiles : DbMigration
    {


        public override void Up()
        {            
            CreateTable(
                "dbo.ReportFiles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                        Date = c.DateTime(nullable: false),
                        ReportId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Reports", t => t.ReportId, cascadeDelete: true)
                .Index(t => t.ReportId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ReportFiles", "ReportId", "dbo.Reports");
            DropIndex("dbo.ReportFiles", new[] { "ReportId" });
            DropTable("dbo.ReportFiles");
        }
    }
}
