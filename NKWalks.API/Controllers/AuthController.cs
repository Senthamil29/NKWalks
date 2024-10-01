using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NKWalks.API.Models.DTO;
using NKWalks.API.Repository;

namespace NKWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly ITokenRepository tokenRepository;

        public AuthController(UserManager<IdentityUser> userManager,ITokenRepository tokenRepository)
        {
            this.userManager = userManager;
            this.tokenRepository = tokenRepository;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDTO registerRequestDTO)
        {
            var identityUser = new IdentityUser
            {
                UserName = registerRequestDTO.Username,
                Email = registerRequestDTO.Username
            };

            var identityResult = await userManager.CreateAsync(identityUser, registerRequestDTO.Password);
            if (identityResult.Succeeded) 
            {
                if (registerRequestDTO.Roles.Any() &&  registerRequestDTO.Roles != null)
                {
                    identityResult = await userManager.AddToRolesAsync(identityUser, registerRequestDTO.Roles);

                    if (identityResult.Succeeded)
                    {
                        return Ok("User Register Successfully!");
                    }
                }
            }
            return BadRequest("Something Went Wrong");
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO loginRequestDTO)
        {
            var userCheck = await userManager.FindByEmailAsync(loginRequestDTO.Username);
            if (userCheck != null) 
            {
                var checkPassword = await userManager.CheckPasswordAsync(userCheck,loginRequestDTO.Password);

                if (checkPassword)
                {
                    var roles = await userManager.GetRolesAsync(userCheck);

                    if (roles != null)
                    {
                        var jwtToken = tokenRepository.CreateJWTToken(userCheck, roles.ToList());

                        var response = new LoginResponseDTO
                        {
                            JwtToken = jwtToken,
                        };

                        return Ok(response);
                    }
                }
            }
            return BadRequest("Username or Password is incorrect");
        }
    }
}
