using HospitalMgmtService.Database;
using HospitalMgmtService.Model;
using HospitalMgmtService.RequestResponseModel;
using HospitalMgmtService.RequestResponseModel.RequestModel;
using HospitalMgmtService.RequestResponseModel.ResponseModel;
using Humanizer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ServiceStack.Web;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using static HospitalMgmtService.Controllers.CustomExceptions;
using static HospitalMgmtService.RequestResponseModel.Constants.Errors;

namespace HospitalMgmtService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuotationController : ControllerBase
    {
        private readonly DBContext _context;
        private SuccessResponse successResponse = new SuccessResponse();
        private FailureResponse failureResponse = new FailureResponse();
        private ErrorResponse errorResponse = new ErrorResponse();
        private readonly ILogger<QuotationController> _logger;


        public QuotationController(DBContext context, ILogger<QuotationController> logger)
        {

            _context = context;
            _logger = logger;
        }

        [HttpPost("GetQuotations")]
        public async Task<IActionResult> GetQuotations([FromBody] GetQuotationsRequest getQuotations, [FromHeader(Name = "userId")] int userId)
        {
            try
            {
                var quotationQuery = _context.Quotation
                    .Select(q => new GetAllQuotationsResponse
                    {
                        quotationId = q.QuotationId,
                        customerCategoryName = q.Customer.CustomerCategory.CategoryName,
                        customerName = q.Customer.CustomerName,
                        ethnicity = q.Customer.Ethnicity,
                        address = q.Customer.Address,
                        customerContactNo = q.Customer.ContactNo_1,
                        quotationDate = q.QuotationDate,
                        quotationNo = q.QuotationNo,
                        createdBy = q.CreatedBy,
                        createdAt = q.CreatedAt
                    })
                    .AsQueryable();

                // Apply search filters based on searchByType
                switch (getQuotations.searchByType)
                {
                    case 1:
                        quotationQuery = quotationQuery.Where(q => q.quotationNo.ToUpper().Contains(getQuotations.searchByValue.ToUpper()));
                        break;
                    case 2:
                        quotationQuery = quotationQuery.Where(q => q.customerCategoryName.ToUpper().Contains(getQuotations.searchByValue.ToUpper()));
                        break;
                    case 3:
                        quotationQuery = quotationQuery.Where(q => q.customerName.ToUpper().Contains(getQuotations.searchByValue.ToUpper()));
                        break;
                    case 4:
                        quotationQuery = quotationQuery.Where(q => q.ethnicity.ToUpper().Contains(getQuotations.searchByValue.ToUpper()));
                        break;
                    default:
                        throw new InvalidSearchType();
                }

                // Apply date filters if both dates are provided
                if (getQuotations.fromtDate.HasValue && getQuotations.totDate.HasValue)
                {
                    DateTime startDate = getQuotations.fromtDate.Value;
                    DateTime endDate = getQuotations.totDate.Value.AddDays(1); // To include the end date

                    quotationQuery = quotationQuery.Where(q => q.createdAt >= startDate && q.createdAt < endDate);
                }

                var quotations = await quotationQuery.ToListAsync();

                var successResponse = new SuccessResponse
                {
                    status = true,
                    data = quotations
                };

                return Ok(successResponse);
            }
            catch (InvalidSearchType ex)
            {
                _logger.LogError($" Exception Message: {ex.Message}");
                _logger.LogError($" Exception Stack Trace: {ex.StackTrace}");

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
            catch (Exception ex)
            {
                _logger.LogError($" Exception Message: {ex.Message}");
                _logger.LogError($" Exception Stack Trace: {ex.StackTrace}");
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("GetQuotationById")]
        public async Task<IActionResult> GetQuotationById(GetQuotationByIdRequest getQuotationById, [FromHeader(Name = "userId")] int userId)
        {
            try
            {
                var quotation = await _context.Quotation.Include(q => q.QuotationDocuments)
                    .Include(q => q.QuotationRecord)
                    .Where(q => q.QuotationId == getQuotationById.quotationId)
                    .Select(q => new GetQuotationByIdResponse
                    {
                        quotationId = q.QuotationId,
                        quotationNo = q.QuotationNo,
                        customerId = q.Customer.CustomerId,
                        customerName = q.Customer.CustomerName,
                        ethnicity = q.Customer.Ethnicity,
                        address = q.Customer.Address,
                        customerContactNo = q.Customer.ContactNo_1,
                        quotationDate = q.QuotationDate,
                        newField1 = q.NewField1,
                        newField2 = q.NewField2,
                        newField3 = q.NewField3,
                        newField4 = q.NewField4,
                        newField5 = q.NewField5,
                        newField6 = q.NewField6,
                        newField7 = q.NewField7,
                        newField8 = q.NewField8,
                        saleNote1 = q.SaleNote1,
                        saleNote2 = q.SaleNote2,
                        saleNote3 = q.SaleNote3,
                        saleNote4 = q.SaleNote4,
                        createdAt = q.CreatedAt,
                        createdBy = q.CreatedBy,
                       

                        Products = q.QuotationRecord.Select(p => new ProductDto
                        {
                            productId = p.Product.ProductId,
                            productName = p.Product.ProductName,
                            customField1=p.Product.CustomField1,
                            customField2 = p.Product.CustomField2,
                            customField3 = p.Product.CustomField3,

                        }).ToList(),

                          Documents = q.QuotationDocuments.Select(p => new DocumentsDTO
                          {
                              documentId = p.QuoatationDocumentId,
                              documentType = p.DocumentTypes,
                              documentName = p.DocumentName,

                          }).ToList()

                    }).FirstOrDefaultAsync();

                if (quotation != null)
                {
                    var successResponse = new SuccessResponse();

                    successResponse.status = true;
                    successResponse.data = quotation;
                    return Ok(successResponse);
                }
                else
                {
                    throw new QuotationIdDoesnotExists();}

            }
            catch(QuotationIdDoesnotExists ex)
            {

                _logger.LogError($" Exception Message: {ex.Message}");
                _logger.LogError($" Exception Stack Trace: {ex.StackTrace}");
                errorResponse = new ErrorResponse();
                errorResponse.message = Constants.Errors.Messages.QUOTATION_ID_DOES_NOT_EXISTS_MESSAGE;
                errorResponse.code = Constants.Errors.Codes.QUOTATION_ID_DOES_NOT_EXISTS_ERROR_CODE;
                failureResponse = new FailureResponse();
                failureResponse.status = false;
                failureResponse.error = errorResponse;
                return NotFound(failureResponse);

            }
            catch (Exception ex)
            {

                _logger.LogError($" Exception Message: {ex.Message}");
                _logger.LogError($" Exception Stack Trace: {ex.StackTrace}");
                return BadRequest(ex.Message);

            }


        }

        [HttpPost("AddQuotation")]
        public async Task<IActionResult> AddQuotation([FromBody] AddQuotationRequest addQuotationRequest, [FromHeader(Name = "userId")] int userId)
        {
            try
            {

               

                var newQuotationNumber = GenerateNewQuotationNumber();
                    Quotation newQuotation = new Quotation();
                    newQuotation.CustomerIdFk = (long)addQuotationRequest.customerId;
                    newQuotation.QuotationNo = newQuotationNumber;
                    newQuotation.QuotationDate = addQuotationRequest.quotationDate;
                    newQuotation.NewField1 = addQuotationRequest.newField1;
                    newQuotation.NewField2 = addQuotationRequest.newField2;
                    newQuotation.NewField3 = addQuotationRequest.newField3;
                    newQuotation.NewField4 = addQuotationRequest.newField4;
                    newQuotation.NewField5= addQuotationRequest.newField5;
                    newQuotation.NewField6 = addQuotationRequest.newField6;
                    newQuotation.NewField7 = addQuotationRequest.newField7;
                    newQuotation.NewField8 = addQuotationRequest.newField8;
                    newQuotation.SaleNote1 = addQuotationRequest.saleNote1;
                    newQuotation.SaleNote2 = addQuotationRequest.saleNote2;
                    newQuotation.SaleNote3 = addQuotationRequest.saleNote3;
                    newQuotation.SaleNote4 = addQuotationRequest.saleNote4;
                    newQuotation.CreatedBy = userId;
                    newQuotation.CreatedAt = DateTime.Now;

                if (addQuotationRequest.customerId == 0 )
                {
                    throw new CustomerIdCannotBeBlank();
                }

                _context.Quotation.Add(newQuotation);
                    _context.SaveChanges();

                  var  existingQuotation = _context.Quotation.Find(newQuotation.QuotationId);


                    foreach (var productDto in addQuotationRequest.Products)
                    {
                        Product product = _context.Product.Find(productDto.productId);

                        if (product == null)
                        {
                            throw new ProductIdDoesnotExists();
                        }

                        QuotationRecord quotationRecord = new QuotationRecord();
                        quotationRecord.Quotation = existingQuotation;
                        quotationRecord.Product = product;
                        quotationRecord.CreatedBy = userId;
                        quotationRecord.CreatedAt = DateTime.Now;
                        _context.QuotationRecord.Add(quotationRecord);
                        _context.SaveChanges();
                    }

                    

                    successResponse.status = true;
                    successResponse.data = new
                    {
                        id = newQuotation.QuotationId,
                        message = Constants.SuccessMessages.QUOTATION_SAVED_MESSAGE,

                    };
                    return Ok(successResponse);
     
            
               
            }
            catch(CustomerIdCannotBeBlank ex)
            {

                _logger.LogError($" Exception Message: {ex.Message}");
                _logger.LogError($" Exception Stack Trace: {ex.StackTrace}");
                errorResponse = new RequestResponseModel.ErrorResponse();
                errorResponse.message = Constants.Errors.Messages.CUSTOMER_ID_CAN_NOT_BE_BLANK;
                errorResponse.code = Constants.Errors.Codes.CUSTOMER_ID_CAN_NOT_BE_BLANK_ERROR_CODE;
                failureResponse = new RequestResponseModel.FailureResponse();
                failureResponse.status = false;
                failureResponse.error = errorResponse;
                return BadRequest(failureResponse);
            }
            catch(QuotationNoCannotBeBlank ex)
            {

                _logger.LogError($" Exception Message: {ex.Message}");
                _logger.LogError($" Exception Stack Trace: {ex.StackTrace}");
                errorResponse = new RequestResponseModel.ErrorResponse();
                errorResponse.message = Constants.Errors.Messages.QUOTATION_NO_CAN_NOT_BE_BLANK;
                errorResponse.code = Constants.Errors.Codes.QUOTATION_NO_CAN_NOT_BE_BLANK_ERROR_CODE;
                failureResponse = new RequestResponseModel.FailureResponse();
                failureResponse.status = false;
                failureResponse.error = errorResponse;
                return BadRequest(failureResponse);
            }
            catch(ProductIdDoesnotExists ex)
            {

                _logger.LogError($" Exception Message: {ex.Message}");
                _logger.LogError($" Exception Stack Trace: {ex.StackTrace}");
                errorResponse = new RequestResponseModel.ErrorResponse();
                errorResponse.message = Constants.Errors.Messages.PRODUCT_ID_DOES_NOT_EXISTS_MESSAGE;
                errorResponse.code = Constants.Errors.Codes.PRODUCT_ID_DOES_NOT_EXISTS_ERROR_CODE;
                failureResponse = new RequestResponseModel.FailureResponse();
                failureResponse.status = false;
                failureResponse.error = errorResponse;
                return NotFound(failureResponse);
            }
            catch(FailedToSaveQuotation ex)
            {

                _logger.LogError($" Exception Message: {ex.Message}");
                _logger.LogError($" Exception Stack Trace: {ex.StackTrace}");
                errorResponse = new ErrorResponse();
                errorResponse.message = Constants.Errors.Messages.FAILED_TO_SAVE_QUOTATION_MESSAGE;
                errorResponse.code = Constants.Errors.Codes.FAILED_TO_DELETE_QUOTATION_DATA_ERROR_CODE;
                failureResponse = new FailureResponse();
                failureResponse.status = false;
                failureResponse.error = errorResponse;
                return BadRequest(failureResponse);

            }
            catch (QuotationAlreadyExists ex)
            {

                _logger.LogError($" Exception Message: {ex.Message}");
                _logger.LogError($" Exception Stack Trace: {ex.StackTrace}");
                errorResponse = new RequestResponseModel.ErrorResponse();
                errorResponse.message = Constants.Errors.Messages.QUOTATION_ALREADY_EXISTS_MESSAGE;
                errorResponse.code = Constants.Errors.Codes.QUOTATION_ALREADY_EXISTS_ERROR_CODE;
                failureResponse = new RequestResponseModel.FailureResponse();
                failureResponse.status = false;
                failureResponse.error = errorResponse;
                return Conflict(failureResponse);

            }
            catch (Exception ex)
            {
                _logger.LogError($" Exception Message: {ex.Message}");
                _logger.LogError($" Exception Stack Trace: {ex.StackTrace}");
                return BadRequest(ex.Message);
            }

           
        }
        private string GenerateNewQuotationNumber()
        {
            var maxQuotationId = _context.Quotation.Max(c => (int?)c.QuotationId);
            if (maxQuotationId.HasValue)
            {
                return "q-" + (maxQuotationId + 1);
            }

            return "q-1";
        }




        [HttpPost("UpdateQuotation")]
        public async Task<IActionResult> UpdateQuotation([FromBody] UpdateQuotationRequest updateQuotationRequest, [FromHeader(Name = "userId")] int userId)
        {
            try
            {
                var existingQuotation = _context.Quotation.FirstOrDefault(r => r.QuotationId == updateQuotationRequest.quotationId);
                if (existingQuotation != null)
                {
                    existingQuotation.CustomerIdFk = updateQuotationRequest.customerId;
                    existingQuotation.QuotationDate = updateQuotationRequest.quotationDate;
                    existingQuotation.NewField1 = updateQuotationRequest.newField1;
                    existingQuotation.NewField2 = updateQuotationRequest.newField2;
                    existingQuotation.NewField3 = updateQuotationRequest.newField3;
                    existingQuotation.NewField4 = updateQuotationRequest.newField4;
                    existingQuotation.NewField5 = updateQuotationRequest.newField5;
                    existingQuotation.NewField6 = updateQuotationRequest.newField6;
                    existingQuotation.NewField7 = updateQuotationRequest.newField7;
                    existingQuotation.NewField8 = updateQuotationRequest.newField8;
                    existingQuotation.SaleNote1 = updateQuotationRequest.saleNote1;
                    existingQuotation.SaleNote2 = updateQuotationRequest.saleNote2;
                    existingQuotation.SaleNote3 = updateQuotationRequest.saleNote3;
                    existingQuotation.SaleNote4 = updateQuotationRequest.saleNote4;
                    existingQuotation.UpdatedBy = userId;
                    existingQuotation.UpdatedAt = DateTime.Now;
                    var existingQuotationRecord = _context.QuotationRecord.Where(rf => rf.QuotationIdFk == existingQuotation.QuotationId).ToList();
                    _context.SaveChanges();
                    _context.QuotationRecord.RemoveRange(existingQuotationRecord);

                    foreach (var productDto in updateQuotationRequest.Products)
                    {
                        Product product = _context.Product.Find(productDto.productId);
                        if (product == null)
                        {
                            throw new ProductIdDoesnotExists();
                        }
                        QuotationRecord quotationRecord = new QuotationRecord();
                        quotationRecord.Quotation = existingQuotation;
                        quotationRecord.Product = product;
                        quotationRecord.CreatedBy = userId;
                        quotationRecord.CreatedAt = DateTime.Now;
                        _context.QuotationRecord.Add(quotationRecord);
                        _context.SaveChanges();

                    }
                    //foreach (var documentDto in updateQuotationRequest.Documents)
                    //{
                    //    QuotationDocument existingDocument = _context.QuotationDocuments.Find(documentDto.documentId);
                    //    if (existingDocument == null)
                    //    {
                    //        throw new QuotationDocumentPathDoesnotExists();
                    //    }
                    //    existingDocument.DocumentPath = documentDto.documentPath;
                    //    existingDocument.CreatedAt = DateTime.Now;
                    //    existingDocument.CreatedBy = userId;
                    //    string[] documentPaths = documentDto.documentPath.Split('/');
                    //    existingDocument.DocumentName = documentPaths[documentPaths.Length - 1];
                    //    existingDocument.DocumentTypes = documentDto.documentType;

                    //    // Add other document properties as needed
                    //    await _context.SaveChangesAsync();

                    //}


                    var successResponse = new SuccessResponse();
                    successResponse.data = Constants.SuccessMessages.QUOTATION_UPDATED_MESSAGE;
                    successResponse.status = true;

                    return Ok(successResponse);
                }
                else
                {
                    throw new FailedToUpdateQuotation();
                }
            }
            catch (ProductIdDoesnotExists ex)
            {
                _logger.LogError($" Exception Message: {ex.Message}");
                _logger.LogError($" Exception Stack Trace: {ex.StackTrace}");
                errorResponse = new RequestResponseModel.ErrorResponse();
                errorResponse.message = Constants.Errors.Messages.PRODUCT_ID_DOES_NOT_EXISTS_MESSAGE;
                errorResponse.code = Constants.Errors.Codes.PRODUCT_CATEGORY_ID_DOES_NOT_EXISTS_ERROR_CODE;
                failureResponse = new RequestResponseModel.FailureResponse();
                failureResponse.status = false;
                failureResponse.error = errorResponse;
                return NotFound(failureResponse);
            }
            catch(QuotationDocumentPathDoesnotExists ex)
            {

                _logger.LogError($" Exception Message: {ex.Message}");
                _logger.LogError($" Exception Stack Trace: {ex.StackTrace}");
                errorResponse = new RequestResponseModel.ErrorResponse();
                errorResponse.message = Constants.Errors.Messages.QUOTATION_DOCUMENT_PATH_DOES_NOT_EXISTS_MESSAGE;
                errorResponse.code = Constants.Errors.Codes.QUOTATION_DOCUMENT_PATH_DOES_NOT_EXISTS_ERROR_CODE;
                failureResponse = new RequestResponseModel.FailureResponse();
                failureResponse.status = false;
                failureResponse.error = errorResponse;
                return NotFound(failureResponse);
            }
            catch(FailedToUpdateQuotation ex)
            {

                _logger.LogError($" Exception Message: {ex.Message}");
                _logger.LogError($" Exception Stack Trace: {ex.StackTrace}");
                errorResponse = new ErrorResponse();
                errorResponse.message = Constants.Errors.Messages.FAILED_TO_UPDATE_QUOTATION_MESSAGE;
                errorResponse.code = Constants.Errors.Codes.FAILED_TO_UPDATE_QUOTATION_DATA_ERROR_CODE;
                failureResponse = new RequestResponseModel.FailureResponse();
                failureResponse.status = false;
                failureResponse.error = errorResponse;
                return NotFound(failureResponse);
            }
            catch (Exception ex)
            {
                _logger.LogError($" Exception Message: {ex.Message}");
                _logger.LogError($" Exception Stack Trace: {ex.StackTrace}");
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("DeleteQuotation")]

        public async Task<IActionResult> DeleteQuotation([FromBody] DeleteQuotationRequest deleteQuotationRequest, [FromHeader(Name = "userId")] int userId)
        {
            try
            {
                var existingQuotation = _context.Quotation.Include(qt => qt.QuotationRecord)
                    .FirstOrDefault(p => p.QuotationId == deleteQuotationRequest.quotationId);
                var existingQuotationDocu = _context.Quotation.Include(qt => qt.QuotationDocuments)
                    .FirstOrDefault(p => p.QuotationId == deleteQuotationRequest.quotationId);

                if (existingQuotation != null && existingQuotationDocu != null)
                {
                    var filepath = Path.Combine(Directory.GetCurrentDirectory(), "Static\\QuotationDocument", deleteQuotationRequest.quotationId.ToString());
                    DirectoryInfo directory = new DirectoryInfo(filepath);

                    if(directory.Exists)
                    {
                        this.Empty(directory);
                        _context.QuotationDocuments.RemoveRange(existingQuotation.QuotationDocuments);
                        _context.QuotationRecord.RemoveRange(existingQuotation.QuotationRecord);
                        _context.Quotation.RemoveRange(existingQuotation);
                        await _context.SaveChangesAsync();
                    }
                    else
                    {
                        _context.QuotationRecord.RemoveRange(existingQuotation.QuotationRecord);
                        _context.Quotation.RemoveRange(existingQuotation);
                        await _context.SaveChangesAsync(true);
                    }
                    
                    var success = new SuccessResponse
                    {

                        status = true,
                        data = new
                        {
                            message = Constants.SuccessMessages.QUOTATION_DELETED_MESSAGE
                        }

                    };
                    return Ok(success);
                }
                else
                {
                    throw new FailedToDeleteCustomer();
                }

            }
            catch (FailedToDeleteQuotation ex)
            {

                _logger.LogError($" Exception Message: {ex.Message}");
                _logger.LogError($" Exception Stack Trace: {ex.StackTrace}");
                errorResponse = new RequestResponseModel.ErrorResponse();

                errorResponse.message = Constants.Errors.Messages.FAILED_TO_DELETE_QUOTATION_MESSAGE;
                errorResponse.code = Constants.Errors.Codes.FAILED_TO_DELETE_QUOTATION_DATA_ERROR_CODE;
                failureResponse = new RequestResponseModel.FailureResponse();
                failureResponse.status = false;
                failureResponse.error = errorResponse;
                return NotFound(failureResponse);
            }
            catch (Exception ex)
            {
                _logger.LogError($" Exception Message: {ex.Message}");
                _logger.LogError($" Exception Stack Trace: {ex.StackTrace}");
                return BadRequest(ex.Message);

            }
          
        }
        private void Empty( DirectoryInfo directory)
        {
            foreach (FileInfo file in directory.GetFiles())
            {
                file.Delete();
            }
            foreach (DirectoryInfo subDirectory in directory.GetDirectories())
            {
                subDirectory.Delete(true);
            } 
               
        }
    }
}
