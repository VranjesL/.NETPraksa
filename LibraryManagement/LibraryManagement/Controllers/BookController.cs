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
        private readonly ApplicationDBContext _context;
        private readonly IBookRepository _bookRepo;
        public BookController(ApplicationDBContext context, IBookRepository bookRepo)
        {
            _context = context;
            _bookRepo = bookRepo;
        }

        [HttpGet]
        //[Authorize]
        public async Task<IActionResult> GetAll()
        {
            var books = await _bookRepo.GetAllBooksAsync();
            
            var bookDto = books.Select(s => s.ToBookDto()).ToList();

            return Ok(bookDto);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {   
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var book = await _bookRepo.GetBookByIdAsync(id);

            if(book ==  null) return NotFound();

            return Ok(book.ToBookDto());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateBookRequestDto bookDto)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var bookModel = bookDto.ToBookFromCreateDto();
            
            await _bookRepo.CreateBookAsync(bookModel);

            return CreatedAtAction(nameof(GetById), new { id = bookModel.Id, authorId = bookModel.AuthorId }, bookModel.ToBookDto());
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            
            var bookModel = await _bookRepo.DeleteBookAsync(id);

            if(bookModel == null) return NotFound();

            return NoContent();
        }
    }
}