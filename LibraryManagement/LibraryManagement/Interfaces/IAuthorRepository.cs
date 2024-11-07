using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryManagement.DTOs;
using LibraryManagement.Models;

namespace LibraryManagement.Interfaces
{
    public interface IAuthorRepository
    {
        Task<List<Author>> GetAllAuthorsAsync();
        Task<Author?> GetByIdAsync(int id);
        Task<Author> CreateAuthorAsync(Author authorModel);
        Task<Author?> UpdateAuthorAsync(int id, UpdateAuthorRequestDto authorDto);
        Task<Author?> DeleteAuthorAsync(int id);
        Task<bool> AuthorExists(int id);
    }
}