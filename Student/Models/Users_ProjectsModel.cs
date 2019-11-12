using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Student.Models
{
    public class Users_Projects
    {
        [Key]
        [Required]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int UsersProjectsID { get; set; }

        [ForeignKey("UserFK")]
        [Required]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.None)]
        public int UserID { get; set; }

        [ForeignKey("ProjectFK")]
        [Required]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.None)]
        public int ProjectID { get; set; }

        [Range(0,10)]
        public int Mark { get; set; }

        public string FileName { get; set; }

        public string FileExtension { get; set; }

        public Byte[] File { get; set; }

        public virtual User UserFK { get; set; }

        public virtual Project ProjectFK { get; set; }

    }

    public class Users_Projects_Comments
    {
        [Key]
        [Required]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [ForeignKey("UserProjectFK")]
        [Required]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.None)]
        public int UsersProjectsID { get; set; }

        public int UserID { get; set; }

        public string Comment { get; set; }

        public virtual Users_Projects UserProjectFK { get; set; }

    }
}