namespace HospitalProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class reports : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Reports",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);

            Sql("INSERT INTO Reports(Title, Description) VALUES ('Annual Reports', 'Hospitals aren''t built from the ground up; they are built from the inside out. They are rooted by its core services, grounded by its employees and physicians, tended to by a dedicated and compassionate group of volunteers, supported by its vision, mission and values, and molded by its strategic directions.')");
        }
        
        public override void Down()
        {
            DropTable("dbo.Reports");
        }
    }
}
