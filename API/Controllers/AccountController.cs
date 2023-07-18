using System.Security.Cryptography;
using System.Text;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class AccountController : BaseAPIController
    {
        private readonly DataContext _context;

        private readonly ITokenService TokenService;

        public AccountController(DataContext context, ITokenService tokenService)
        {
            _context = context;
            TokenService = tokenService;
        }

        [HttpPost("register")] // url: api/account/register
        public async Task<ActionResult<UserDTO>> Register(RegisterAccountDTO regis_acc) {
            if (await AccountExist(regis_acc.UserName)) return BadRequest("User is existed");

            using var hmac = new HMACSHA512();
            var user = new AppUser {
                UserName = regis_acc.UserName,
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(regis_acc.Password)),
                PasswordSalt = hmac.Key
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return new UserDTO {
                Username = user.UserName,
                Token = TokenService.CreateToken(user)
            };
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDTO>> Login(LoginDTO login_acc) {
            var user = await _context.Users.SingleOrDefaultAsync(x => x.UserName == login_acc.UserName);

            if(user == null) {
                return Unauthorized("Invalid Username");
            }

            using var hmac = new HMACSHA512(user.PasswordSalt);

            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(login_acc.Password));

            for (int i = 0; i < computedHash.Length; i++) {
                if(computedHash[i] != user.PasswordHash[i]) {
                    return Unauthorized("Invalid Password");
                }
            }
            return new UserDTO {
                Username = user.UserName,
                Token = TokenService.CreateToken(user)
            };
        }

        private async Task<bool> AccountExist(string username){
            return await _context.Users.AnyAsync(x => x.UserName == username);
        }
    }
}