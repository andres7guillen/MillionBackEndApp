using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MillionApp.Api.Middleware;
using MillionApp.Api.Models.Security;
using MillionApp.Domain.Exceptions;

namespace MillionApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

    public class UserController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IConfiguration _configuration;

        public UserController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }

        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CustomResponse<AuthenticationResponseModel>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(CustomResponse<object>))]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromForm] UserCredentialsModel userCredentials)
        {
            try
            {
                string username = userCredentials.Email.Substring(0, userCredentials.Email.IndexOf('@'));

                var usuario = new IdentityUser
                {
                    Email = userCredentials.Email,
                    UserName = username,
                    EmailConfirmed = true
                };

                var userCreated = await _userManager.CreateAsync(usuario, userCredentials.Password);
                if (userCreated.Succeeded)
                {
                    await _userManager.AddClaimAsync(usuario, new Claim(ClaimTypes.NameIdentifier, usuario.UserName));
                    return Ok(BuildToken(userCredentials));
                }
                else
                {
                    return BadRequest(userCreated.Errors);
                }
            }
            catch (Exception e)
            {
                throw new BusinessContextException(BusinessContextExceptionEnum.ErrorRegisteringUser);
            }
        }

        [HttpPost("Login")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CustomResponse<AuthenticationResponseModel>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(CustomResponse<object>))]
        public async Task<IActionResult> Login([FromBody] UserCredentialsModel userCredentials)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(userCredentials.Email);

                if (user != null)
                {
                    var isPasswordValid = await _userManager.CheckPasswordAsync(user, userCredentials.Password);
                    if (isPasswordValid)
                    {
                        return Ok(BuildToken(userCredentials: userCredentials));
                    }
                    else
                    {
                        var (code, message) = BusinessContextException.Detail(BusinessContextExceptionEnum.PasswordOrUserInvalid);
                        var errorResponse = new ErrorResponse(code, message);
                        return StatusCode(500, CustomResponse<ErrorResponse>.BuildError(500, errorResponse.Message));
                    }
                }
                else
                {
                    var (code, message) = BusinessContextException.Detail(BusinessContextExceptionEnum.PasswordOrUserInvalid);
                    var errorResponse = new ErrorResponse(code, message);
                    return StatusCode(500, CustomResponse<ErrorResponse>.BuildError(500, errorResponse.Message));
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        private CustomResponse<AuthenticationResponseModel> BuildToken(UserCredentialsModel userCredentials)
        {
            var claims = new List<Claim>()
            {
                new Claim("role", userCredentials.Email)
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["jwtKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expirationTime = DateTime.UtcNow.AddMinutes(10);

            var securityToken = new JwtSecurityToken(issuer: null, audience: null, claims: claims,
                expires: expirationTime, signingCredentials: creds);
            var auth = new AuthenticationResponseModel()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(securityToken),
                Expiration = expirationTime,
                EmailUser = userCredentials.Email
            };
            return CustomResponse<AuthenticationResponseModel>
                 .BuildSuccess(auth);
        }
    }
}

