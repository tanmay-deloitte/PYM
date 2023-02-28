using Microsoft.AspNetCore.Mvc;
using PYM.Models;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.Extensions.Configuration;
using PYM;
using PYM.models;
using Microsoft.EntityFrameworkCore;

namespace CourseAPI.Controllers;

    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        private IConfiguration _config;  
        private PYMContext _context;

        public LoginController(IConfiguration _config,PYMContext context)
        {
            _config= _config;
            _context= context;
        }
        [HttpPost, Route("login")]
        public IActionResult Login(loginDTO loginDTO)
        {
            try
            {
                User user = _context.User.Include(s=>s.Roles).SingleOrDefault(user=>user.UserName==loginDTO.UserName);
                if (string.IsNullOrEmpty(loginDTO.UserName) ||
                string.IsNullOrEmpty(loginDTO.Password))
                    return BadRequest("Username and/or Password not specified");
                if (loginDTO.UserName.Equals(user.UserName) &&
                loginDTO.Password.Equals(user.Password))
                {
                    List<Claim> claimList = new List<Claim>();
                    foreach (var item in user.Roles)
                    {
                        claimList.Add(new Claim("roles", item.RoleName));
                    }
                    var secretKey = new SymmetricSecurityKey
                    (Encoding.UTF8.GetBytes("Thisismysecretkey"));
                    var signinCredentials = new SigningCredentials
                   (secretKey, SecurityAlgorithms.HmacSha256);
                    var jwtSecurityToken = new JwtSecurityToken(
                        "https://localhost:7124",  
                        "https://localhost:7124", 
                        claims:claimList,
                        expires: DateTime.Now.AddMinutes(10),
                        signingCredentials: signinCredentials
                    );
                    return Ok(new JwtSecurityTokenHandler().
                    WriteToken(jwtSecurityToken));
                }
            }
            catch
            {
                return BadRequest
                ("An error occurred in generating the token");
            }
            return Unauthorized();
        }
    }