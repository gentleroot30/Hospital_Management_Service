using HospitalMgmtService.Database;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using HospitalMgmtService.Model;
using Microsoft.AspNetCore.Authorization;
using HospitalMgmtService.RequestResponseModel.ResponseModel;
using HospitalMgmtService.RequestResponseModel;
using HospitalMgmtService.RequestResponseModel.RequestModel;

namespace HospitalMgmtService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private SuccessResponse successResponse = new SuccessResponse();
        private LoginResponse TokenAPI { get; set; }
        private FailureResponse failureResponse = new FailureResponse();
        private ErrorResponse errorResponse = new ErrorResponse();
        private static Random random = new Random();

        private readonly DBContext _context;
        private readonly IConfiguration _configuration;
        public LoginController(DBContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpPost("Login")]
        public async Task<ActionResult<SuccessResponse>> Login([FromBody] AuthenticationRequestModel user)
        {
            var userExists = await _context.Users.FirstOrDefaultAsync(x => x.Email.ToLower().Trim() == user.Email.ToLower().Trim());

            if (userExists != null)
            {
                bool isPasswordValid = BCrypt.Net.BCrypt.Verify(user.Password, userExists.Password);
                if (isPasswordValid)
                {
                    var tokenHandler = new JwtSecurityTokenHandler();
                    var key = Encoding.ASCII.GetBytes(_configuration["Security:Secret"]);
                    var tokenDescriptor = new SecurityTokenDescriptor
                    {
                        Subject = new ClaimsIdentity(new Claim[]
                        {
                            new Claim("UserId", userExists.UserId.ToString()),
                            new Claim("UserName", userExists.Name ),
                            new Claim("Email", userExists.Email),
                        }),
                        Expires = DateTime.UtcNow.AddDays(1),
                        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                            SecurityAlgorithms.HmacSha256Signature)
                    };
                    var token = tokenHandler.CreateToken(tokenDescriptor);
                    var userAuthenticationViewModel = new UserAuthenticationViewModel
                    {
                        Token = tokenHandler.WriteToken(token)
                    };

                    var successResponse = new SuccessResponse
                    {
                        data = new LoginResponse
                        {
                            userId = userExists.UserId,
                            accessToken = userAuthenticationViewModel.Token,
                            refreshToken = userAuthenticationViewModel.Token
                        },
                        status = true,
                      
                    };

                    return Ok(successResponse);
                }
                else
                {
                    var failureResponse = new FailureResponse
                    {
                        status = false,
                        error = new ErrorResponse
                        {
                            message = Constants.Errors.Messages.INVALID_PASSWORD_MESSAGE,
                            code = Constants.Errors.Codes.INVALID_PASSWORD_ERROR_CODE
                        }
                    };
                    return BadRequest(failureResponse);
                }
            }

            var userNotFoundResponse = new FailureResponse
            {
                status = false,
                error = new ErrorResponse
                {
                    message = Constants.Errors.Messages.INVALID_USER_MESSAGE,
                    code = Constants.Errors.Codes.INVALID_USER_ERROR_CODE
                }
            };
            return BadRequest(userNotFoundResponse);
        }
    

    //[HttpPost("ForgotPassword")]
    //public async Task<ActionResult<User>> ForgotPassword(string searchVal)
    //{
    //    var userExists = await _context.Users.FirstOrDefaultAsync(x => x.Email.ToLower().Trim() == searchVal.ToLower().Trim() || x.ContactNo1.ToLower().Trim() == searchVal.ToLower().Trim());

    //    if (userExists != null)
    //    {
    //        userExists.Password = RandomString(8);
    //        _context.Users.Update(userExists);
    //        _context.SaveChanges();

    //        var successResponse = new SuccessResponse
    //        {
    //            status = true,
    //        };
    //        return Ok(successResponse);

    //    }
    //    errorResponse = new ErrorResponse()

    //    {

    //        message = Constants.Errors.Messages.INVALID_EMAIL_MESSAGE,
    //        code = Constants.Errors.Codes.INVALID_LOGIN_EMAIL_ERROR_CODE
    //    };


    //    failureResponse = new FailureResponse()
    //    {
    //        status = false,
    //        error = errorResponse,
    //    };
    //    return Ok(failureResponse);

    //}

    [NonAction]
        public string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

    }
}