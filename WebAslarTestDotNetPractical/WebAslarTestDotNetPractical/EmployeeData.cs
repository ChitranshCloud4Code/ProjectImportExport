using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAslarTestDotNetPractical
{
    public class EmployeeData
    {
        public EmployeeData(int Id, string Name, DateTime JDate, DateTime DOB, string Skills, decimal Salary, string Designation, string Email)
        {
            this.Id = Id;
            this.Name = Name;
            this.JDate = JDate;
            this.DOB = DOB;
            this.Skills = Skills;
            this.Salary = Salary;
            this.Designation = Designation;
            this.Email = Email;
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime JDate { get; set; }
        public DateTime DOB { get; set; }
        public string Skills { get; set; }
        public decimal Salary { get; set; }
        public string Designation { get; set; }
        public string Email { get; set; }
    }
}