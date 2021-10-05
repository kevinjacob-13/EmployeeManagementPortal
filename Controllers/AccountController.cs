using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using DotNetAssignment.ViewModels;
using DotNetAssignment.ServiceLayer;
using DotNetAssignment.DomainModels;
using DotNetAssignment.Repositories;

using System.Web.Helpers;

namespace DotNetAssignment.Controllers
{
    public class AccountController : Controller
    {
        IEmployeesService us;
        public AccountController(IEmployeesService us)
        {
            this.us = us;
        }

        // GET: Account
        public ActionResult Login()
        {
            LoginViewModel lvm = new LoginViewModel();
            return View(lvm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel lvm)
        {
            if (ModelState.IsValid)
            {
                UserViewModel uvm = us.GetUsersByEmailAndPassword(lvm.Email, lvm.Password);
                if (uvm != null)
                {
                    Session["CurrentUserID"] = uvm.EmpID;
                    Session["CurrentUserName"] = uvm.FirstName+" "+uvm.LastName;
                    Session["CurrentUserEmail"] = uvm.Email;
                    Session["CurrentUserPassword"] = uvm.Password;
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("x", "Invalid Email / Password");
                    return View(lvm);
                }
            }
            else
            {
                ModelState.AddModelError("x", "Invalid Data");
                return View(lvm);
            }
        }
    }
}