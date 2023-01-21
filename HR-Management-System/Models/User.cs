using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HR_Management_System.Models
{
    [Table("user")]
    public class User
    {
        [Key]
        [Column("userid")]
        public int UserId { get; set; }
        [Column("displayname")]
        public string? DisplayName { get; set; }
        [Column("username")]
        public string? UserName { get; set; }
        [Column("email")]
        public string? Email { get; set; }
        [Column("password")]
        public string? Password { get; set; }
        [Column("createddate")]
        public DateTime? CreatedDate { get; set; }
    }
}
