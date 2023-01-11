using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HR_Management_System.Models
{
    [Table("salary")]
    public class Salary
    {
        [Key]
        [Column("s_id")]
        public int Id { get; set; }
        [Column("em_id")]
        public int EmployeeId { get; set; }
        [Column("s_amount")]
        public int SalaryAmount { get; set; }
    }
}
