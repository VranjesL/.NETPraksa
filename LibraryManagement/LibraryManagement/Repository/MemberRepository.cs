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
        public async Task<List<MemberDto>> GetTopBorrowersAsync(int top, bool showAllTime)
        {   
            var topBorrowers = _context.Members.Select(m => new MemberDto
                {
                    FirstName = m.FirstName,
                    LastName = m.LastName,
                    TotalRentalsAllTime = m.TotalRentalsAllTime,
                    TotalRentalsNow = m.TotalRentalsNow,
                    Username = m.UserName
                });

            if(showAllTime)
            {
               return await topBorrowers.OrderByDescending(x => x.TotalRentalsAllTime).Take(top).ToListAsync();
            }
            else
            {
                return await topBorrowers.OrderByDescending(x => x.TotalRentalsNow).Take(top).ToListAsync();
            }
        }
    }
}