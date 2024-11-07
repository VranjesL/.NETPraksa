using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryManagement.Data;
using LibraryManagement.DTOs;
using LibraryManagement.Interfaces;
using LibraryManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Repository
{
    public class AuthorRepository : IAuthorRepository
    {   
        private readonly ApplicationDBContext _context;
        public AuthorRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<bool> AuthorExists(int id)
        {
            return await _context.Authors.AnyAsync(a => a.Id == id);
        }

        public async Task<Author> CreateAuthorAsync(Author authorDto)
        {
            await _context.Authors.AddAsync(authorDto);
            await _context.SaveChangesAsync();

            return authorDto;
        }

        public async Task<Author?> DeleteAuthorAsync(int id)
        {
            var author = await _context.Authors.FirstOrDefaultAsync(a => a.Id == id);

            if(author == null) return null;

            _context.Remove(author);
            await _context.SaveChangesAsync();
            return author;
        }

        public async Task<List<Author>> GetAllAuthorsAsync()
        {
            return await _context.Authors.Include(a => a.Books).ToListAsync();
        }

        public async Task<Author?> GetByIdAsync(int id)
        {
            return await _context.Authors.Include(a => a.Books).FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<Author?> UpdateAuthorAsync(int id, UpdateAuthorRequestDto authorDto)
        {
            var author = await _context.Authors.FirstOrDefaultAsync(a => a.Id == id);

            if(author == null) return null;

            author.FirstName = authorDto.FirstName;
            author.LastName = authorDto.LastName;
            author.BirthOfDate = authorDto.BirthOfDate;

            await _context.SaveChangesAsync();

            return author;
        }
    }
}