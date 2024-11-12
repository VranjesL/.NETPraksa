using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryManagement.Models;

namespace LibraryManagement.Interfaces
{
    public interface IBookRentalRepository
    {
        Task<List<Book>> GetMemberPortfolio(Member member);
    }
}