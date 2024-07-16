using HospitalMgmtService.Database;
using HospitalMgmtService.Model;
using HospitalMgmtService.RequestResponseModel;
using HospitalMgmtService.RequestResponseModel.RequestModel;
using HospitalMgmtService.RequestResponseModel.ResponseModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Data;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Threading.Tasks;
using static HospitalMgmtService.Controllers.CustomExceptions;
using static HospitalMgmtService.RequestResponseModel.Constants.Errors;
using System.Security.Cryptography;
using BCrypt.Net;
using System.Text;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.Extensions.Logging;


using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace HospitalMgmtService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly DBContext _context;
        private readonly ILogger<UserController> _logger;

        private SuccessResponse successResponse = new SuccessResponse();
        private FailureResponse failureResponse = new FailureResponse();
        private ErrorResponse errorResponse = new ErrorResponse();
        public UserController(DBContext context, IConfiguration configuration, ILogger<UserController> logger)
        {
            _context = context;
            _configuration = configuration;
            _logger = logger;
        }
        [HttpPost("GetUsers")]
        public async Task<ActionResult<User>> GetUsers([FromBody] GetUsersRequest getUsers)
        {
            try
            {
                var userQuery = _context.Users.Select(x => new GetAllUsersResponse
                {
                    userId = x.UserId,
                    roleId = x.RoleIdFk,
                    name = x.Name,
                    address = x.Address,
                    emailId = x.Email,
                    roleName = x.Roles.RoleName,
                    contactNo_1 = x.ContactNo1,
                    contactNo_2 = x.ContactNo2,
                    contactNo_3 = x.ContactNo3,
                    createdAt = x.CreatedAt,
                    createdBy = x.CreatedBy
                }).AsQueryable();

                if (getUsers.fromtDate.HasValue && getUsers.totDate.HasValue)
                {
                    DateTime startDate = getUsers.fromtDate.Value;
                    DateTime endDate = getUsers.totDate.Value.AddDays(1);

                    userQuery = userQuery.Where(x => x.createdAt >= startDate && x.createdAt < endDate);
                }

                switch (getUsers.searchByType)
                {
                    case 1:
                        userQuery = userQuery.Where(role => role.roleName.Contains(getUsers.searchByValue));
                        break;
                    case 2:
                        userQuery = userQuery.Where(user => user.name.Contains(getUsers.searchByValue));
                        break;
                    case 3:
                        userQuery = userQuery.Where(user => user.contactNo_1.Contains(getUsers.searchByValue));
                        break;
                    case 4:
                        userQuery = userQuery.Where(user => user.emailId.Contains(getUsers.searchByValue));
                        break;
                    default:
                        var errorResponse = new ErrorResponse
                        {
                            message = Constants.Errors.Messages.INVALID_SEARCH_TYPE_MESSAGE,
                            code = Constants.Errors.Codes.INVALID_SEARCHTYPE_ERROR_CODE
                        };
                        var failureResponse = new FailureResponse
                        {
                            status = false,
                            error = errorResponse
                        };
                        return BadRequest(failureResponse);
                }

                var user = await userQuery.ToListAsync();

                var successResponse = new SuccessResponse
                {
                    status = true,
                    data = user
                };
                return Ok(successResponse);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("GetUserById")]
        public async Task<IActionResult> GetUserById([FromBody] GetUserByIdRequest userById)
        {
            try
            {
                var user = _context.Users.Where(r => r.UserId == userById.userId).Select(u => new GetAllUsersResponse
                {
                    userId = u.UserId,
                    roleId = u.RoleIdFk,
                    name = u.Name,
                    emailId = u.Email,
                    address = u.Address,
                    roleName = u.Roles.RoleName,
                    contactNo_1 = u.ContactNo1,
                    contactNo_2 = u.ContactNo2,
                    contactNo_3 = u.ContactNo3
                }).FirstOrDefault();

                if (user != null)
                {

                    var successResponse = new SuccessResponse();
                    successResponse.status = true;
                    successResponse.data = user;
                    return Ok(successResponse);
                }
                else
                {
                    errorResponse = new ErrorResponse();
                    errorResponse.message = Constants.Errors.Messages.USER_ID_DOES_NOT_EXIST;
                    errorResponse.code = Constants.Errors.Codes.USER_ID_DOES_NOT_EXISTS;
                    failureResponse = new FailureResponse();
                    failureResponse.status = false;
                    failureResponse.error = errorResponse;
                    return NotFound(failureResponse);

                }
            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("AddUser")]
        public async Task<IActionResult> AddUser([FromBody] AddUserRequest addUser, [FromHeader(Name = "userId")] int userId)
        {
            try
            {

                var existingRole = _context.Roles.FirstOrDefault(b => b.RoleId == addUser.roleId);
                if (existingRole == null)
                {
                    throw new FailedToFetchUserData();
                }
                var existingUser = _context.Users.FirstOrDefault(u => u.Email == addUser.emailId);
                if (existingUser == null)
                {
                    if (string.IsNullOrWhiteSpace(addUser.name))
                    {
                        errorResponse = new ErrorResponse();
                        errorResponse.message = Messages.USER_NAME_CAN_NOT_BE_BLANK_MESSAGE;
                        errorResponse.code = Codes.USER_NAME_CAN_NOT_BE_BLANK_CODE;
                        failureResponse = new FailureResponse();
                        failureResponse.status = false;
                        failureResponse.error = errorResponse;
                        return BadRequest(failureResponse);
                    }

                    // Check if email is not blank
                    if (string.IsNullOrWhiteSpace(addUser.emailId))
                    {
                        errorResponse = new ErrorResponse();
                        errorResponse.message = Messages.USER_EMAIL_CAN_NOT_BE_BLANK_MESSAGE;
                        errorResponse.code = Codes.USER_EMAIL_CAN_NOT_BE_BLANK_CODE;
                        failureResponse = new FailureResponse();
                        failureResponse.status = false;
                        failureResponse.error = errorResponse;
                        return BadRequest(failureResponse);
                    }

                    if (!IsValidEmail(addUser.emailId))
                    {
                        errorResponse = new ErrorResponse();
                        errorResponse.message = Messages.INVALID_USER_EMAIL_MESSAGE;
                        errorResponse.code = Codes.INVALID_USER_EMAIL_ERROR_CODE;
                        failureResponse = new FailureResponse();
                        failureResponse.status = false;
                        failureResponse.error = errorResponse;
                        return BadRequest(failureResponse);
                    }

                    var plainPassword = GenerateRandomPassword();
                    var hashedPassword = BCrypt.Net.BCrypt.HashPassword(plainPassword);

                    var users = new User()
                    {
                        RoleIdFk = addUser.roleId,
                        Name = addUser.name,
                        Email = addUser.emailId,
                        Address = addUser.address,
                        ContactNo1 = addUser.contactNo_1,
                        ContactNo2 = addUser.contactNo_2,
                        ContactNo3 = addUser.contactNo_3,
                        //Password = addUser.password,
                        Password = hashedPassword,
                        CreatedBy = userId,
                        CreatedAt = DateTime.Now
                    };
                    _context.Users.Add(users);
                    _context.SaveChanges();


                    SendPasswordEmail(addUser.emailId, plainPassword);


                    successResponse = new SuccessResponse();
                    successResponse.status = true;
                    successResponse.data = new
                    {
                        id = users.UserId,
                        message = Constants.SuccessMessages.USER_SAVED_MESSAGE,
                        password = users.Password
                    };
                    return Ok(successResponse);

                }
                else
                {
                    errorResponse = new RequestResponseModel.ErrorResponse();
                    errorResponse.message = Constants.Errors.Messages.FAILED_TO_ADD_USER_MESSAGE;
                    errorResponse.code = Constants.Errors.Codes.FAILED_TO_ADD_USER_CODE;
                    return BadRequest(failureResponse);

                }
            }
            catch (FailedToFetchUserData e)
            {
                errorResponse = new ErrorResponse();
                errorResponse.message = Messages.ROLE_ID_DOES_NOT_EXISTS_MESSAGE;
                errorResponse.code = Codes.ROLE_ID_DOES_NOT_EXISTS_ERROR_CODE;
                failureResponse = new FailureResponse();
                failureResponse.status = false;
                failureResponse.error = errorResponse;
                return NotFound(failureResponse);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            finally { }

        }
        //[HttpPost("Login")]
        //public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        //{
        //    try
        //    {
        //        var user = _context.Users.FirstOrDefault(u => u.Email == loginRequest.Email);
        //        if (user == null)
        //        {
        //            return Unauthorized(new { message = "Invalid email or password." });
        //        }

        //        if (!BCrypt.Net.BCrypt.Verify(loginRequest.Password, user.Password))
        //        {
        //            return Unauthorized(new { message = "Invalid email or password." });
        //        }

        //        var token = GenerateJwtToken(user);

        //        return Ok(new { token });
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}


        private string GenerateRandomPassword(int length = 8)
        {
            const string validChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var random = new Random();
            var password = new StringBuilder();
            for (int i = 0; i < length; i++)
            {
                password.Append(validChars[random.Next(validChars.Length)]);
            }
            return password.ToString();
        }
        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                var builder = new StringBuilder();
                for (int i = 0; i < hashedBytes.Length; i++)
                {
                    builder.Append(hashedBytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
        private void SendPasswordEmail(string email, string password)
        {
            try
            {
                // Logging configuration values
                Console.WriteLine("Fetching SMTP configuration...");

                var smtpHost = _configuration["EmailSettings:SmtpHost"];
                var smtpPort = int.Parse(_configuration["EmailSettings:SmtpPort"]);
                var fromAddress = _configuration["EmailSettings:FromAddress"];
                var fromPassword = _configuration["EmailSettings:FromPassword"];

                Console.WriteLine($"SMTP Host: {smtpHost}, Port: {smtpPort}, From Address: {fromAddress}");

                var fromMailAddress = new MailAddress(fromAddress, "HMS");
                var toMailAddress = new MailAddress(email);
                string subject = "Your new account password";
                string body = $"<p>Username: <strong>{email}</strong></p><p>Password: <strong>{password}</strong></p>";
                //      System.Net.ServicePointManager.ServerCertificateValidationCallback +=
                //(s, cert, chain, sslPolicyErrors) => true;

                var smtp = new SmtpClient
                {
                    Host = smtpHost,
                    Port = smtpPort,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(fromMailAddress.Address, fromPassword)
                };

                using (var message = new MailMessage(fromMailAddress, toMailAddress)
                {
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = true
                })
                {
                    Console.WriteLine("Attempting to send email...");
                    smtp.Send(message);
                    Console.WriteLine("Email sent successfully.");
                }
            }
            catch (SmtpException ex)
            {
                // Detailed logging for SmtpException
                Console.WriteLine($"SMTP Exception: {ex.Message}");
                Console.WriteLine($"Status Code: {ex.StatusCode}");
                Console.WriteLine($"Inner Exception: {ex.InnerException?.Message}");
            }
            catch (Exception ex)
            {
                // Detailed logging for general exceptions
                Console.WriteLine($"General Exception: {ex.Message}");
                Console.WriteLine($"Inner Exception: {ex.InnerException?.Message}");
            }
        }
        //private string GenerateJwtToken(User user)
        //{
        //    var tokenHandler = new JwtSecurityTokenHandler();
        //    var key = Encoding.ASCII.GetBytes(_configuration["JwtSettings:Secret"]);
        //    var tokenDescriptor = new SecurityTokenDescriptor
        //    {
        //        Subject = new ClaimsIdentity(new[]
        //        {
        //        new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
        //        new Claim(ClaimTypes.Email, user.Email)
        //    }),
        //        Expires = DateTime.UtcNow.AddDays(7),
        //        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        //    };
        //    var token = tokenHandler.CreateToken(tokenDescriptor);
        //    return tokenHandler.WriteToken(token);
        //}
    



    //private void ValidateEmailSettings() // Example validation logic
    //{
    //    if (string.IsNullOrEmpty(_configuration["EmailSettings:SmtpHost"]))
    //    {
    //        throw new ArgumentException("SmtpHost is not configured in app settings.");
    //    }
    //    // Add similar checks for other email settings
    //}



    [HttpPost("UpdateUser")]
        public async Task<IActionResult> UpdateUser([FromBody] UpdateUsersRequest updateUser, [FromHeader(Name = "userId")] int userId)
        {
            try 
            {
                var existingRole = _context.Roles.FirstOrDefault(b => b.RoleId == updateUser.roleId);
                if (existingRole == null)
                {
                    errorResponse = new ErrorResponse();
                    errorResponse.message = Messages.ROLE_ID_DOES_NOT_EXISTS_MESSAGE;
                    errorResponse.code = Codes.ROLE_ID_DOES_NOT_EXISTS_ERROR_CODE;
                    failureResponse = new FailureResponse();
                    failureResponse.status = false;
                    failureResponse.error = errorResponse;
                    return NotFound(failureResponse);
                }
                var existingUser = _context.Users.FirstOrDefault(u => u.UserId == updateUser.userId);
                if(existingUser != null)
                {
                    existingUser.UserId = updateUser.userId;
                    existingUser.RoleIdFk = updateUser.roleId;
                    existingUser.Name = updateUser.name;
                    existingUser.Address = updateUser.address;
                    existingUser.Email = updateUser.emailId;
                    //existingUser.Password = updateUser.password;
                    existingUser.ContactNo1 = updateUser.contactNo_1;
                    existingUser.ContactNo2 = updateUser.contactNo_2;
                    existingUser.ContactNo3 = updateUser.contactNo_3;
                    existingUser.UpdatedBy = userId;
                    existingUser.UpdatedAt = DateTime.Now;

                    _context.Users.Update(existingUser);
                    _context.SaveChanges();
                    successResponse = new SuccessResponse();
                    successResponse.status = true;
                    successResponse.data = new
                    {
                        message = Constants.SuccessMessages.USER_UPDATED_MESSAGE
                    };
                    return Ok(successResponse);

                }
                else
                {
                    errorResponse = new ErrorResponse();
                    errorResponse.message = Messages.FAILED_TO_UPDATE_USER_MESSAGE;
                    errorResponse.code = Codes.FAILED_TO_UPDATE_USER_CODE;
                    failureResponse = new FailureResponse();
                    failureResponse.status = false;
                    failureResponse.error = errorResponse;
                    return NotFound(failureResponse);
                }

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }


        [HttpPost("DeleteUser")]
        public async Task<IActionResult> DeleteUser([FromBody] DeleteUserRequest deleteUser, [FromHeader(Name = "userId")] int userId)

        {
            try
            {
                var existingUser = _context.Users.FirstOrDefault(u => u.UserId == deleteUser.userId);
                if (existingUser != null)
                {
                    _context.Remove(existingUser);
                    _context.SaveChanges();
                    successResponse = new SuccessResponse();
                    successResponse.status = true;
                    successResponse.data = new
                    {
                        message = Constants.SuccessMessages.USER_DELETED_MESSAGE
                    };
                    return Ok(successResponse);
                }
                else
                {
                    errorResponse = new ErrorResponse();
                    errorResponse.message = Messages.FAILED_TO_DELETE_USER_MESSAGE;
                    errorResponse.code = Codes.FAILED_DELETE_USER_CODE;
                    failureResponse = new FailureResponse();
                    failureResponse.status = false;
                    failureResponse.error = errorResponse;
                    return NotFound(failureResponse);
                }

            }catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

        }

    }
}
