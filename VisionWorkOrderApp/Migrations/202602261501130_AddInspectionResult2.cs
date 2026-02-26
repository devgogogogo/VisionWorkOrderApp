namespace VisionWorkOrderApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddInspectionResult2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.InspectionResults",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SessionId = c.Int(nullable: false),
                        TimeStamp = c.DateTime(nullable: false),
                        Label = c.String(),
                        confidence = c.Double(),
                        ImagePath = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.InspectionResults");
        }
    }
}
