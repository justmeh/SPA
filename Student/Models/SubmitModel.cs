using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Student.Models
{
    public class SubmitModel
    {
        [Required]
        [Key]
        public HttpPostedFileBase File { get; set; }
    }
}