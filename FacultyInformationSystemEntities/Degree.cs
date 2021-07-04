
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FacultyInformationSystemEntities
{
    public class Degree
    {
        [Key]
        [Display(Name = "Degree Id")]
        public int DegreeId { get; set; }
        [Display(Name = "Degree")]
        public string Degree1 { get; set; }
        [Display(Name = "Specialization")]
        public string Specialization { get; set; }
        [Display(Name = "Degree Year")]
        public string DegreeYear { get; set; }
        public string Grade { get; set; }
        [Display(Name = "Faculty Id")]
        public int FacultyId { get; set; }

        [ForeignKey("FacultyId")]
        public Faculty faculty { get; set; }
    }
}
