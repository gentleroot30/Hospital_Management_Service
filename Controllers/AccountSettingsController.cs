using HospitalMgmtService.Model;
using HospitalMgmtService.RequestResponseModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System;
using HospitalMgmtService.RequestResponseModel.RequestModel;
using HospitalMgmtService.Database;
using Microsoft.AspNetCore.StaticFiles;

namespace HospitalMgmtService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountSettingsController : ControllerBase
    {
        private readonly DBContext _context;
        private SuccessResponse successResponse = new SuccessResponse();
        private FailureResponse failureResponse = new FailureResponse();
        private ErrorResponse errorResponse = new ErrorResponse();
        public AccountSettingsController(DBContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Route("UploadProfilePhoto")]
        public async Task<IActionResult> UploadProfilePhoto([FromForm] ProfilePhotoUploadRequest profilePhotoRequest, [FromHeader(Name = "userId")] int userId)
        {
            try
            {
                IFormFile file = profilePhotoRequest.file;

                var result = await UploadProfilePhoto(file);

                var user = _context.Users.FirstOrDefault(u => u.UserId == userId);

                if (user == null)
                {
                    return NotFound("User not found");
                }

                user.ProfilePicture = Convert.FromBase64String(result);

                _context.SaveChanges();

                var successResponse = new SuccessResponse
                {
                    status = true,
                    data = new
                    {
                        message = Constants.SuccessMessages.PROFILE_PHOTO_SAVED_MESSAGE,
                        userId = user.UserId
                    }
                };

                return Ok(successResponse);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        private async Task<string> UploadProfilePhoto(IFormFile file)
        {
            try
            {
                if (!IsImageFileValid(file))
                {
                    throw new Exception("Invalid file type. Only JPEG, PNG, or JPG files are allowed.");
                }

                using (MemoryStream ms = new MemoryStream())
                {
                    await file.CopyToAsync(ms);
                    byte[] fileBytes = ms.ToArray();
                    return Convert.ToBase64String(fileBytes);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private bool IsImageFileValid(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return false;
            }

            var allowedExtensions = new[] { ".jpeg", ".jpg", ".png" };
            var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
            return allowedExtensions.Contains(extension);
        }


        [HttpPost]
        [Route("DeleteProfilePhoto")]
        public IActionResult DeleteProfilePhoto([FromBody] DeleteProfilePhotoRequest deleteProfilePhotoRequest, [FromHeader(Name = "userId")] int userId)
        {
            try
            {
                var user = _context.Users.FirstOrDefault(u => u.UserId == userId);

                if (user == null)
                {
                    return NotFound("User not found");
                }
                user.ProfilePicture = null;

                _context.SaveChanges();

                var successResponse = new SuccessResponse
                {
                    data = Constants.SuccessMessages.PROFILE_PHOTO_DELETED_MESSAGE,
                    status = true
                };

                return Ok(successResponse);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        private async Task<string> UploadHeader(IFormFile file)
        {
            try
            {
                var allowedExtensions = new[] { ".jpeg", ".jpg", ".png" };
                var extension = Path.GetExtension(file.FileName).ToLower();

                if (!allowedExtensions.Contains(extension))
                {
                    throw new InvalidOperationException("Invalid file format. Please upload a JPEG or PNG file.");
                }

                var filename = DateTime.Now.Ticks.ToString() + extension;
                var filepath = Path.Combine(Directory.GetCurrentDirectory(), "Static\\Header");

                if (!Directory.Exists(filepath))
                {
                    Directory.CreateDirectory(filepath);
                }

                var exactpath = Path.Combine(Directory.GetCurrentDirectory(), "Static\\Header", filename);

                using (var stream = new FileStream(exactpath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                return filename;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        [Route("UploadHeader")]
        public async Task<IActionResult> UploadHeader([FromForm] HeaderRequest headerRequest, [FromHeader(Name = "userId")] int userId)
        {
            try
            {
                IFormFile file = headerRequest.file;
                var result = await UploadHeader(file);

                var successResponse = new SuccessResponse
                {
                    status = true,
                    data = new
                    {
                        message = Constants.SuccessMessages.HEADER_PHOTO_SAVED_MESSAGE,
                        // purchaseDocumentId = addPurchaseDocument.PurchaseDocumentId 
                    }
                };

                return Ok(successResponse);
            }
            catch (InvalidOperationException ex)
            {
                // Handle invalid file format exception
                errorResponse = new ErrorResponse
                {
                    message = ex.Message,
                    code = Constants.Errors.Codes.INVALID_FILE_FORMAT_ERROR_CODE
                };

                failureResponse = new FailureResponse
                {
                    status = false,
                    error = errorResponse
                };

                return BadRequest(failureResponse);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("DeleteHeader")]
        public IActionResult DeleteHeader([FromHeader(Name = "userId")] int userId)
        {
            try
            {
                var filepath = Path.Combine(Directory.GetCurrentDirectory(), "Static\\Header");

                if (Directory.Exists(filepath))
                {
                    // Delete all files in the directory
                    var files = Directory.GetFiles(filepath);
                    foreach (var file in files)
                    {
                        System.IO.File.Delete(file);
                    }

                }

                var successResponse = new SuccessResponse
                {
                    data = Constants.SuccessMessages.HEDAER_PHOTO_DELETED_MESSAGE,
                    status = true
                };

                return Ok(successResponse);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        private async Task<string> UploadFooter(IFormFile file)
        {
            try
            {
                var allowedExtensions = new[] { ".jpeg", ".jpg", ".png" };
                var extension = Path.GetExtension(file.FileName).ToLower();

                if (!allowedExtensions.Contains(extension))
                {
                    throw new InvalidOperationException("Invalid file format. Please upload a JPEG or PNG file.");
                }

                var filename = DateTime.Now.Ticks.ToString() + extension;
                var filepath = Path.Combine(Directory.GetCurrentDirectory(), "Static\\Footer");

                if (!Directory.Exists(filepath))
                {
                    Directory.CreateDirectory(filepath);
                }

                var exactpath = Path.Combine(Directory.GetCurrentDirectory(), "Static\\Footer", filename);

                using (var stream = new FileStream(exactpath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                return filename;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        [Route("UploadFooter")]
        public async Task<IActionResult> UploadFooter([FromForm] FooterRequest footerRequest, [FromHeader(Name = "userId")] int userId)
        {
            try
            {
                IFormFile file = footerRequest.file;
                var result = await UploadFooter(file);

                var successResponse = new SuccessResponse
                {
                    status = true,
                    data = new
                    {
                        message = Constants.SuccessMessages.FOOTER_PHOTO_SAVED_MESSAGE,
                    }
                };

                return Ok(successResponse);
            }
            catch (InvalidOperationException ex)
            {
                errorResponse = new ErrorResponse
                {
                    message = ex.Message,
                    code = Constants.Errors.Codes.INVALID_FILE_FORMAT_ERROR_CODE
                };

                failureResponse = new FailureResponse
                {
                    status = false,
                    error = errorResponse
                };

                return BadRequest(failureResponse);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("DeleteFooter")]
        public IActionResult DeleteFooter([FromHeader(Name = "userId")] int userId)
        {
            try
            {
                var filepath = Path.Combine(Directory.GetCurrentDirectory(), "Static\\Footer");

                if (Directory.Exists(filepath))
                {
                    var files = Directory.GetFiles(filepath);
                    foreach (var file in files)
                    {
                        System.IO.File.Delete(file);
                    }


                }

                var successResponse = new SuccessResponse
                {
                    data = Constants.SuccessMessages.FOOTER_PHOTO_DELETED_MESSAGE,
                    status = true
                };

                return Ok(successResponse);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        private async Task<string> UploadImportFile(IFormFile file)
        {
            try
            {
                var allowedExtensions = new[] { ".xls", ".xlsx" };
                var extension = Path.GetExtension(file.FileName).ToLower();

                if (!allowedExtensions.Contains(extension))
                {
                    throw new InvalidOperationException("Invalid file format. Please upload a Excel file.");
                }

                var filename = DateTime.Now.Ticks.ToString() + extension;
                var filepath = Path.Combine(Directory.GetCurrentDirectory(), "Static\\Import Stock File");

                if (!Directory.Exists(filepath))
                {
                    Directory.CreateDirectory(filepath);
                }

                var exactpath = Path.Combine(Directory.GetCurrentDirectory(), "Static\\Import Stock File", filename);

                using (var stream = new FileStream(exactpath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                return filename;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        [HttpPost]
        [Route("UploadImportStockFile")]
        public async Task<IActionResult> UploadImportFile([FromForm] ImportFileRequest importFileRequest, [FromHeader(Name = "userId")] int userId)
        {
            try
            {
                IFormFile file = importFileRequest.file;
                var result = await UploadImportFile(file);

                var successResponse = new SuccessResponse
                {
                    status = true,
                    data = new
                    {
                        message = Constants.SuccessMessages.IMPORT_STOCK_FILE_SAVED_MESSAGE,
                    }
                };

                return Ok(successResponse);
            }
            catch (InvalidOperationException ex)
            {
                errorResponse = new ErrorResponse
                {
                    message = ex.Message,
                    code = Constants.Errors.Codes.INVALID_FILE_FORMAT_ERROR_CODE
                };

                failureResponse = new FailureResponse
                {
                    status = false,
                    error = errorResponse
                };

                return BadRequest(failureResponse);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("DownloadImportStockTemplate")]
        public IActionResult DownloadImportStockTemplate([FromHeader(Name = "userId")] int userId)
        {
            try
            {
                var filepath = Path.Combine(Directory.GetCurrentDirectory(), "Static\\ImportStockTemplate", "excelfile.xlsx");

                if (!System.IO.File.Exists(filepath))
                {
                    return NotFound("Import Stock Template File not found");
                }

                return PhysicalFile(filepath, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "excelfile.xlsx");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
