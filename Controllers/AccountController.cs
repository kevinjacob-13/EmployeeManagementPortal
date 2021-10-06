using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using DotNetAssignment.ViewModels;
using DotNetAssignment.ServiceLayer;
using DotNetAssignment.CustomFilters;
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
                UserViewModel uvm = this.us.GetUsersByEmailAndPassword(lvm.Email, lvm.Password);
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
        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }

        [UserAuthorizationFilterAttribute]
        public ActionResult ChangePassword()
        {
            int uid = Convert.ToInt32(Session["CurrentUserID"]);
            UserViewModel uvm = this.us.GetUsersByEmpID(uid);
            EditPasswordViewModel eupvm = new EditPasswordViewModel() { Email = uvm.Email, Password = "", ConfirmPassword = "", EmpID = uvm.EmpID };
            return View(eupvm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [UserAuthorizationFilterAttribute]
        public ActionResult ChangePassword(EditPasswordViewModel eupvm)
        {
            if (ModelState.IsValid)
            {
                eupvm.EmpID = Convert.ToInt32(Session["CurrentUserID"]);
                this.us.UpdateUserPassword(eupvm);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("x", "Invalid data");
                return View(eupvm);
            }
        }

        [UserAuthorizationFilterAttribute]
        public ActionResult ChangeProfile()
        {
            int uid = Convert.ToInt32(Session["CurrentUserID"]);
            UserViewModel uvm = this.us.GetUsersByEmpID(uid);
            EditEmployeeDetailsViewModel eudvm = new EditEmployeeDetailsViewModel() { Email=uvm.Email ,FirstName = uvm.FirstName, LastName = uvm.LastName, Mobile = uvm.Mobile, EmpID = uvm.EmpID, Address=uvm.Address, ImageUrl=uvm.ImageUrl};
            return View(eudvm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [UserAuthorizationFilterAttribute]
        public ActionResult ChangeProfile(EditEmployeeDetailsViewModel eudvm)
        {
            if (ModelState.IsValid)
            {
                eudvm.EmpID = Convert.ToInt32(Session["CurrentUserID"]);
                this.us.UpdateUserDetails(eudvm);
                Session["CurrentUserName"] = eudvm.FirstName+" "+eudvm.LastName;
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("x", "Invalid data");
                return View(eudvm);
            }
        }
    }
}