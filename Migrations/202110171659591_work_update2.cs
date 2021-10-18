namespace DotNetAssignment.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class work_update2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Employees", "IsHR", c => c.Boolean(nullable: false));
            DropColumn("dbo.Roles", "IsHR");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Roles", "IsHR", c => c.Boolean(nullable: false));
            DropColumn("dbo.Employees", "IsHR");
        }
    }
}
