using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FacultyInformationSystemEntities
{
    public class CourseTaught
    {
        [Key]
        [Display(Name = "Course Id")]
        public int CourseId { get; set; }
        [Display(Name = "First Date Taught")]
        [Required(ErrorMessage = "First Date Taught is required")]

        [DataType(DataType.Date)]
        public DateTime FirstDateTaught { get; set; }

        [Display(Name = "Subject Id")]
        public int SubjectId { get; set; }
        [ForeignKey("SubjectId")]
        public Subject Subject { get; set; }
        [Display(Name = "Faculty Id")]
        public int FacultyId { get; set; }

        [ForeignKey("FacultyId")]
        public Faculty Faculty { get; set; }


        [NotMapped]
        [Display(Name = "Subject Name")]
        public string SubjectName { get; set; }
    }
}
