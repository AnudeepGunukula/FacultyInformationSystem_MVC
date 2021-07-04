using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FacultyInformationSystemEntities
{
    public class Grant
    {
        [Key]
        [Display(Name = "Grant Id")]


        public int GrantId { get; set; }
        [Required(ErrorMessage = "Grant Title is required")]
        [RegularExpression(@"^[a-zA-Z\s]+", ErrorMessage = "Please, use letters in the Grant Title. Digits are not allowed.")]
        [Display(Name = "Grant Title")]
        public string GrantTitle { get; set; }
        [Display(Name = "Grant Description")]
        [Required(ErrorMessage = "Grant Description is required")]
        [RegularExpression(@"^[a-zA-Z\s]+", ErrorMessage = "Please, use letters in the Grant Description. Digits are not allowed.")]
        public string GrantDescription { get; set; }
        [Display(Name = "Faculty Id")]
        public int FacultyId { get; set; }

        [ForeignKey("FacultyId")]
        public Faculty Faculty { get; set; }
    }
}
