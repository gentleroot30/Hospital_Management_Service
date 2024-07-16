using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Collections.Generic;


namespace HospitalMgmtService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploadController : ControllerBase
    {
        private readonly IWebHostEnvironment _environment;
        private readonly ILogger<UploadController> _logger;

        public UploadController(IWebHostEnvironment environment, ILogger<UploadController> logger)
        {
            _environment = environment;
            _logger = logger;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            try
            {
                if (file == null || file.Length == 0)
                {
                    _logger.LogError("File is null or empty.");
                    return BadRequest("Upload a file");
                }

                _logger.LogInformation("Received file: {FileName}", file.FileName);

                if (string.IsNullOrEmpty(_environment.WebRootPath))
                {
                    _logger.LogError("WebRootPath is null or empty.");
                    return StatusCode(500, "Internal server error: WebRootPath is null or empty.");
                }

                var uploadsFolder = Path.Combine(_environment.WebRootPath, "uploads");
                //_logger.LogInformation("Uploads folder path: {UploadsFolder}", uploadsFolderc);

                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                    _logger.LogInformation("Created uploads folder.");
                }

                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                var filePath = Path.Combine(uploadsFolder, fileName);
                _logger.LogInformation("File path: {FilePath}", filePath);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                    _logger.LogInformation("File copied to path: {FilePath}", filePath);
                }

                var fileUrl = $"{Request.Scheme}://{Request.Host}/uploads/{fileName}";
                _logger.LogInformation("File URL: {FileUrl}", fileUrl);

                return Ok(new { url = fileUrl });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while uploading the file.");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    

    }
}

