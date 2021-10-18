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
                RoleViewModel rvm = this.us.GetRoleInformationByRoleID(uvm.RoleID);
                if (uvm != null)
                {
                    Session["CurrentUserID"] = uvm.EmpID;
                    Session["CurrentUserName"] = uvm.FirstName+" "+uvm.LastName;
                    Session["CurrentUserEmail"] = uvm.Email;
                    Session["CurrentUserPassword"] = uvm.Password;
                    Session["CurrentProjectManager"] = uvm.ProjectManagerID;
                    Session["SpecialPermissionStatus"] = uvm.IsSpecialPermission;
                    Session["IsHR"] = uvm.IsHR;
                    Session["CurrentRoleName"] = rvm.RoleName;
                    Session["HRStatus"] = rvm.IsHR;

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
        [HRAuthorizationFilter]
        public ActionResult CreateProfile(UserViewModel uvm)
        {
            if (ModelState.IsValid)
            {
                if (uvm.RoleID == 5)
                {
                    uvm.IsHR = true;
                }
                else
                {
                    uvm.IsHR = false;
                }
                PortalDbContext db = new PortalDbContext();
                List<Project> p = db.Projects.Where(temp => temp.ProjectID == uvm.ProjectID).ToList();
                int pmid = Convert.ToInt32(p[0].ProjectManagerID);
                //Select(temp => temp.ProjectManagerID).
                uvm.ProjectManagerID = pmid;
                uvm.Password = uvm.FirstName + uvm.Mobile;
                this.us.InsertUser(uvm);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("x", "Invalid data");
                return RedirectToAction("CreateProfile", "Account");
            }
        }

        [HRAuthorizationFilter]
        public ActionResult CreateProfile()
        {
            UserViewModel uvm = new UserViewModel();
            return View(uvm);
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        [HRAuthorizationFilter]
        public ActionResult EditProfileByHR(UserViewModel uvm)
        {
            if (ModelState.IsValid)
            {
                //this.us.UpdateUserPassword(uvm);
                if (uvm.RoleID == 5)
                {
                    uvm.IsHR = true;
                }
                else
                {
                    uvm.IsHR = false;
                }
                uvm.EmpID = Convert.ToInt32(Session["SearchEmpID"]);
                this.us.UpdateUserDetails(uvm);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("x", "Invalid data");
                return View(uvm);
            }
        }

        [HRAuthorizationFilter]
        public ActionResult EditProfileByHR()
        {
            int id = Convert.ToInt32(TempData["EmpIDQuery"]);
            UserViewModel uvm = this.us.GetUsersByEmpID(id);
            Session["SearchEmpID"] = uvm.EmpID;
            //ViewBag.Password = uvm.Password;
            return View(uvm);
        }

        [HttpPost]
        [HRAuthorizationFilter]
        public ActionResult EditProfileByEMPID(string EmpID)
        {
            int uid = Convert.ToInt32(EmpID);
            //UserViewModel uvm = this.us.GetUsersByEmpID(uid);
            //return View(uvm);
            TempData["EmpIDQuery"] = uid;
            return RedirectToAction("EditProfileByHR", "Account");
        }

        [HRAuthorizationFilter]
        public ActionResult EditProfileByEMPID()
        {
            return View();
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