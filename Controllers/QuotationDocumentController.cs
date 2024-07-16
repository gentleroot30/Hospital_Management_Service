using HospitalMgmtService.Database;
using HospitalMgmtService.Model;
using HospitalMgmtService.RequestResponseModel;
using HospitalMgmtService.RequestResponseModel.RequestModel;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Logging;
//using RestSharp;
using ServiceStack.Web;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using static HospitalMgmtService.RequestResponseModel.Constants.Errors;

namespace HospitalMgmtService.Controllers

{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class QuotationDocumentController : ControllerBase
    {
        private readonly DBContext _context;
        private SuccessResponse successResponse;
        private FailureResponse failureResponse;
        private ErrorResponse errorResponse;
        public QuotationDocumentController(DBContext context)
        {
            _context = context;
        }
        private async Task<string> UploadQuotationDocument(IFormFile file,long quotationId)
        {
            string filename = "";
            try
            {
                var extension ="." + file.FileName.Split('.')[file.FileName.Split('.').Length - 1];
                filename = DateTime.Now.Ticks.ToString() + extension;
                var filepath = Path.Combine(Directory.GetCurrentDirectory(), "Static\\QuotationDocument\\",quotationId.ToString());

                if(!Directory.Exists(filepath))
                {
                    Directory.CreateDirectory(filepath);
                }

                var exactpath = Path.Combine(filepath, filename);
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
            return filename;
        }

        [HttpPost]
        [Route("UploadQuotationDocument")]
        public async Task<IActionResult> UploadFileController([FromForm] QuotationDocumentRequest quotationDocumentRequest, [FromHeader(Name = "userId")] int userId)
        {
            try
            {
                IFormFile file = quotationDocumentRequest.file;
                //int userId = quotationDocumentRequest.userId;

                var result = await UploadQuotationDocument(file,quotationDocumentRequest.QuotationId);

                var addQuotationDocument = new QuotationDocument()
                {
                    DocumentName = Path.GetFileName(result),
                    DocumentPath = result,
                    CreatedAt = DateTime.Now,
                    CreatedBy = userId,
                    DocumentTypes = quotationDocumentRequest.DocumentTypes,
                    QuotationIdFk = quotationDocumentRequest.QuotationId
                };

                _context.QuotationDocuments.Add(addQuotationDocument);
                _context.SaveChanges();

                var successResponse = new SuccessResponse
                {
                    status = true,
                    data = new
                    {
                        message = Constants.SuccessMessages.PURCHASE_DOCUMENT_SAVED_MESSAGE,
                        quotationDocumentId = addQuotationDocument.QuoatationDocumentId // Include the Purchase Document ID
                    }
                };

                return Ok(successResponse);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("DownloadQuotationDocument")]
        public async Task<IActionResult> DownloadQuotationDocument([FromBody] QuotationDocumentDownloadRequest quotationDocumentDownloadRequest, [FromHeader(Name = "userId")] int userId)
        {
            try
            {
                var quotationDocument = _context.QuotationDocuments.FirstOrDefault(q => q.QuoatationDocumentId == quotationDocumentDownloadRequest.QuoatationDocumentId);

                if (quotationDocument == null)
                {
                    return NotFound("Quotation Document not found");
                }

                var filepath = Path.Combine(Directory.GetCurrentDirectory(), "Static\\QuotationDocument", quotationDocument.DocumentPath);
                var provider = new FileExtensionContentTypeProvider();

                if (!provider.TryGetContentType(filepath, out var contentType))
                {
                    contentType = "application/octet-stream";
                }

                var bytes = await System.IO.File.ReadAllBytesAsync(filepath);
                return File(bytes, contentType, Path.GetFileName(filepath));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("DeleteQuotationDocument")]
        public IActionResult DeleteQuotationDocument([FromBody] QuotationDocumentDeleteRequest quotationDocumentDeleteRequest, [FromHeader(Name = "userId")] int userId)
        {
            try
            {
                var quotationDocument = _context.QuotationDocuments.FirstOrDefault(q => q.QuoatationDocumentId == quotationDocumentDeleteRequest.QuoatationDocumentId);

                if (quotationDocument == null)
                {
                    return NotFound("Quotation Document not found");
                }

                var filepath = Path.Combine(Directory.GetCurrentDirectory(), "Static\\QuotationDocument", quotationDocument.DocumentPath);

                _context.QuotationDocuments.Remove(quotationDocument);
                _context.SaveChanges();

                if (System.IO.File.Exists(filepath))
                {
                    System.IO.File.Delete(filepath);
                }

                var successResponse = new SuccessResponse
                {
                    data = Constants.SuccessMessages.QUOTATION_DOCUMENT_DELETED_MESSAGE,
                    status = true
                };

                return Ok(successResponse);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }







    }
}
