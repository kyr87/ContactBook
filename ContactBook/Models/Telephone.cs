using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ContactBook.Models
{
    public class Telephone
    {
        public int TelephoneId { get; set; }
        [Required]
        [Display(Name ="Phone Number")]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        public int ContactID { get; set; }
        public Contact contact { get; set; }
    }
}