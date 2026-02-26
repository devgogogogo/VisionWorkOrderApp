namespace VisionWorkOrderApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeEquipmentIdToName : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.WorkOrders", "EquipmentName", c => c.String());
            DropColumn("dbo.WorkOrders", "EquipmentId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.WorkOrders", "EquipmentId", c => c.Int(nullable: false));
            DropColumn("dbo.WorkOrders", "EquipmentName");
        }
    }
}
