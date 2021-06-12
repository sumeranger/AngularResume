using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using WebAPI.Dtos;
using WebAPI.interfaces;
using WebAPI.Models;
using WebAPI.Errors;
using System;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Configuration;

namespace WebAPI.Controllers
{
    public class AccountController : BaseController
    {
        private readonly IUnitOfWork uow;
        private readonly IConfiguration configuration;
        public AccountController(IUnitOfWork uow, IConfiguration configuration)
        {
            this.configuration = configuration;
            this.uow = uow;

        }

        //api/account/login
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginReqDto loginReq)
        {
            var user = await uow.UserRepository.Authenticate(loginReq.Username, loginReq.Password);
            ApiError apiError = new ApiError();
            if (user == null)
            {
                apiError.ErrorCode = Unauthorized().StatusCode;
                apiError.ErrorMessage = "Invalid User Id or Password";
                apiError.ErrorDetails = "This error appear when provided user id or password does not exist";
                return Unauthorized(apiError);
            }

            var loginRes = new LoginResDto();
            loginRes.Username = user.Username;
            loginRes.Token = CreateJTW(user);
            return Ok(loginRes);
        }

        //api/account/register
        [HttpPost("register")]
        public async Task<IActionResult> Register(LoginReqDto loginReq)
        {
            
            ApiError apiError = new ApiError();
            if(String.IsNullOrEmpty(loginReq.Username) || String.IsNullOrEmpty(loginReq.Password)){
                apiError.ErrorCode = BadRequest().StatusCode;
                apiError.ErrorMessage = "Username and password field are required.";
                return BadRequest(apiError);
            }

            if(await uow.UserRepository.UserAlreadyExist(loginReq.Username)){
                apiError.ErrorCode = BadRequest().StatusCode;
                apiError.ErrorMessage = "User already exists, please try different user name.";
                return BadRequest(apiError);
            }
            uow.UserRepository.Register(loginReq.Username, loginReq.Password);
            await uow.SaveAsync();
            return StatusCode(201);
        }

        // private bool UsernameAndPasswordIsNull(LoginReqDto loginReq)
        // {
        //     return String.IsNullOrEmpty(loginReq.Username) || String.IsNullOrEmpty(loginReq.Password);
        // }

        private string CreateJTW(User user)
        {
            var secretKey = configuration.GetSection("AppSettings:Key").Value;
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var claims = new Claim[]{
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
            };

            var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(10),
                SigningCredentials = signingCredentials
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}