using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryManagement.Data;
using LibraryManagement.Interfaces;
using LibraryManagement.Models;
using Microsoft.AspNetCore.Http.HttpResults;
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

        /*public async Task<Book> BorrowBookAsync(int bookId, string memberId)
        {
            var book = await _context.Books.FirstOrDefaultAsync(b => b.Id == bookId);

            if(book == null || book.Status != "Available") return null;

            var borrowedBooks = await _context.BookRentals
                .Where(br => br.MemberId == memberId && !br.isReturned).CountAsync();

            if(borrowedBooks >= 5) return null;

            var bookRental = new BookRental
            {
                BookId = bookId,
                MemberId = memberId,
                RentalDate = DateTime.UtcNow
            };
            
            book.Status = "Rented";

            await _context.BookRentals.AddAsync(bookRental);
            _context.Books.Update(book);
            await _context.SaveChangesAsync();

            return book;
        }

        public async Task<Book> ReturnBookAsync(int bookId, string memberId)
        {
            var bookRental = await _context.BookRentals.FirstOrDefaultAsync(br => br.BookId == bookId && br.MemberId == memberId && !br.isReturned);

            if(bookRental == null) return null;

            if(bookRental.RentalDate.AddDays(15) < DateTime.UtcNow) throw new InvalidOperationException("The book is overdue and should be returned.");

            bookRental.ReturnDate = DateTime.UtcNow;

            var book = await _context.Books.FirstOrDefaultAsync(b => b.Id == bookId);
            book.Status = "Available";

            _context.BookRentals.Update(bookRental);
            _context.Books.Update(book);
            await _context.SaveChangesAsync();
            return book;
        }*/



        public async Task<BookRental> CreateAsync(BookRental bookRental)
        {   
            var existingRental = await _context.BookRentals
                .FirstOrDefaultAsync(br => br.BookId == bookRental.BookId && br.MemberId == bookRental.MemberId && br.ReturnDate == null);

            if (existingRental != null)
            {
                // Ako knjiga nije vraćena, vraćamo grešku
                return null; // Ili možete baciti exception u zavisnosti od logike
            }
            await _context.BookRentals.AddAsync(bookRental);
            await _context.SaveChangesAsync();
            return bookRental;
        }

        public async Task<BookRental> GetBookRentalByBookName(Member member, string bookName)
        {
            return await _context.BookRentals.FirstOrDefaultAsync(br => br.MemberId == member.Id && br.Book.BookName.ToLower() == bookName.ToLower());
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

        public async Task<BookRental> ReturnBook(Member member, string bookName)
        {
            var bookRentalModel = await _context.BookRentals.Include(br => br.Book).FirstOrDefaultAsync(x => x.MemberId == member.Id && x.Book.BookName.ToLower() == bookName.ToLower() && x.ReturnDate == null);

            if(bookRentalModel == null) return null;
            
            bookRentalModel.Book.Status = "Available";

            _context.Books.Update(bookRentalModel.Book);
            _context.BookRentals.Remove(bookRentalModel);
            await _context.SaveChangesAsync();

            return bookRentalModel;
        }
    }
}