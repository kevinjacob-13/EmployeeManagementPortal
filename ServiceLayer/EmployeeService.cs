using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using AutoMapper.Configuration;
using DotNetAssignment.ViewModels;
using DotNetAssignment.DomainModels;
using DotNetAssignment.Repositories;

namespace DotNetAssignment.ServiceLayer
{
    public interface IEmployeesService
    {
 //       int InsertUser(RegisterViewModel uvm);
        void UpdateUserDetails(EditEmployeeDetailsViewModel uvm);
        void UpdateUserPassword(EditPasswordViewModel uvm);
//        void DeleteUser(int uid);
//       List<UserViewModel> GetUsers();
        UserViewModel GetUsersByEmailAndPassword(string Email, string Password);
        //UserViewModel GetUsersByEmail(string Email);
        UserViewModel GetUsersByEmpID(int EmpID);
    }
    public class EmployeeService : IEmployeesService
    {
        readonly IEmployeesRepository ur;

        public EmployeeService()
        {
            ur = new EmployeesRepository();
        }

        //public int InsertUser(RegisterViewModel uvm)
        //{
        //    var config = new MapperConfiguration(cfg => { cfg.CreateMap<RegisterViewModel, User>(); cfg.IgnoreUnmapped(); });
        //    IMapper mapper = config.CreateMapper();
        //    User u = mapper.Map<RegisterViewModel, User>(uvm);
        //    u.PasswordHash = SHA256HashGenerator.GenerateHash(uvm.Password);
        //    ur.InsertUser(u);
        //    int uid = ur.GetLatestUserID();
        //    return uid;
        //}

        public void UpdateUserDetails(EditEmployeeDetailsViewModel uvm)
        {
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<EditEmployeeDetailsViewModel, Employee>(); cfg.IgnoreUnmapped(); });
            IMapper mapper = config.CreateMapper();
            Employee u = mapper.Map<EditEmployeeDetailsViewModel, Employee>(uvm);
            ur.UpdateUserDetails(u);
        }

        public void UpdateUserPassword(EditPasswordViewModel uvm)
        {
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<EditPasswordViewModel, Employee>(); cfg.IgnoreUnmapped(); });
            IMapper mapper = config.CreateMapper();
            Employee u = mapper.Map<EditPasswordViewModel, Employee>(uvm);
            u.PasswordHash = SHA256HashGenerator.GenerateHash(uvm.Password);
            ur.UpdateUserPassword(u);
        }

        //public void DeleteUser(int uid)
        //{
        //    ur.DeleteUser(uid);
        //}

        //public list<userviewmodel> getusers()
        //{
        //    list<user> u = ur.getusers();
        //    var config = new mapperconfiguration(cfg => { cfg.createmap<user, userviewmodel>(); cfg.ignoreunmapped(); });
        //    imapper mapper = config.createmapper();
        //    list<userviewmodel> uvm = mapper.map<list<user>, list<userviewmodel>>(u);
        //    return uvm;
        //}

        public UserViewModel GetUsersByEmailAndPassword(string Email, string Password)
        {
            Employee u = ur.GetUsersByEmailAndPassword(Email, SHA256HashGenerator.GenerateHash(Password)).FirstOrDefault();
            UserViewModel uvm = null;
            if (u != null)
            {
                var config = new MapperConfiguration(cfg => { cfg.CreateMap<Employee, UserViewModel>(); cfg.IgnoreUnmapped(); });
                IMapper mapper = config.CreateMapper();
                uvm = mapper.Map<Employee, UserViewModel>(u);
            }
            return uvm;
        }

        //public UserViewModel GetUsersByEmail(string Email)
        //{
        //    User u = ur.GetUsersByEmail(Email).FirstOrDefault();
        //    UserViewModel uvm = null;
        //    if (u != null)
        //    {
        //        var config = new MapperConfiguration(cfg => { cfg.CreateMap<User, UserViewModel>(); cfg.IgnoreUnmapped(); });
        //        IMapper mapper = config.CreateMapper();
        //        uvm = mapper.Map<User, UserViewModel>(u);
        //    }
        //    return uvm;
        //}

        public UserViewModel GetUsersByEmpID(int UserID)
        {
            Employee u = ur.GetUsersByEmpID(UserID).FirstOrDefault();
            UserViewModel uvm = null;
            if (u != null)
            {
                var config = new MapperConfiguration(cfg => { cfg.CreateMap<Employee, UserViewModel>(); cfg.IgnoreUnmapped(); });
                IMapper mapper = config.CreateMapper();
                uvm = mapper.Map<Employee, UserViewModel>(u);
            }
            return uvm;
        }
    }
}


