using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryManagement.DTOs;
using LibraryManagement.Interfaces;
using LibraryManagement.Mappers;
using LibraryManagement.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Controllers
{   
    [Route("LibraryManagement/member")]
    [ApiController]
    public class MemberController : ControllerBase
    {   
        private readonly UserManager<Member> _memberManager;
        private readonly ITokenService _tokenService;
        private readonly SignInManager<Member> _signInManager;
        private readonly IMemberRepository _memberRepo;
        public MemberController(UserManager<Member> memberManager, ITokenService tokenService, SignInManager<Member> signInManager, IMemberRepository memberRepo)
        {
            _memberManager = memberManager;
            _tokenService = tokenService;
            _signInManager = signInManager;
            _memberRepo = memberRepo;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);

            var member = await _memberManager.Users.FirstOrDefaultAsync(m => m.UserName == loginDto.Username.ToLower());

            if(member == null) return Unauthorized("Invalid username!");

            var result = await _signInManager.CheckPasswordSignInAsync(member, loginDto.Password, false);

            if(!result.Succeeded) return Unauthorized("Username not found and/or password incorrect!");

            return Ok(
                new NewMemberDto
                {
                    UserName = member.UserName,
                    FirstName = member.FirstName,
                    LastName = member.LastName,
                    Token = await _tokenService.CreateToken(member)
                }
            );
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            try
            {
                if(!ModelState.IsValid) return BadRequest(ModelState);

                var member = new Member
                {
                    UserName = registerDto.Username,
                    FirstName = registerDto.FirstName,
                    LastName = registerDto.LastName,
                    //Password = registerDto.Password
                };

                // createAsync returns an object that is going to have properties that allows to check
                var createdMember = await _memberManager.CreateAsync(member, registerDto.Password);

                if(createdMember.Succeeded)
                {
                    // similar to createasync but its for roles
                    var roleResult = await _memberManager.AddToRoleAsync(member, "User");
                    if(roleResult.Succeeded)
                    {
                        return Ok(
                            new NewMemberDto
                            {
                                UserName = member.UserName,
                                FirstName = member.FirstName,
                                LastName = member.LastName,
                                Token = await _tokenService.CreateToken(member)
                            }
                        );
                    }
                        
                    else
                        return StatusCode(500, roleResult.Errors);
                }
                else
                    return StatusCode(500, createdMember.Errors);

            } catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }

        [HttpGet("Members with most books borrowed throughout history")]
        public async Task<IActionResult> GetTopBorrowers([FromQuery] int top = 5, [FromQuery] bool showAllTime = true)
        {
            var topBorrowers = await _memberRepo.GetTopBorrowersAsync(top, showAllTime);

            if (topBorrowers == null || !topBorrowers.Any()) return NotFound("No borrowers found.");

            return Ok(topBorrowers);
        }

    }
}