using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FacultyInformationSystemEntities
{
    public class Subject

    {

        [Key]
        [Display(Name = "Subject Id")]
        public int SubjectId { get; set; }

        [Required(ErrorMessage = "Subject name is required")]
        [RegularExpression(@"^[a-zA-Z\s]+", ErrorMessage = "Please, use letters in the Subject name. Digits are not allowed.")]
        [Display(Name = "Subject Name")]
        public string SubjectName { get; set; }
    }

}
