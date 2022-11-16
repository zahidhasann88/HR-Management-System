using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HR_Management_System.Models
{
    [Table("employee")]
    public partial class Employee
    {
        [Key]
        [Column("employeeid")]
        public int Employeeid { get; set; }
        [Column("employename")]
        public string EmployeName { get; set; }
        [Column("age")]
        public int Age { get; set; }
        [Column("department")]
        public string Department { get; set; }
        [Column("dependentid")]
        public int Dependentid { get; set; }
        [Column("dependentname")]
        public string DependentName { get; set; }
        [Column("position")]
        public string Position { get; set; }
        [Column("joindate")]
        public DateTime Joindate { get; set; }
        [Column("salary")]
        public string Salary { get; set; }
        
    }
}
