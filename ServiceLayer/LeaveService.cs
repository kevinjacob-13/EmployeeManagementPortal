using System;
using System.Collections.Generic;
using System.Linq;
using DotNetAssignment.ViewModels;
using DotNetAssignment.DomainModels;
using DotNetAssignment.Repositories;
using AutoMapper;
using AutoMapper.Configuration;

namespace DotNetAssignment.ServiceLayer
{
    public interface ILeavesService
    {
        void InsertLeave(LeaveViewModel levm);
        void UpdateLeave(int LeaveID, string LeaveStatus);
        void DeleteLeave(int LeaveID);
        List<LeaveViewModel> GetLeaves();
        LeaveViewModel GetLeaveByLeaveID(int LeaveID);
        List<LeaveViewModel> GetLeavesByEmpID(int EmpID);
        List<LeaveViewModel> GetLeavesByPMID(int PMID);
    }
    public class LeaveService : ILeavesService
    {
        ILeaveRepository lr;

        public LeaveService()
        {
            lr = new LeaveRepository();
        }

        public void InsertLeave(LeaveViewModel levm)
        {
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<LeaveViewModel, Leave>(); cfg.IgnoreUnmapped(); });
            IMapper mapper = config.CreateMapper();
            Leave l = mapper.Map<LeaveViewModel, Leave>(levm);
            lr.InsertLeave(l);
        }

        public void UpdateLeave(int LeaveID, string LeaveStatus)
        {
            lr.UpdateLeave(LeaveID,LeaveStatus);
        }

        public void DeleteLeave(int LeaveID)
        {
            lr.DeleteLeave(LeaveID);
        }

        public List<LeaveViewModel> GetLeaves()
        {
            List<Leave> l = lr.GetLeaves();
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<Leave, LeaveViewModel>(); cfg.IgnoreUnmapped(); });
            IMapper mapper = config.CreateMapper();
            List<LeaveViewModel> levm = mapper.Map<List<Leave>, List<LeaveViewModel>>(l);
            return levm;
        }

        public LeaveViewModel GetLeaveByLeaveID(int LeaveID)
        {
            Leave l = lr.GetLeaveByLeaveID(LeaveID).FirstOrDefault();
            LeaveViewModel levm = null;
            if (l != null)
            {
                var config = new MapperConfiguration(cfg => { cfg.CreateMap<Leave, LeaveViewModel>(); cfg.IgnoreUnmapped(); });
                IMapper mapper = config.CreateMapper();
                levm = mapper.Map<Leave, LeaveViewModel>(l);
            }
            return levm;
        }
        public List<LeaveViewModel> GetLeavesByEmpID(int EmpID)
        {
            List<Leave> l = lr.GetLeavesByEmpID(EmpID);
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<Leave, LeaveViewModel>(); cfg.IgnoreUnmapped(); });
            IMapper mapper = config.CreateMapper();
            List<LeaveViewModel> levm = mapper.Map<List<Leave>, List<LeaveViewModel>>(l);
            return levm;
        }

        public List<LeaveViewModel> GetLeavesByPMID(int PMID)
        {
            List<Leave> l = lr.GetLeavesByPMID(PMID);
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<Leave, LeaveViewModel>(); cfg.IgnoreUnmapped(); });
            IMapper mapper = config.CreateMapper();
            List<LeaveViewModel> levm = mapper.Map<List<Leave>, List<LeaveViewModel>>(l);
            return levm;
        }
    }
}