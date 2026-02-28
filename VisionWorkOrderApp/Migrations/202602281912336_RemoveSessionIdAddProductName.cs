namespace VisionWorkOrderApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveSessionIdAddProductName : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.InspectionResults", "ProductName", c => c.String());
            DropColumn("dbo.InspectionResults", "SessionId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.InspectionResults", "SessionId", c => c.Int(nullable: false));
            DropColumn("dbo.InspectionResults", "ProductName");
        }
    }
}
