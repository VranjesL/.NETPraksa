using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagement.DTOs
{
    public class MemberDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int TotalRentalsAllTime { get; set; }
        public int TotalRentalsNow { get; set; }
        public string Username { get; set; }
    }
}