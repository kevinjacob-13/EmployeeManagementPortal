using System;
using System.Collections.Generic;
using System.Linq;

namespace DotNetAssignment.ViewModels
{
    public class UserViewModel
    {
        public int EmpID { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Mobile { get; set; }
        public int RoleID { get; set; } 
        public DateTime? DateOfBirth { get; set; }
        public string Address { get; set; }
        public int ProjectID { get; set; }
        public int ProjectManagerID { get; set; }
        public bool IsSpecialPermission { get; set; }
        public bool IsHR { get; set; }
        public string ImageUrl { get; set; }
    }
}