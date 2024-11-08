using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryManagement.DTOs;
using LibraryManagement.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.Controllers
{   
    [Route("LibraryManagement/member")]
    [ApiController]
    public class MemberController : ControllerBase
    {   
        private readonly UserManager<Member> _memberManager;
        public MemberController(UserManager<Member> memberManager)
        {
            _memberManager = memberManager;
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
                        return Ok("Member created!");
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
    }
}