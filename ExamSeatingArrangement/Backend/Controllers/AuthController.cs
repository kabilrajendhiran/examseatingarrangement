using AutoMapper;
using ExamSeatingArrangement2020.Dtos;
using ExamSeatingArrangement2020.Models;
using ExamSeatingArrangement2020.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ExamSeatingArrangement2020.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _auth;
        private readonly IMapper _authMapper;
        private readonly IConfiguration _config;

        public AuthController(IAuthRepository auth, IMapper authMapper, IConfiguration config)
        {
            _auth = auth;
            _authMapper = authMapper;
            _config = config;
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromForm] UserForRegisterDto user)
        {
            if (await _auth.UserExists(user.UserName))
            {
                return BadRequest(new { res = "Username Already Exists!" });
            }

            var userToRegister = _authMapper.Map<User>(user);

            string filepath = await _auth.StoreProfilePicPath(user.ProfilePicture);

            var createdUser = await _auth.Register(userToRegister, user.Password, filepath);

            var userToReturn = _authMapper.Map<UserToReturnDto>(createdUser);

            return Ok(userToReturn);
        }

        [Authorize]
        [HttpPost("reset")]
        public async Task<IActionResult> ResetPasswordAsync([FromForm] UserParmsDto userParms)
        {
            try
            {
                await _auth.ResetPassword(userParms.UserName, userParms.Password, true);
                return Ok(new { res = "Password reset success" });
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return BadRequest(new { res = "Error resetting password" });
            }
        }

        [AllowAnonymous]
        [HttpPost("forget")]
        public async Task<IActionResult> ForgetPasswordAsync([FromForm] UserParmsDto userParms)
        {
            try
            {
                if (!await _auth.UserExists(userParms.UserName))
                {
                    return BadRequest(new { res = "Username not exists" });
                }

                var result = await _auth.ResetPassword(userParms.UserName, userParms.Password, false);
                if (result)
                {
                    return Ok(new { res = "Password reset success" });
                }
                return BadRequest(new { res = "Error resetting password" });
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return BadRequest(new { res = "Error resetting password" });
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromForm] UserParmsDto userParms)
        {
            try
            {
                var User = await _auth.Login(userParms.UserName, userParms.Password);

                if (User == null)
                    return Unauthorized(new { res = "Username or password is incorrect" });

                if (User.IsAuthorized == false)
                    return Unauthorized(new { res = "You are not authorized by admin" });

                var file = _auth.FileFactory(User);

                var fileobj = File(file, "image/png", "profilePic.png");

                var userToReturn = new ReturnFullDetailsOfUser()
                {
                    Id = User.Id,
                    UserName = User.UserName,
                    ProfilePic = fileobj,
                    Token = GenerateJwtToken(User).Result,
                    Role = User.Role
                };

                return Ok(userToReturn);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Unauthorized();
            }
        }

        private async Task<string> GenerateJwtToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Role,user.Role)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8
                .GetBytes(_config.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        [Authorize]
        [HttpGet("users")]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _auth.Users();
            List<ReturnFullDetailsOfUser> fullDetailsOfUsers = new List<ReturnFullDetailsOfUser>();

            foreach (var user in users)
            {
                var file = _auth.FileFactory(user);
                var fileobj = File(file, "image/png", "profilePic.png");

                var userToReturn = new ReturnFullDetailsOfUser()
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    ProfilePic = fileobj,
                    Role = user.Role,
                    IsAuthorized = user.IsAuthorized
                };

                fullDetailsOfUsers.Add(userToReturn);
            }

            return Ok(fullDetailsOfUsers);
        }

        [Authorize]
        [HttpPut("user")]
        public async Task<IActionResult> UpdateUser([FromBody] UserForUpdateDto user)
        {
            var result = await _auth.Updateuser(user);
            if (result)
            {
                return Ok(new { res = "User updated successfully!!!" });
            }
            return BadRequest(new { res = "Update user failure!" });
        }

        [Authorize]
        [HttpDelete("user/{id}")]
        public async Task<IActionResult> DeleteUserAsync(int id)
        {
            var res = await _auth.DeleteUser(id);
            if (res)
            {
                return Ok(new { res = "Successfully deleted!" });
            }
            return BadRequest(new { res = "Deleting user failure" });
        }
    }
}