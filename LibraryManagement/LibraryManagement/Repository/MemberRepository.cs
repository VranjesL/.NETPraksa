using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryManagement.Data;
using LibraryManagement.DTOs;
using LibraryManagement.Interfaces;
using LibraryManagement.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Repository
{
    public class MemberRepository : IMemberRepository
    {   
        private readonly ApplicationDBContext _context;

        public MemberRepository(ApplicationDBContext context)
        {
            _context = context;
        }
        public async Task<List<MemberDto>> TopBorrowersAllTimeAsync(int top)
        {
            return await _context.Members.Select(m => new MemberDto
            {
                FirstName = m.FirstName,
                LastName = m.LastName,
                TotalRentals = m.TotalRentals,
                Username = m.UserName
            }).OrderByDescending(x => x.TotalRentals).Take(top).ToListAsync();
        }

        public Task<List<Member>> TopBorrowersRightNowAsync(int top)
        {
            throw new NotImplementedException();
        }
    }
}