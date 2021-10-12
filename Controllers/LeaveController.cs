using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DotNetAssignment.ViewModels;
using DotNetAssignment.ServiceLayer;
using DotNetAssignment.CustomFilters;

namespace DotNetAssignment.Controllers
{
    public class LeaveController : Controller
    {
        IEmployeesService us;
        ILeavesService ls;
        public LeaveController(IEmployeesService us, ILeavesService ls)
        {
            this.us = us;
            this.ls = ls;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [UserAuthorizationFilterAttribute]
        public ActionResult LeaveDelete(LeaveViewModel levm)
        {
            this.ls.DeleteLeave(levm.LeaveID);
            return RedirectToAction("LeaveHome", "Leave");
        }

        [UserAuthorizationFilterAttribute]
        public ActionResult LeaveHome()
        {
            int uid = Convert.ToInt32(Session["CurrentUserID"]);
            List<LeaveViewModel> leavelist = this.ls.GetLeavesByEmpID(uid);
            return View(leavelist);
        }

        [UserAuthorizationFilterAttribute]
        public ActionResult Create()
        {
            List<string> LeaveTypes = new List<string>() {"Medical","Paid","UnPaid"};
            ViewBag.LeaveTypes = LeaveTypes;
            ViewBag.Today = DateTime.Now;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [UserAuthorizationFilterAttribute]
        public ActionResult Create(LeaveViewModel levm)
        {
            if (ModelState.IsValid)
            {
                levm.LeaveStatus = "Pending";
                levm.EmpID = Convert.ToInt32(Session["CurrentUserID"]);
                this.ls.InsertLeave(levm);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("x", "Invalid data");
                return View();
            }
        }
    }
}