using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FacultyInformationSystemEntities
{
    public class Designation
    {
        [Key]
        [Display(Name = "Designation Id")]
        public int DesignationId { get; set; }
        [Required(ErrorMessage = "Designation name is required")]
        [RegularExpression(@"^[a-zA-Z\s]+", ErrorMessage = "Please, use letters in the Designation name. Digits are not allowed.")]
        [Display(Name = "Designation Name")]
        public string DesignationName { get; set; }
    }
}
