using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TestWebApp.Dtos.Auth;
using TestWebApp.Models;

namespace TestWebApp.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        public AuthController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto register) {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var user = new AppUser
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
                            new {
                            status = true,
                            message = "User registered successfully"
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
