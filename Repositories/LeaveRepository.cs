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
        void UpdateLeave(int LeaveID, string LeaveStatus);
        void DeleteLeave(int LeaveID);
        List<Leave> GetLeaves();
        List<Leave> GetLeaveByLeaveID(int LeaveID);
        List<Leave> GetLeavesByEmpID(int EmpID);
        List<Leave> GetLeavesByPMID(int PMID);
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

        public void UpdateLeave(int LeaveID, string LeaveStatus)
        {
            Leave qt = db.Leaves.Where(temp => temp.LeaveID == LeaveID).FirstOrDefault();
            if (qt != null)
            {
                qt.LeaveStatus = LeaveStatus;
                db.SaveChanges();
            }
        }

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
        public List<Leave> GetLeavesByPMID(int PMID)
        {
            List<Leave> lv = db.Leaves.Where(temp => temp.ProjectManagerID == PMID && temp.LeaveStatus == "Pending").ToList();
            return lv;
        }
    }
}