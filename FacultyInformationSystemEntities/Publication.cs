using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FacultyInformationSystemEntities
{
    public class Publication
    {
        [Key]
        [Display(Name = "Pubication Id")]
        public int PublicationId { get; set; }


        [Display(Name = "Publication Title")]
        [Required(ErrorMessage = "Publication Title is required")]
        [RegularExpression(@"^[a-zA-Z\s]+", ErrorMessage = "Please, use letters in the Publication Title. Digits are not allowed.")]
        public string PublicationTitle { get; set; }

        [Display(Name = "Article Name")]
        [Required(ErrorMessage = "Article name is required")]
        [RegularExpression(@"^[a-zA-Z\s]+", ErrorMessage = "Please, use letters in the Article name. Digits are not allowed.")]

        public string ArticleName { get; set; }


        [Display(Name = "Publisher Name")]
        [Required(ErrorMessage = "Publisher name is required")]
        [RegularExpression(@"^[a-zA-Z\s]+", ErrorMessage = "Please, use letters in the Publisher name. Digits are not allowed.")]
        public string PublisherName { get; set; }

        [Display(Name = "Publisher Location")]
        [Required(ErrorMessage = "Publisher Location is required")]
        [RegularExpression(@"^[a-zA-Z\s]+", ErrorMessage = "Please, use letters in the Publisher Location. Digits are not allowed.")]
        public string PublisherLocation { get; set; }
        [Display(Name = "Citiation Date")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Citiation Date is required")]
        public DateTime CitationDate { get; set; }

        [Display(Name = "Faculty Id")]
        public int FacultyId { get; set; }

        [ForeignKey("FacultyId")]
        public Faculty Faculty { get; set; }
    }
}
