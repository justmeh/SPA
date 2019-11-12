using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Student.Models
{
    public class UserRole
    {
        [Key]
        [ForeignKey("UserFK")]
        [Required]
        public int UserID { get; set; }

        public roles Role { get; set; }
       

        public enum roles
        {
            student,
            professor,
            administrator

        }

        public virtual User UserFK { get; set; }
    }
}