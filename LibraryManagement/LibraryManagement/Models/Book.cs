using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagement.Models
{
    public class Book
    {   
        public int Id { get; set; }
        public string BookName { get; set; } = string.Empty;
        public DateTime PublicationDate { get; set; }
        // has to be unique ISBN
        public string ISBN { get; set; } = string.Empty;
        // default status is available
        public string Status { get; set; } = "Available";
        public int AuthorId { get; set; }
        public Author Author { get; set; }
        public BookRental BookRental { get; set; }
    }
}