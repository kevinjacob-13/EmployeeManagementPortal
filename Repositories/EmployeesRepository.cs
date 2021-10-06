﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DotNetAssignment.DomainModels;

namespace DotNetAssignment.Repositories
{
    public interface IEmployeesRepository
    {
        //void InsertUser(User u);
        void UpdateUserDetails(Employee u);
        void UpdateUserPassword(Employee u);
        //void DeleteUser(int uid);
        //List<User> GetUsers();
        List<Employee> GetUsersByEmailAndPassword(string Email, string Password);
        //List<User> GetUsersByEmail(string Email);
        List<Employee> GetUsersByEmpID(int EmpID);
        //int GetLatestUserID();
    }
    public class EmployeesRepository : IEmployeesRepository
    {
        PortalDbContext db;

        public EmployeesRepository()
        {
            db = new PortalDbContext();
        }

        //public void InsertUser(User u)
        //{
        //    db.Users.Add(u);
        //    db.SaveChanges();
        //}

        public void UpdateUserDetails(Employee u)
        {
            Employee us = db.Employees.Where(temp => temp.EmpID == u.EmpID).FirstOrDefault();
            if (us != null)
            {
                us.Address = u.Address;
                us.Mobile = u.Mobile;
                us.ImageUrl = u.ImageUrl;

                db.SaveChanges();
            }
        }

        public void UpdateUserPassword(Employee u)
        {
            Employee us = db.Employees.Where(temp => temp.EmpID == u.EmpID).FirstOrDefault();
            if (us != null)
            {
                us.PasswordHash = u.PasswordHash;
                db.SaveChanges();
            }
        }

        //public void DeleteUser(int uid)
        //{
        //    User us = db.Users.Where(temp => temp.UserID == uid).FirstOrDefault();
        //    if (us != null)
        //    {
        //        db.Users.Remove(us);
        //        db.SaveChanges();
        //    }
        //}

        //public List<User> GetUsers()
        //{
        //    List<User> us = db.Users.Where(temp => temp.IsAdmin == false).OrderBy(temp => temp.Name).ToList();
        //    return us;
        //}

        public List<Employee> GetUsersByEmailAndPassword(string Email, string PasswordHash)
        {
            List<Employee> us = db.Employees.Where(temp => temp.Email == Email && temp.PasswordHash == PasswordHash).ToList();
            return us;
        }

        //public List<User> GetUsersByEmail(string Email)
        //{
        //    List<User> us = db.Users.Where(temp => temp.Email == Email).ToList();
        //    return us;
        //}

        public List<Employee> GetUsersByEmpID(int EmpID)
        {
            List<Employee> us = db.Employees.Where(temp => temp.EmpID == EmpID).ToList();
            return us;
        }

        //public int GetLatestUserID()
        //{
        //    int uid = db.Users.Select(temp => temp.UserID).Max();
        //    return uid;
        //}
    }
}