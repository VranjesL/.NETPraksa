using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagement.Models
{   
    [Table("BookRentals")]
    public class BookRental
    {
        public int Id { get; set; }

        /* string zato sto u modelu member nasledjujemo
           identity koji ima ugradjen string Id */
        public string MemberId { get; set; }
        public Member Member { get; set; }

        public int BookId { get; set; }
        public Book Book { get; set; }
        
        public DateTime? RentalDate { get; set; }
        public DateTime? ReturnDate { get; set; }
    }
}