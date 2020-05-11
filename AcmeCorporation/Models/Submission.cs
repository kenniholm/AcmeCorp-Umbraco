using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AcmeCorporation.Models
{
    public class Submission
    {
        [Key]
        public int SubmissionId { get; set; }

        [Required(ErrorMessage = "Please enter name"), MaxLength(30), MinLength(2)]
        [DataType(DataType.Text)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Please enter last name"), MaxLength(30), MinLength(2)]
        [DataType(DataType.Text)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Please enter a valid email address")]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email address")]
        public string EmailAdress { get; set; }

        [Required(ErrorMessage = "Please enter serial provided from your purchase receipt")]
        [Display(Name = "Product serial number")]
        public Guid ProductSerial { get; set; }
    }
}
