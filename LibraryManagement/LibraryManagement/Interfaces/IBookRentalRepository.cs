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
        //Task<Book> BorrowBookAsync(int bookId, string memberId);
        //Task<Book> ReturnBookAsync(int bookId, string memberId);
        Task<BookRental> GetBookRentalByBookName(Member member, string bookName);
        //Task<BookRental> BorrowBook()
        Task<Member> FindMemberByUsername(string username);
    }
}