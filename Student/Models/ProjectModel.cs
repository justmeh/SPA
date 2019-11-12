using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Student.Models
{
    public class Project
    {
        [Key]
        [Required]
        public int ID { get; set; }

        [Required]
        [Index(IsUnique = true)]
        [StringLength(50)]
        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime Deadline { get; set; }

        public bool Optional { get; set; }
    }
}