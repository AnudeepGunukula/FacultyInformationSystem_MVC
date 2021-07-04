using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FacultyInformationSystemEntities
{
    public class Course
    {
        [Key]
        [Display(Name = "Course Id")]

        public int CourseId { get; set; }
        [Display(Name = "Course Name")]
        [Required(ErrorMessage = "Course name is required")]
        [RegularExpression(@"^[a-zA-Z\s]+", ErrorMessage = "Please, use letters in the Course name. Digits are not allowed.")]
        public string CourseName { get; set; }
        [RegularExpression(@"^[0-9]\d{2}", ErrorMessage = "Please, give valid course credits")]
        [Required(ErrorMessage = "Pincode is required")]
        [Display(Name = "Course Credits")]
        public int CourseCredits { get; set; }

        [Display(Name = "Dept Id")]
        public int DeptId { get; set; }
        [Display(Name = "Dept Id")]

        [ForeignKey("DeptId")]
        public Department Dept { get; set; }


        [NotMapped]
        [Display(Name = "Department Name")]
        public string DepartmentName { get; set; }
    }
}
