using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SigmaUniversity.Models
{
    [Table("College")]
    public class College
    {
        [Key]
        public int CollegeCode { get; set; }
        public string CollegeName { get; set; }
        public string DeanFirstName { get; set; }
        public string DeanLastName { get; set; }
        public string DeanEmail { get; set; }
    }
}