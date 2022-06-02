using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SigmaUniversity.Models
{
    [Table("ITADMIN")]
    public class ITAdmin
    {
        [Key]
        public int StaffID { get; set; }
        public string Password { get; set; }
    }
}