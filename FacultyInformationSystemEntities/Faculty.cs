using FacultyInformationSystemEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FacultyInformationSystemEntities
{
    public class Faculty
    {

        [Key]
        [Display(Name = "Faculty Id")]
        public int FacultyId { get; set; }

        [Display(Name = "First Name")]
        [Required(ErrorMessage = "First name is required")]
        [RegularExpression(@"^[a-zA-Z\s]+", ErrorMessage = "Please, use letters in the first name. Digits are not allowed.")]
        [StringLength(int.MaxValue, MinimumLength = 2, ErrorMessage = "First name must have at least 2 characters")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "Last name is required")]
        [RegularExpression(@"^[a-zA-Z\s]+", ErrorMessage = "Please, use letters in the last name. Digits are not allowed.")]
        [StringLength(int.MaxValue, MinimumLength = 2, ErrorMessage = "First name must have at least 2 characters")]

        public string LastName { get; set; }


        [Required(ErrorMessage = "Address is required")]
        [RegularExpression(@"^[A-Za-z0-9'\.\-\s\,]+", ErrorMessage = "Address should not contain special characters")]
        public string Address { get; set; }

        [RegularExpression(@"^[a-zA-Z\s]+", ErrorMessage = "Please, use letters in the City. Digits are not allowed.")]
        [Required(ErrorMessage = "City is required")]

        public string City { get; set; }

        [RegularExpression(@"^[a-zA-Z\s]+", ErrorMessage = "Please, use letters in the State. Digits are not allowed.")]
        [Required(ErrorMessage = "State is required")]
        public string State { get; set; }

        [RegularExpression(@"^[0-9]\d{5}", ErrorMessage = "Please, only give 6 digits pincode")]
        [Required(ErrorMessage = "Pincode is required")]
        public int Pincode { get; set; }


        [Display(Name = "Mobile Number")]
        [RegularExpression(@"^[0-9]\d{9}", ErrorMessage = "Mobile Number should be 10 digits")]
        [Required(ErrorMessage = "Mobile Number is required")]
        public string MobileNo { get; set; }


        [Display(Name = "Hire Date")]
        //[Range(typeof(DateTime), "1/1/1901", "27/4/2021")]
        [Required(ErrorMessage = "Hire Date is required")]

        [DataType(DataType.Date)]
        public DateTime HireDate { get; set; }


        [Required]
        [RegularExpression(@"[a-z0-9\.\-+_]+@[a-z0-9\.\-+_]+\.[a-z]+", ErrorMessage = "Please enter valid email")]

        [Display(Name = "Email Address")]
        public string EmailAddress { get; set; }

        [Required]
        [Display(Name = "Date of Birth")]
        [DataType(DataType.Date)]
        public DateTime DateofBirth { get; set; }
        public int DeptId { get; set; }


        [ForeignKey("DeptId")]
        public Department Dept { get; set; }
        public int DesignationId { get; set; }

        [ForeignKey("DesignationId")]
        public Designation Designation { get; set; }

        [NotMapped]
        [Display(Name = "Department Name")]
        public string DepartmentName { get; set; }
        [NotMapped]
        [Display(Name = "Designation Name")]
        public string DesignationName { get; set; }

        [NotMapped]
        [Display(Name = "Faculty Name")]
        public string FacultyName { get; set; }


    }
}
