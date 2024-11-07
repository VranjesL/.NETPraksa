using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryManagement.DTOs;
using LibraryManagement.Models;

namespace LibraryManagement.Interfaces
{
    public interface IBookRepository
    {
        Task<List<Book>> GetAllBooksAsync();
        Task<Book?> GetBookByIdAsync(int id);
        Task<Book> CreateBookAsync(Book bookModel);
        Task<Book> UpdateBookAsync(int id, UpdateBookRequestDto bookDto);
        Task<Book?> DeleteBookAsync(int id);
        Task<bool> BookExists(int id);
    }
}