using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestWebApp.Dtos.Auth;
using TestWebApp.Interfaces;
using TestWebApp.Models;

namespace TestWebApp.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<appUser> _userManager;
        private readonly ITokenService _tokenService;
        private readonly SignInManager<appUser> _signInManager;
        public AuthController(UserManager<appUser> userManager, ITokenService tokenService, SignInManager<appUser> signInManager)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _signInManager = signInManager;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto register)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var user = new appUser
                {
                    UserName = register.Username,
                    Email = register.Email
                };

                var result = await _userManager.CreateAsync(user, register.Password);

                if (result.Succeeded)
                {
                    var rolesResult = await _userManager.AddToRoleAsync(user, "User");

                    if (rolesResult.Succeeded)
                    {
                        return Ok(
                            new NewUserDto
                            {

                                Status = true,
                                Message = "User registered successfully",
                                Username = user.UserName,
                                Email = user.Email,
                                Token = _tokenService.CreateToken(user)

                            });
                    }
                    else
                    {
                        return StatusCode(500, rolesResult.Errors);
                    }

                }
                else
                {
                    return BadRequest(result.Errors);
                }

            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "An error occurred while registering the user",
                    error = ex.Message
                });
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto login)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var user = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == login.UserName.ToLower());
                if (user == null)
                {
                    return Unauthorized(new { message = "Invalid username or password" });
                }
                var passwordValid = await _signInManager.CheckPasswordSignInAsync(user, login.Password, false);
                if (!passwordValid.Succeeded)
                {
                    return Unauthorized(new { message = "Invalid username or password" });
                }
                return Ok(
                    new NewUserDto
                    {
                        Status = true,
                        Message = "Login successful",
                        Username = user.UserName,
                        Email = user.Email,
                        Token = _tokenService.CreateToken(user)
                    });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "An error occurred while logging in",
                    error = ex.Message
                });
            }
        }
    }
      
}
