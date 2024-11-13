using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagement.Models
{   
    [Table("Authors")]
    public class Author
    {   
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public DateTime BirthOfDate { get; set; }
        
        // list of books that author has written
        public List<Book> Books { get; set; } = new List<Book>();
    }
}