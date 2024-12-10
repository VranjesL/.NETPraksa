using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagement.DTOs
{
    public class BookTimesRentedDto
    {
        public int Id { get; set; }
        public string BookName { get; set; } = string.Empty;
        public string AuthorFirstName { get; set; } = string.Empty;
        public string AuthorLastName { get; set; } = string.Empty;
        public int TimesRented { get; set; }
    }
}