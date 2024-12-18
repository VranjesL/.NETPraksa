using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryManagement.Models;

namespace LibraryManagement.Interfaces
{
    public interface ITokenService
    {
        Task<string> CreateToken(Member member);
    }
}