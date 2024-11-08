using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using LibraryManagement.Models;

namespace LibraryManagement.DTOs
{
    public class CreateBookRequestDto
    {   
        [Required]
        [MinLength(3, ErrorMessage = "BookName must be longer than 3 characters!")]
        [MaxLength(100, ErrorMessage = "BookName cannot be over 100 characters!")]
        public string BookName { get; set; } = string.Empty;
        public DateTime PublicationDate { get; set; }
        // has to be unique ISBN
        [Required]
        [StringLength(13, MinimumLength = 13, ErrorMessage = "ISBN has to be exactly 13 characters long!")]
        public string ISBN { get; set; } = string.Empty;
        // default status is available
        [Required]
        [RegularExpression("^(available|Available|Rented|rented)$", ErrorMessage = "Status has to be either 'available' or 'rented'!")]
        public string Status { get; set; } = "Available";
    }
}