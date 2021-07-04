using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FacultyInformationSystemEntities
{
    public class User

    {

        [Key]
        [Display(Name = "User Id")]
        public int UserId { get; set; }
        [Display(Name = "User Name")]
        public string UserName { get; set; }
        public string Password { get; set; }
        [Display(Name = "User Type")]
        public string Usertype { get; set; }
        [Display(Name = "Universal ID")]
        public int UniversalId { get; set; }
    }

}
