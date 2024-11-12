using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryManagement.Data;
using LibraryManagement.Interfaces;
using LibraryManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Repository
{
    public class BookRentalRepository : IBookRentalRepository
    {   
        private readonly ApplicationDBContext _context;
        public BookRentalRepository(ApplicationDBContext context)
        {
            _context = context;
        }
        public async Task<List<Book>> GetMemberPortfolio(Member member)
        {
            return await _context.BookRentals.Where(m => m.MemberId == member.Id).Select(book => new Book
            {
                Id = book.BookId,
                BookName = book.Book.BookName,
                PublicationDate = book.Book.PublicationDate,
                ISBN = book.Book.ISBN,
                Status = book.Book.Status
            }).ToListAsync();
        }
    }
}