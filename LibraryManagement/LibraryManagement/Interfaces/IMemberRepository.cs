using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryManagement.DTOs;
using LibraryManagement.Models;

namespace LibraryManagement.Interfaces
{
    public interface IMemberRepository
    {
        Task<List<MemberDto>> GetTopBorrowersAsync(int top, bool showAllTime);
    }
}