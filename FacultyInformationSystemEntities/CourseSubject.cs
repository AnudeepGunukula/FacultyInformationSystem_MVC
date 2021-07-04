using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FacultyInformationSystemEntities
{
    public class CourseSubject
    {
        [Key]
        [Display(Name = "Coure Id")]
        public int CourseId { get; set; }
        [Display(Name = "Subject Id")]
        public int SubjectId { get; set; }
        [Display(Name = "Subject Id")]
        [ForeignKey("SubjectId")]
        public Subject Subject { get; set; }
    }
}
