using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using LibraryManagement.Interfaces;
using LibraryManagement.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace LibraryManagement.Service
{
    public class TokenService : ITokenService
    {   
        // using iconfiguration so we can pull stuff from appsetting.json
        private readonly UserManager<Member> _userManager;
        private readonly IConfiguration _config;
        // _key created from SigningKey value from appsetting.json
        private readonly SymmetricSecurityKey _key;
        public TokenService(IConfiguration config, UserManager<Member> userManager)
        {   
            _userManager = userManager;
            _config = config;

            //we have to turn it into bytes, not gonna accept regular string
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:SigningKey"]));
        }

        // method that creates JWT for authentication
        public async Task<string> CreateToken(Member member)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.UniqueName, member.UserName),
                new Claim(JwtRegisteredClaimNames.GivenName, member.FirstName),
                new Claim(JwtRegisteredClaimNames.FamilyName, member.LastName)
            };

            var roles = await _userManager.GetRolesAsync(member);
            foreach(var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            // creating signing credentials, this part guarantees authenticity of token
            var credentials = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);
            
            // this is where we give description to our token
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(7),
                SigningCredentials = credentials,
                Issuer = _config["JWT:Issuer"],
                Audience = _config["JWT:Audience"]
            };

            // this is where we actually create the token
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}