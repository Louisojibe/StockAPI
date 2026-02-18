using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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
        public AuthController(UserManager<appUser> userManager, ITokenService tokenService)
        {
            _userManager = userManager;
            _tokenService = tokenService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto register) {
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
                            new NewUserDto {

                                Status = true,
                                Message = "User registered successfully",
                                Email = user.Email,
                                Token = _tokenService.CreateToken(user)

                            });
                    } else
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
                return StatusCode(500, new {
                    message = "An error occurred while registering the user",
                    error = ex.Message
                });
            }
        }
    }
}
