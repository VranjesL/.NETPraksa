using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryManagement.Data;
using LibraryManagement.DTOs;
using LibraryManagement.Interfaces;
using LibraryManagement.Mappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.Controllers
{   
    [Route("LibraryManagement/book")]
    [ApiController]
    public class BookController : ControllerBase
    {   
        private readonly IBookRepository _bookRepo;
        private readonly IAuthorRepository _authorRepo;
        public BookController(IBookRepository bookRepo, IAuthorRepository authorRepo)
        {
            _bookRepo = bookRepo;
            _authorRepo = authorRepo;
        }

        [HttpGet]
        [Authorize(Policy = "AdminOrUser")]
        public async Task<IActionResult> GetAll()
        {   
            if(!ModelState.IsValid) return BadRequest(ModelState);

            var books = await _bookRepo.GetAllBooksAsync();
            
            var bookDto = books.Select(s => s.ToBookDto()).ToList();

            return Ok(bookDto);
        }

        [HttpGet("{id:int}")]
        [Authorize(Policy = "AdminOrUser")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {   
            if(!ModelState.IsValid) return BadRequest(ModelState);

            var book = await _bookRepo.GetBookByIdAsync(id);

            if(book ==  null) return NotFound();

            return Ok(book.ToBookDto());
        }

        [HttpPost("{authorId:int}")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> Create([FromRoute] int authorId, [FromBody] CreateBookRequestDto bookDto)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);

            if(!await _authorRepo.AuthorExists(authorId)) return BadRequest("Author does not exist");

            var bookModel = bookDto.ToBookFromCreateDto(authorId);
            
            await _bookRepo.CreateBookAsync(bookModel);

            return CreatedAtAction(nameof(GetById), new { id = bookModel.Id }, bookModel.ToBookDto());
        }

        [HttpDelete]
        [Route("{id:int}")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);
            
            var bookModel = await _bookRepo.DeleteBookAsync(id);

            if(bookModel == null) return NotFound();

            return NoContent();
        }

        [HttpPut]
        [Route("{id:int}")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateBookRequestDto bookDto)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);

            var bookModel = await _bookRepo.UpdateBookAsync(id, bookDto);

            if(bookModel == null) return NotFound();

            return Ok(bookModel.ToBookDto());
        }

        [HttpGet("Most-Rented")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> GetMostRentedBooks([FromQuery] int noBooksToShow = 5)
        {   
            if(!ModelState.IsValid) return BadRequest(ModelState);

            var mostRentedBooks = await _bookRepo.GetMostRentedBooksAsync(noBooksToShow);

            return Ok(mostRentedBooks);
        }
    }
}