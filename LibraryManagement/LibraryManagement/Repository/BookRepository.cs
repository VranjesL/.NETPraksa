using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryManagement.Data;
using LibraryManagement.Interfaces;
using LibraryManagement.Mappers;
using LibraryManagement.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Repository
{
    public class BookRepository : IBookRepository
    {   
        private readonly ApplicationDBContext _context;
        public BookRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<Book> CreateBookAsync(Book bookModel)
        {
            await _context.Books.AddAsync(bookModel);
            await _context.SaveChangesAsync();
            return bookModel;
        }

        public async Task<Book?> DeleteBookAsync(int id)
        {
            var bookModel = await _context.Books.FirstOrDefaultAsync(b => b.Id == id);

            if(bookModel == null) return null;

            _context.Books.Remove(bookModel);
            await _context.SaveChangesAsync();
            return bookModel;
        }

        public async Task<List<Book>> GetAllBooksAsync()
        {
            return await _context.Books.ToListAsync();
        }

        public async Task<Book?> GetBookByIdAsync(int id)
        {
            var book = await _context.Books.FirstOrDefaultAsync(b => b.Id == id);

            if (book == null) return null;

            return book;
        }

        public Task<Book> UpdateBookAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> BookExists(int id)
        {
            return _context.Books.AnyAsync(b => b.Id == id);
        }
    }
}