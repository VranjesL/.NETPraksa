using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace LibraryManagement.Models
{   
    // don't have to have username/passwrod fields because we inherit it from identityUser
    public class Member : IdentityUser
    {   
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public int TotalRentals { get; set; }
        public List<BookRental> BookRentals { get; set; } = new List<BookRental>();
    }
}