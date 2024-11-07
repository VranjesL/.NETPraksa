using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryManagement.DTOs;
using LibraryManagement.Interfaces;
using LibraryManagement.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.Controllers
{
    [Route("LibraryManagement/author")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorRepository _authorRepo;

        public AuthorController(IAuthorRepository authorRepo)
        {
            _authorRepo = authorRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var authors = await _authorRepo.GetAllAuthorsAsync();

            var authorDto = authors.Select(a => a.ToAuthorDto());

            return Ok(authorDto);
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var author = await _authorRepo.GetByIdAsync(id);

            if (author == null) return NotFound();

            return Ok(author.ToAuthorDto());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateAuthorRequestDto authorDto)
        {
            var author = authorDto.ToAuthorFromCreateDto();
            await _authorRepo.CreateAuthorAsync(author);

            return CreatedAtAction(nameof(GetById), new { id = author.Id }, author.ToAuthorDto());
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);
            
            var authorModel = await _authorRepo.DeleteAuthorAsync(id);

            if(authorModel == null) return NotFound();

            return NoContent();
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, UpdateAuthorRequestDto authorDto)
        {
            var author = await _authorRepo.UpdateAuthorAsync(id, authorDto);

            if(author == null) return NotFound();

            return Ok(author.ToAuthorDto());
        }
    }
}