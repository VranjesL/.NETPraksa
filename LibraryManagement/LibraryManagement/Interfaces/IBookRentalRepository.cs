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
        Task<BookRental> CreateAsync(BookRental bookRental);
        Task<BookRental> ReturnBook(Member member, string bookName);
        Task<BookRental> GetBookRentalByBookName(Member member, string bookName);
        Task<Member> FindMemberByUsername(string username);
        Task<List<Book>> GetMostRentedBooksInLastXDays(int x);
        Task<List<Book>> GetBooksRentedByMember(string memberUsername);
    }
}