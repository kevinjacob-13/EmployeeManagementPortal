using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DotNetAssignment.DomainModels;

namespace DotNetAssignment.Repositories
{
    public interface ILeaveRepository
    {
        void InsertLeave(Leave l);
//        void UpdateLeaveDetails(Leave l);
        void DeleteLeave(int LeaveID);
        List<Leave> GetLeaves();
        List<Leave> GetLeaveByLeaveID(int LeaveID);
        List<Leave> GetLeavesByEmpID(int EmpID);
    }
    public class LeaveRepository : ILeaveRepository
    {
        PortalDbContext db;

        public LeaveRepository()
        {
            db = new PortalDbContext();
        }

        public void InsertLeave(Leave l)
        {
            db.Leaves.Add(l);
            db.SaveChanges();
        }

        //public void UpdateLeaveDetails(Leave l)
        //{
        //    Leave qt = db.Leaves.Where(temp => temp.LeaveID == l.LeaveID).FirstOrDefault();
        //    if (qt != null)
        //    {
        //        qt.QuestionName = q.QuestionName;
        //        qt.QuestionDateAndTime = q.QuestionDateAndTime;
        //        qt.CategoryID = q.CategoryID;
        //        db.SaveChanges();
        //    }
        //}

        public void DeleteLeave(int LeaveID)
        {
            Leave qt = db.Leaves.Where(temp => temp.LeaveID == LeaveID).FirstOrDefault();
            if (qt != null)
            {
                db.Leaves.Remove(qt);
                db.SaveChanges();
            }
        }

        public List<Leave> GetLeaves()
        {
            List<Leave> lv = db.Leaves.OrderByDescending(temp => temp.StartDate).ToList();
            return lv;
        }

        public List<Leave> GetLeaveByLeaveID(int LeaveID)
        {
            List<Leave> lv = db.Leaves.Where(temp => temp.LeaveID == LeaveID).ToList();
            return lv;
        }
        public List<Leave> GetLeavesByEmpID(int EmpID)
        {
            List<Leave> lv = db.Leaves.Where(temp => temp.EmpID == EmpID).ToList();
            return lv;
        }
    }
}