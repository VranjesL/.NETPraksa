using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryManagement.Data;
using LibraryManagement.DTOs;
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
            return await _context.Books.Include(b => b.Author).ToListAsync();
        }

        public async Task<Book?> GetBookByIdAsync(int id)
        {
            return await _context.Books.Include(b => b.Author).FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task<Book> UpdateBookAsync(int id, UpdateBookRequestDto bookDto)
        {
            var existingBook = await _context.Books.Include(b => b.Author).FirstOrDefaultAsync(b => b.Id == id);

            if(existingBook == null) return null;

            existingBook.BookName = bookDto.BookName;
            existingBook.PublicationDate = bookDto.PublicationDate;
            existingBook.ISBN = bookDto.ISBN;
            existingBook.Status = bookDto.Status;
            existingBook.Author.FirstName = bookDto.AuthorFirstName;
            existingBook.Author.LastName = bookDto.AuthorLastName;
            existingBook.Author.BirthOfDate = bookDto.AuthorBirthOfDate;

            await _context.SaveChangesAsync();

            return existingBook;
        }
    }
}