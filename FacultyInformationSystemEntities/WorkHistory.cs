using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FacultyInformationSystemEntities
{
    public class WorkHistory
    {

        [Key]
        [Display(Name = "WorkHistory Id")]
        public int WorkHistoryId { get; set; }
        [Display(Name = "Organization Name")]
        [Required(ErrorMessage = "Organization Name is required")]
        [RegularExpression(@"^[a-zA-Z\s]+", ErrorMessage = "Please, use letters in the Grant Title. Digits are not allowed.")]
        public string Organization { get; set; }
        [Display(Name = "Job Title")]
        [Required(ErrorMessage = "Job Title is required")]
        [RegularExpression(@"^[a-zA-Z\s]+", ErrorMessage = "Please, use letters in the Job Title. Digits are not allowed.")]
        public string JobTitle { get; set; }
        [Display(Name = "Job Begin Date")]
        [Required(ErrorMessage = "Job Begin Date is required")]
        [DataType(DataType.Date)]
        public DateTime JobBeginDate { get; set; }
        [Display(Name = "Job End Date")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Job End Date is required")]
        public DateTime JobEndDate { get; set; }
        [Display(Name = "Job Responsibilities")]
        [Required(ErrorMessage = "Job Responsibilities is required")]
        [RegularExpression(@"^[a-zA-Z\s]+", ErrorMessage = "Please, use letters in the Job Title. Digits are not allowed.")]
        public string JobResponsibilities { get; set; }
        [Display(Name = "Job Type")]
        [Required(ErrorMessage = "Job Type is required")]
        [RegularExpression(@"^[a-zA-Z\s]+", ErrorMessage = "Please, use letters in the Job Title. Digits are not allowed.")]
        public string JobType { get; set; }

        [Display(Name = "Faculty Id")]
        public int FacultyId { get; set; }

        [ForeignKey("FacultyId")]
        public Faculty Faculty { get; set; }
    }
}
