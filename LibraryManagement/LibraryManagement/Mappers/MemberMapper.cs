using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryManagement.DTOs;
using LibraryManagement.Models;

namespace LibraryManagement.Mappers
{
    public static class MemberMapper
    {
        public static MemberDto ToMemberDto(this Member memberModel)
        {
            return new MemberDto
            {
                FirstName = memberModel.FirstName,
                LastName = memberModel.LastName,
                TotalRentalsAllTime = memberModel.TotalRentalsAllTime,
                TotalRentalsNow = memberModel.TotalRentalsNow
            };
        }
    }
}