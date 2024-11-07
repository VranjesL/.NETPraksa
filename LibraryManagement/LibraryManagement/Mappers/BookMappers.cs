using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryManagement.DTOs;
using LibraryManagement.Models;

namespace LibraryManagement.Mappers
{   
    // extension method so we declare it static
    public static class BookMappers
    {
        public static BookDto ToBookDto(this Book bookModel)
        {
            return new BookDto
            {
                Id = bookModel.Id,
                BookName = bookModel.BookName,
                PublicationDate = bookModel.PublicationDate,
                ISBN = bookModel.ISBN,
                Status = bookModel.Status,
                AuthorFirstName = bookModel.Author?.FirstName,
                AuthorLastName = bookModel.Author?.LastName
            };
        }

        public static Book ToBookFromCreateDto(this CreateBookRequestDto bookDto)
        {
            return new Book
            {
                BookName = bookDto.BookName,
                PublicationDate = bookDto.PublicationDate,
                ISBN = bookDto.ISBN,
                Status = bookDto.Status,
                Author = new Author
                {
                    FirstName = bookDto.AuthorFirstName,
                    LastName = bookDto.AuthorLastName
                }
                //AuthorId = bookDto.AuthorId
            };
        }

        /*public static Book ToBookFromUpdateDto(this UpdateBookRequestDto bookDto)
        {
            return new Book
            {
                BookName = bookDto.BookName,
                PublicationDate = bookDto.PublicationDate,
                ISBN = bookDto.ISBN,
                Status = bookDto.Status,
            };
        }*/
    }
}