using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FacultyInformationSystemEntities
{
    public class Department
    {
        [Key]
        [Display(Name = "Dept Id")]
        public int DeptId { get; set; }
        [Required(ErrorMessage = "Department name is required")]
        [RegularExpression(@"^[a-zA-Z\s]+", ErrorMessage = "Please, use letters in the Department name. Digits are not allowed.")]
        [Display(Name = "Department Name")]
        public string DeptName { get; set; }
    }
}
