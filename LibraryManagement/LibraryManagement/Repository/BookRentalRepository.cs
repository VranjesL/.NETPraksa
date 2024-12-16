using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
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
            if (member == null)
            {
                throw new ArgumentNullException(nameof(member), "Member cannot be null.");
            }   

            return await _context.BookRentals.Include(br => br.Book).Where(br => br.MemberId == member.Id).Select(br => new Book
            {
                Id = br.BookId,
                BookName = br.Book.BookName,
                PublicationDate = br.Book.PublicationDate,
                ISBN = br.Book.ISBN,
                Status = br.Book.Status
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

        public async Task<Member> FindMemberByUsername(string username)
        {
            var member =  await _context.Members.FirstOrDefaultAsync(m => m.UserName == username);
        
            if(member == null) return null;

            return member;
        }

        public async Task<List<Book>> GetMostRentedBooksInLastXDays(int x)
        {   
            DateTime targetDate = DateTime.UtcNow.AddDays(-x);
            return await _context.BookRentals.Include(b => b.Book).ThenInclude(b => b.Author).Where(b => b.RentalDate >= targetDate).Select(b => b.Book).ToListAsync();
        }

        public async Task<List<Book>> GetBooksRentedByMember(string memberUsername)
        {
            return await _context.BookRentals.Include(br => br.Book)
                                            .ThenInclude(br => br.Author)
                                            .Where(br => br.Member.UserName == memberUsername)
                                            .Select(br => br.Book)
                                            .ToListAsync();
        }
    }
}