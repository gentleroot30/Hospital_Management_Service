using HospitalMgmtService.RequestResponseModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using static HospitalMgmtService.RequestResponseModel.Constants.Errors;
using System.IO;
using System.Threading.Tasks;
using System;
using HospitalMgmtService.Model;
using HospitalMgmtService.Database;
using HospitalMgmtService.RequestResponseModel.RequestModel;
using System.Linq;

namespace HospitalMgmtService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PurchaseDocumentController : ControllerBase
    {
        private readonly DBContext _context;
        private SuccessResponse successResponse = new SuccessResponse();
        private FailureResponse failureResponse = new FailureResponse();
        private ErrorResponse errorResponse = new ErrorResponse();
        public PurchaseDocumentController(DBContext context)
        {
            _context = context;
        }
        private async Task<string> UploadPurchaseDocument(IFormFile file)
        {
            string filename = "";                   
                var extension = "." + file.FileName.Split('.')[file.FileName.Split('.').Length - 1];
                filename = DateTime.Now.Ticks.ToString() + extension;
                var filepath = Path.Combine(Directory.GetCurrentDirectory(), "Static\\PurchaseDocument");

                if (!Directory.Exists(filepath))
                {
                    Directory.CreateDirectory(filepath);
                }

                var exactpath = Path.Combine(Directory.GetCurrentDirectory(), "Static\\PurchaseDocument", filename);
                using (var stream = new FileStream(exactpath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
                return filename;                      
        }

        [HttpPost]
        [Route("UploadPurchaseDocument")]
        public async Task<IActionResult> UploadPurchaseDocument([FromForm] PurchaseDocumentRequest purchaseDocumentRequest, [FromHeader(Name = "userId")] int userId)
        {
            try
            {
                IFormFile file = purchaseDocumentRequest.file;
                //int userId = quotationDocumentRequest.userId;

                var result = await UploadPurchaseDocument(file);

                var addPurchaseDocument = new PurchaseDocument()
                {
                    DocumentName = Path.GetFileName(result),
                    DocumentPath = result,
                    CreatedAt = DateTime.Now,
                    CreatedBy = userId,
                    DocumentTypes = purchaseDocumentRequest.DocumentTypes,
                    PurchaseIdFk = purchaseDocumentRequest.PurchaseId

                };

                _context.PurchaseDocuments.Add(addPurchaseDocument);
                _context.SaveChanges();

                var successResponse = new SuccessResponse
                {
                    status = true,
                     data = new
                     {
                         message = Constants.SuccessMessages.PURCHASE_DOCUMENT_SAVED_MESSAGE,
                         purchaseDocumentId = addPurchaseDocument.PurchaseDocumentId // Include the Purchase Document ID
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
        [Route("DownloadPurchaseDocument")]
        public async Task<IActionResult> DownloadPurchaseDocument([FromBody] GetPurchaseDocumentRequest getPurchaseDocumentRequest, [FromHeader(Name = "userId")] int userId)
        {
            try
            {
                var purchaseDocument = _context.PurchaseDocuments.FirstOrDefault(q => q.PurchaseDocumentId == getPurchaseDocumentRequest.purchaseDocumentId);

                if (purchaseDocument == null)
                {
                    return NotFound("Purchase Document not found");
                }

                var filepath = Path.Combine(Directory.GetCurrentDirectory(), "Static\\PurchaseDocument", purchaseDocument.DocumentPath);
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
        [Route("DeletePurchaseDocument")]
        public IActionResult DeletePurchaseDocument([FromBody] DeletePurchaseDocumentRequest deletePurchaseDocumentRequest, [FromHeader(Name = "userId")] int userId)
        {
            try
            {
                var purchaseDocument = _context.PurchaseDocuments.FirstOrDefault(q => q.PurchaseDocumentId ==   deletePurchaseDocumentRequest.purchaseDocumentId);

                if (purchaseDocument == null)
                {
                    return NotFound("Purchase Document not found");
                }

                var filepath = Path.Combine(Directory.GetCurrentDirectory(), "Static\\PurchaseDocument", purchaseDocument.DocumentPath);

                _context.PurchaseDocuments.Remove(purchaseDocument);
                _context.SaveChanges();

                if (System.IO.File.Exists(filepath))
                {
                    System.IO.File.Delete(filepath);
                }

                var successResponse = new SuccessResponse
                {
                    data = Constants.SuccessMessages.PURCHASE_DOCUMENT_DELETED_MESSAGE,
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






