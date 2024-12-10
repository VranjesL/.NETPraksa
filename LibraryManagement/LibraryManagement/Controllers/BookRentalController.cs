using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryManagement.DTOs;
using LibraryManagement.Extensions;
using LibraryManagement.Interfaces;
using LibraryManagement.Mappers;
using LibraryManagement.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.Controllers
{   
    [Route("LibraryManagement/bookRental")]
    [ApiController]
    public class BookRentalController : ControllerBase
    {   
        private readonly UserManager<Member> _userManager;
        private readonly IBookRepository _bookRepo;
        private readonly IBookRentalRepository _bookRentalRepo;

        public BookRentalController(UserManager<Member> userManager,
        IBookRepository bookRepo, IBookRentalRepository bookRentalRepo)
        {
            _userManager = userManager;
            _bookRepo = bookRepo;
            _bookRentalRepo = bookRentalRepo;
        }

        [HttpPost]
        [Authorize]
        // this function needs refactoring
        public async Task<IActionResult> BorrowBook(string bookName)
        {
            var username = User.Identity?.Name;
            if(string.IsNullOrEmpty(username)) return BadRequest("Username not found!");
            
            var member = await _bookRentalRepo.FindMemberByUsername(username);
            if(member == null) return BadRequest("Member not found!");

            var book = await _bookRepo.GetBookByName(bookName);
            if(book == null) return BadRequest("Book not found!");

            if(book.Status != "Available") return BadRequest("Book is already rented!");

            var memberPortfolio = await _bookRentalRepo.GetMemberPortfolio(member);
            if(memberPortfolio.Count >= 5) return BadRequest("You cannot borrow more than 5 books at a time!");

            if(memberPortfolio.Any(e => e.BookName.ToLower() == bookName.ToLower())) return BadRequest("Cannot borrow the same book");

            var bookRentalModel = new BookRental
            {
                BookId = book.Id,
                MemberId = member.Id,
                RentalDate = DateTime.UtcNow
            };

            if (book.Author == null)
            {
                return StatusCode(500, "Author information is missing for this book.");
            }

            var updateBookDto = new UpdateBookRequestDto
            {
                BookName = book.BookName,
                PublicationDate = book.PublicationDate,
                ISBN = book.ISBN,
                Status = "Rented",
                AuthorFirstName = book.Author.FirstName,
                AuthorLastName = book.Author.LastName,
                AuthorBirthOfDate = book.Author.BirthOfDate,
                TimesRented = book.TimesRented++ 
            };

            bookRentalModel.ReturnDate = null;

            await _bookRepo.UpdateBookAsync(book.Id, updateBookDto);
            await _bookRentalRepo.CreateAsync(bookRentalModel);
            
            if(bookRentalModel == null)
            {
                return StatusCode(500, "Could not create");
            }
            else
            {
                return Created();
            }
        }

        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> ReturnBook(string bookName)
        {   
            var username = User.Identity?.Name;

            if(string.IsNullOrEmpty(username)) return BadRequest("Username not found!");
            
            var member = await _bookRentalRepo.FindMemberByUsername(username);

            if(member == null) return BadRequest("Member not found!");

            // getting portfolio of member that is currently logged in
            var memberPortfolio = await _bookRentalRepo.GetMemberPortfolio(member);

            // finding exact book that member wants to return
            var filteredBook = memberPortfolio.Where(b => b.BookName.ToLower() == bookName.ToLower()).ToList();
            if(filteredBook == null) return BadRequest("This book is not in your possesion!");

            // finding book rental record
            var bookRental = await _bookRentalRepo.GetBookRentalByBookName(member, bookName);
            if(bookRental == null) return BadRequest("Rental record not found!");

            if(bookRental.RentalDate.Value.AddDays(15) < DateTime.UtcNow) return BadRequest("The book is overdue and should be returned!");
            
            var book = await _bookRepo.GetBookByName(bookName);
            
            var updateBookDto = new UpdateBookRequestDto
            {
                BookName = book.BookName,
                PublicationDate = book.PublicationDate,
                ISBN = book.ISBN,
                Status = "Available",
                AuthorFirstName = book.Author.FirstName,
                AuthorLastName = book.Author.LastName,
                AuthorBirthOfDate = book.Author.BirthOfDate
            };

            await _bookRepo.UpdateBookAsync(book.Id, updateBookDto);
            await _bookRentalRepo.ReturnBook(member, bookName);
        
            return Ok("Book returned successfully");
        }
    }
}