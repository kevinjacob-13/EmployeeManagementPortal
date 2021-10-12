using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DotNetAssignment.ViewModels
{
    public class LeaveViewModel
    {
        public int LeaveID { get; set; }
        public int EmpID { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string LeaveReason { get; set; }
        public string LeaveStatus { get; set; }
        public string LeaveType { get; set; }
        public int? ApprovedByID { get; set; }

        public UserViewModel Employee { get; set; }
    }
}