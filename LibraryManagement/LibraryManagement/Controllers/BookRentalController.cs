using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryManagement.Extensions;
using LibraryManagement.Interfaces;
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
        private readonly IAuthorRepository _authorRepo;
        private readonly IBookRentalRepository _bookRentalRepo;

        public BookRentalController(UserManager<Member> userManager,
        IAuthorRepository authorRepo, IBookRentalRepository bookRentalRepo)
        {
            _userManager = userManager;
            _authorRepo = authorRepo;
            _bookRentalRepo = bookRentalRepo;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetMemberPortfolio()
        {   
            // user is being inheritated from controller base
            var username = User.GetUsername();
            var member = await _userManager.FindByNameAsync(username);
            var memberPortfolio = await _bookRentalRepo.GetMemberPortfolio(member);
            return Ok(memberPortfolio);
        }
    }
}