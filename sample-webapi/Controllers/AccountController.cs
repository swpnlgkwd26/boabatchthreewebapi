using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using sample_webapi.Models;
using sample_webapi.ViewModels;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace sample_webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountController(SignInManager<ApplicationUser> signInManager)
        {
             _signInManager = signInManager;
        }
        // Login that validate user and return the token
        //public async Task<bool> UserExists(LoginViewModel loginViewModel)
        //{
        //    var result = await _signInManager.PasswordSignInAsync(loginViewModel.Email,
        //        loginViewModel.Password, false, false);
        //    return result.Succeeded ==true? true:false;
        //}

        [HttpPost,Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginViewModel user)
        {
            if (user == null)
            {
                return BadRequest();
            }
            var result = await _signInManager.PasswordSignInAsync(user.Email,
               user.Password, false, false);
        //   
            if (result.Succeeded)
            {
                // Generate Token and Return
                // Key
                var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("MySecuretKey@845"));
                var signInCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
                // Claims :  Payload
                var claims = new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub,"ProductAPIAccessToken"),
                    new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Iat,DateTime.Now.ToString() ),
                    new Claim("Email",user.Email)
                };

                var tokenOptions = new JwtSecurityToken(
                    issuer: "http://localhost:5001",
                    audience: "http://localhost:5001",
                    claims: claims,
                    expires :  DateTime.Now.AddSeconds(180),
                    signingCredentials: signInCredentials
                    );

                var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
                return Ok(new { token = tokenString });
            }
            else
            {
                return Unauthorized();
            }
        }


    }
}
