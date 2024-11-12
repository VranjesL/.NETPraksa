using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using LibraryManagement.Interfaces;
using LibraryManagement.Models;
using Microsoft.IdentityModel.Tokens;

namespace LibraryManagement.Service
{
    public class TokenService : ITokenService
    {   
        // using iconfiguration so we can pull stuff from appsetting.json
        private readonly IConfiguration _config;
        private readonly SymmetricSecurityKey _key;
        public TokenService(IConfiguration config)
        {
            _config = config;

            //we have to turn it into bytes, not gonna accept regulat string
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:SigningKey"]));
        }
        public string CreateToken(Member member)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.UniqueName, member.UserName),
                new Claim(JwtRegisteredClaimNames.GivenName, member.FirstName),
                new Claim(JwtRegisteredClaimNames.FamilyName, member.LastName)
                //new Claim(JwtRegisteredClaimNames.Birthdate, member.DateOfBirth),
            };

            // creating signing credentials
            var credentials = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);
            
            // this is where we actually create the token
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(7),
                SigningCredentials = credentials,
                Issuer = _config["JWT:Issuer"],
                Audience = _config["JWT:Audience"]
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}