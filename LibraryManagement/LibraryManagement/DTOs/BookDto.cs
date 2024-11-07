using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryManagement.Models;

namespace LibraryManagement.DTOs
{
    public class BookDto
    {
        public int Id { get; set; }
        public string BookName { get; set; } = string.Empty;
        public DateTime PublicationDate { get; set; }
        // has to be unique ISBN
        public string ISBN { get; set; } = string.Empty;
        // default status is available
        public string Status { get; set; } = "Available";
        public string AuthorFirstName { get; set; } = string.Empty;
        public string AuthorLastName { get; set; } = string.Empty;
    }
}