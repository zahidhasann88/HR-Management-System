using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HR_Management_System.Models
{
    public partial class Employee
    {
        public int? Employeeid { get; set; }
        public string EmployeName { get; set; }
        public int Age { get; set; }
        public string Department { get; set; }
        public int Dependentid { get; set; }
        public string DependentName { get; set; }
        public string Position { get; set; }
        public DateTime Joindate { get; set; }
        public string Salary { get; set; }
    }
}
