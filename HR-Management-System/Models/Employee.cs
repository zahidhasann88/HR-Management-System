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
        [Column("emp_id")]
        public int Id { get; set; }
        [Column("name")]
        public string Name { get; set; }
        [Column("email")]
        public string Email { get; set; }
        [Column("mobile")]
        public string Mobile { get; set; }
        [Column("designation")]
        public string Designation { get; set; }
        [Column("joindate")]
        public DateTime Joindate { get; set; }
        public virtual Salary Salary { get; set; }
    }
}
