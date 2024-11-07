using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryManagement.DTOs;
using LibraryManagement.Models;

namespace LibraryManagement.Mappers
{
    public static class AuthorMapper
    {
        public static AuthorDto ToAuthorDto(this Author authorModel)
        {
            return new AuthorDto
            {
                Id = authorModel.Id,
                FirstName = authorModel.FirstName,
                LastName = authorModel.LastName,
                BirthOfDate = authorModel.BirthOfDate,
                Books = authorModel.Books?.Select(b => b.ToBookDto()).ToList()
            };
        }

        public static Author ToAuthorFromCreateDto(this CreateAuthorRequestDto authorDto)
        {
            return new Author
            {
                FirstName = authorDto.FirstName,
                LastName = authorDto.LastName,
                BirthOfDate = authorDto.BirthOfDate
            };
        }
    }
}