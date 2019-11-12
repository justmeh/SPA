using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Student.Models
{
    public class User
    {
        [Key]
        [Required]
        public int ID { get; set; }

        [Required]
        [Index(IsUnique = true)]
        [StringLength(10)]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        [Range(1000101010010, 8991231529999)]
        public long CNP { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }


        public static bool LoggedIn()
        {
            return (bool)HttpContext.Current.Session["Logged"];
        }

        public static string getName()
        {
            return (string)HttpContext.Current.Session["Name"];
        }

        public static int? getID()
        {
            return (int)HttpContext.Current.Session["ID"];
        }

    };
}