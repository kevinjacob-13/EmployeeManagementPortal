namespace DotNetAssignment.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class leave_updated : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Leaves", "LeaveType", c => c.String());
            AddColumn("dbo.Leaves", "ApprovedByID", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Leaves", "ApprovedByID");
            DropColumn("dbo.Leaves", "LeaveType");
        }
    }
}
