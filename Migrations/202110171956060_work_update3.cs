namespace DotNetAssignment.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class work_update3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Employees", "ProjectID", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Employees", "ProjectID");
        }
    }
}
