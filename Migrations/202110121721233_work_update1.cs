namespace DotNetAssignment.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class work_update1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Employees", "ProjectID", "dbo.Projects");
            DropIndex("dbo.Employees", new[] { "ProjectID" });
            AddColumn("dbo.Employees", "ProjectManagerID", c => c.Int(nullable: false));
            AddColumn("dbo.Employees", "IsSpecialPermission", c => c.Boolean(nullable: false));
            AddColumn("dbo.Roles", "IsHR", c => c.Boolean(nullable: false));
            AddColumn("dbo.Leaves", "ProjectManagerID", c => c.Int(nullable: false));
            DropColumn("dbo.Employees", "ProjectID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Employees", "ProjectID", c => c.Int(nullable: false));
            DropColumn("dbo.Leaves", "ProjectManagerID");
            DropColumn("dbo.Roles", "IsHR");
            DropColumn("dbo.Employees", "IsSpecialPermission");
            DropColumn("dbo.Employees", "ProjectManagerID");
            CreateIndex("dbo.Employees", "ProjectID");
            AddForeignKey("dbo.Employees", "ProjectID", "dbo.Projects", "ProjectID", cascadeDelete: true);
        }
    }
}
