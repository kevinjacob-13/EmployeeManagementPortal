using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
//using DotNetAssignment.Migrations;

namespace DotNetAssignment.DomainModels
{
    public class PortalDbContext : DbContext
    {
        public PortalDbContext() : base("MyConnectionString")
        {
            //Database.SetInitializer(new MigrateDatabaseToLatestVersion<PortalDbContext, Configuration>());
        }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Leave> Leaves { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Role> Roles { get; set; }
    }
}