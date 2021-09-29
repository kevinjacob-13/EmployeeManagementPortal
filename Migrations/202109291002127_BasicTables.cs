namespace DotNetAssignment.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BasicTables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        EmpID = c.Int(nullable: false, identity: true),
                        Email = c.String(nullable: false, maxLength: 50),
                        PasswordHash = c.String(nullable: false),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Mobile = c.String(nullable: false, maxLength: 10),
                        RoleID = c.Int(nullable: false),
                        DateOfBirth = c.DateTime(),
                        Address = c.String(),
                        ProjectID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.EmpID)
                .ForeignKey("dbo.Projects", t => t.ProjectID, cascadeDelete: true)
                .ForeignKey("dbo.Roles", t => t.RoleID, cascadeDelete: true)
                .Index(t => t.Email, unique: true)
                .Index(t => t.Mobile, unique: true)
                .Index(t => t.RoleID)
                .Index(t => t.ProjectID);
            
            CreateTable(
                "dbo.Projects",
                c => new
                    {
                        ProjectID = c.Int(nullable: false, identity: true),
                        ProjectManagerID = c.String(),
                        ProjectName = c.String(),
                    })
                .PrimaryKey(t => t.ProjectID);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        RoleID = c.Int(nullable: false, identity: true),
                        RoleName = c.String(),
                    })
                .PrimaryKey(t => t.RoleID);
            
            CreateTable(
                "dbo.Leaves",
                c => new
                    {
                        LeaveID = c.Int(nullable: false, identity: true),
                        EmpID = c.Int(nullable: false),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        LeaveReason = c.String(),
                        LeaveStatus = c.String(),
                    })
                .PrimaryKey(t => t.LeaveID)
                .ForeignKey("dbo.Employees", t => t.EmpID, cascadeDelete: true)
                .Index(t => t.EmpID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Leaves", "EmpID", "dbo.Employees");
            DropForeignKey("dbo.Employees", "RoleID", "dbo.Roles");
            DropForeignKey("dbo.Employees", "ProjectID", "dbo.Projects");
            DropIndex("dbo.Leaves", new[] { "EmpID" });
            DropIndex("dbo.Employees", new[] { "ProjectID" });
            DropIndex("dbo.Employees", new[] { "RoleID" });
            DropIndex("dbo.Employees", new[] { "Mobile" });
            DropIndex("dbo.Employees", new[] { "Email" });
            DropTable("dbo.Leaves");
            DropTable("dbo.Roles");
            DropTable("dbo.Projects");
            DropTable("dbo.Employees");
        }
    }
}
