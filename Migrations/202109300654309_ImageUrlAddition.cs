﻿namespace DotNetAssignment.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ImageUrlAddition : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Employees", "ImageUrl", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Employees", "ImageUrl");
        }
    }
}
