using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DotNetAssignment.DomainModels
{
    public class Employee
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EmpID { get; set; }

        [Index(IsUnique = true)]
        [Required]
        [StringLength(50)]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string PasswordHash { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [Required]
        [MaxLength(10, ErrorMessage = "Phone Number Should be of 10 digits")]
        [MinLength(10, ErrorMessage = "Phone Number Should be of 10 digits")]
        [Index(IsUnique = true)]
        public string Mobile { get; set; }
        public int RoleID { get; set; } //create table for role
        public DateTime? DateOfBirth { get; set; }
        public string Address { get; set; }
        public int ProjectID { get; set; } // reporting to
        public string ImageUrl { get; set; }


        [ForeignKey("RoleID")]
        public virtual Role RID { get; set; }

        [ForeignKey("ProjectID")]
        public virtual Project PID { get; set; }

    }
}