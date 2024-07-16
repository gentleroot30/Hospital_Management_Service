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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static HospitalMgmtService.Controllers.CustomExceptions;
using static HospitalMgmtService.RequestResponseModel.Constants.Errors;

namespace HospitalMgmtService.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class QuotationTemplateController : ControllerBase
    {
        private readonly DBContext _context;
        private SuccessResponse successResponse = new SuccessResponse();
        private FailureResponse failureResponse = new FailureResponse();
        private ErrorResponse errorResponse = new ErrorResponse(); 
        private readonly ILogger<QuotationTemplateController> _logger;

        public QuotationTemplateController(DBContext context, ILogger<QuotationTemplateController> logger)
        {
            _context = context;
            logger = _logger;
        }

        [HttpPost("GetQuotationTemplate")]
        public async Task<IActionResult> GetQuotationTemplate([FromBody] GetQuotationTemplatesRequest getQuotationTemplatesRequest, [FromHeader(Name = "userId")] int userId)
        {
            try
            {
                var quotationTempQuery = _context.QuotationTemp.Select(x => new GetAllQuotationTemplateResponse
                {
                  quotationTemplateId=  x.TemplateId,
                  quotationTemplateName =  x.Name,
                  createdAt =x.CreatedAt,
                  createdBy =x.CreatedBy
                }).AsQueryable();


                if (getQuotationTemplatesRequest.searchByType == 1)
                {
                    quotationTempQuery = quotationTempQuery.Where(temp => temp.quotationTemplateName.Contains(getQuotationTemplatesRequest.searchByValue));
                }
                else
                {
                    throw new InvalidSearchType();

                }
                var quotationTemp = await quotationTempQuery.ToListAsync();

                var successResponse = new SuccessResponse();
                successResponse.status = true;
                successResponse.data = quotationTemp;
                return Ok(successResponse);

            }
            catch (InvalidSearchType ex)
            {

                _logger.LogError($" Exception Message: {ex.Message}");
                _logger.LogError($" Exception Stack Trace: {ex.StackTrace}");
                errorResponse = new ErrorResponse();
                errorResponse.message = Constants.Errors.Messages.INVALID_SEARCH_TYPE_MESSAGE;
                errorResponse.code = Constants.Errors.Codes.INVALID_SEARCHTYPE_ERROR_CODE;
                failureResponse = new FailureResponse();
                failureResponse.status = false;
                failureResponse.error = errorResponse;
                return BadRequest(failureResponse);
            }
            catch (Exception ex)
            {
                _logger.LogError($" Exception Message: {ex.Message}");
                _logger.LogError($" Exception Stack Trace: {ex.StackTrace}");
                return BadRequest(ex.Message);
            }
        }


        [HttpPost("GetQuotationTemplateById")]
        public async Task<IActionResult> GetQuotationTemplateById([FromBody] GetQuotationTemplateByIdRequest getQuotationTemplateByIdRequest, [FromHeader(Name = "userId")] int userId)
        {
            try
            {
                bool quotationTemplateExists = await _context.QuotationTemp
                    .Where(p => p.TemplateId == getQuotationTemplateByIdRequest.quotationTemplateId)
                    .AnyAsync();

                if (quotationTemplateExists)
                {
                     var  quotationTemplateById = await _context.QuotationTemp
                        .Where(p => p.TemplateId == getQuotationTemplateByIdRequest.quotationTemplateId)
                        .Include(p => p.QuotationTempRecords)
                        .Select(p => new GetQuotationTemplateByIdResponse
                        {
                            quotationTemplateId = p.TemplateId,
                            quotationTemplateName = p.Name,
                            quotationTemplateDescription = p.Description,
                            quotationTempProducts = p.QuotationTempRecords.Select(pr => new RequestResponseModel.ResponseModel.QuotationTempProductDTO
                            {
                                productId = pr.ProductIdFk,
                                productName = pr.Products.ProductName,
                                customField1 = pr.Products.CustomField1,
                                customField2 = pr.Products.CustomField2,
                                customField3 = pr.Products.CustomField3,
                            }).ToList()
                        }).FirstOrDefaultAsync();

                    var successResponse = new SuccessResponse();
                    successResponse.status = true;
                    successResponse.data = quotationTemplateById;
                    return Ok(successResponse);
                }
                else
                {
                    errorResponse = new ErrorResponse();
                    errorResponse.message = Constants.Errors.Messages.QUOTATION_TEMPLATE_ID_DOES_NOT_EXISTS_MESSAGE;
                    errorResponse.code = Constants.Errors.Codes.QUOTATION_TEMPLATE_ID_DOES_NOT_EXISTS_ERROR_CODE;
                    failureResponse = new FailureResponse();
                    failureResponse.status = false;
                    failureResponse.error = errorResponse;
                    return NotFound(failureResponse);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($" Exception Message: {ex.Message}");
                _logger.LogError($" Exception Stack Trace: {ex.StackTrace}");
                return BadRequest(ex.Message);
            }
        }


      
        [HttpPost("AddQuotationTemplate")]
        public async Task<IActionResult> AddQuotationTemplate([FromBody] AddQuotationTemplateRequest addQuotationTemplate, [FromHeader(Name = "userId")] int userId)
        {
            try

            {
                QuotationTemp newQuotationTemp = new QuotationTemp();
                newQuotationTemp.Name = addQuotationTemplate.quotationTemplateName;
                newQuotationTemp.Description = addQuotationTemplate.quotationTemplateDescription;
                newQuotationTemp.CreatedBy = userId;
                newQuotationTemp.CreatedAt = DateTime.Now;

                if (string.IsNullOrWhiteSpace(newQuotationTemp.Name))
                {
                    throw new QuotationTemplateNameCannotBeBlank();
                }
                _context.QuotationTemp.Add(newQuotationTemp);

                _context.SaveChanges();

                QuotationTemp quotationTemp = _context.QuotationTemp.Find(newQuotationTemp.TemplateId);

                foreach (var tempProduct in addQuotationTemplate.products)
                {
                    Product product = _context.Product.Find(tempProduct.productId);

                    if (product == null)
                    {
                        throw new ProductIdDoesnotExists();
                    }

                    QuotationTempRecord quotationTempRecord = new QuotationTempRecord();
                    quotationTempRecord.QuotationTemp = quotationTemp;
                    quotationTempRecord.Products = product;
                    quotationTempRecord.CreatedAt = DateTime.Now;
                    quotationTempRecord.CreatedBy = userId;
                    _context.QuotationTempRecord.Add(quotationTempRecord);
                    _context.SaveChanges();
                }
                if (newQuotationTemp.TemplateId > 0)
                {
                    successResponse.status = true;
                    successResponse.data = new
                    {
                        id = newQuotationTemp.TemplateId,
                        message = Constants.SuccessMessages.QUOTATION_TEMPLATE_SAVED_MESSAGE,

                    };
                    return Ok(successResponse);
                }

                else
                {
                    throw new FailedToSaveQuotationTemplate();
                }
            }
            catch (ProductIdDoesnotExists ex)
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
            catch (QuotationTemplateNameCannotBeBlank ex)
            {
                _logger.LogError($" Exception Message: {ex.Message}");
                _logger.LogError($" Exception Stack Trace: {ex.StackTrace}");
                errorResponse = new ErrorResponse();
                errorResponse.message = Messages.QUOTATION_TEMPLATE_NAME_CAN_NOT_BE_BLANK_MESSAGE;
                errorResponse.code = Codes.QUOTATION_TEMPLATE_NAME_CAN_NOT_BE_BLANK_ERROR_CODE;
                failureResponse = new FailureResponse();
                failureResponse.status = false;
                failureResponse.error = errorResponse;
                return BadRequest(failureResponse);
            }
            catch (FailedToSaveQuotationTemplate ex)
            {

                _logger.LogError($" Exception Message: {ex.Message}");
                _logger.LogError($" Exception Stack Trace: {ex.StackTrace}");
                errorResponse = new RequestResponseModel.ErrorResponse();
                errorResponse.message = Constants.Errors.Messages.FAILED_TO_SAVE_QUOTATION_TEMPLATE_MESSAGE;
                errorResponse.code = Constants.Errors.Codes.FAILED_TO_SAVE_QUOTATION_TEMPLATE_DATA_ERROR_CODE;
                failureResponse = new RequestResponseModel.FailureResponse();
                failureResponse.status = false;
                failureResponse.error = errorResponse;
                return BadRequest(failureResponse);

            }
            catch (Exception ex)
            {
                _logger.LogError($" Exception Message: {ex.Message}");
                _logger.LogError($" Exception Stack Trace: {ex.StackTrace}");
                return BadRequest(ex.Message);

            }
            finally { }

        }


        [HttpPost("UpdateQuotationTemplate")]
        public async Task<IActionResult> UpdateQuotationTemplate([FromBody] UpdateQuotationTemplateRequest updateQuotationTemplateRequest, [FromHeader(Name = "userId")] int userId)
        {
            try
            {
                var existingQuotationTemp = _context.QuotationTemp.FirstOrDefault(r => r.TemplateId == updateQuotationTemplateRequest.quotationTemplateId);
                if (existingQuotationTemp != null)
                {
                    existingQuotationTemp.Name = updateQuotationTemplateRequest.quotationTemplateName;
                    existingQuotationTemp.Description = updateQuotationTemplateRequest.quotationTemplateDescription;
                    existingQuotationTemp.UpdatedBy = userId;
                    existingQuotationTemp.UpdatedAt = DateTime.Now;

                    var existingQuotationTempRecord = _context.QuotationTempRecord.Where(rf => rf.TemplateIdFk == existingQuotationTemp.TemplateId).ToList();
                    _context.SaveChanges();
                    _context.QuotationTempRecord.RemoveRange(existingQuotationTempRecord);


                    foreach (var productDto in updateQuotationTemplateRequest.products)
                    {
                        Product product = _context.Product.Find(productDto.productId);
                        if (product == null)
                        {
                            throw new ProductIdDoesnotExists();
                        }
                        QuotationTempRecord quotationTempRecord = new QuotationTempRecord();
                        quotationTempRecord.QuotationTemp = existingQuotationTemp;
                        quotationTempRecord.Products = product;
                        quotationTempRecord.CreatedBy = userId;
                        quotationTempRecord.CreatedAt = DateTime.Now;

                        _context.QuotationTempRecord.Add(quotationTempRecord);
                        _context.SaveChanges();
                    }



                    var successResponse = new SuccessResponse();
                    successResponse.data = Constants.SuccessMessages.QUOTATION_TEMPLATE_SAVED_MESSAGE;
                    successResponse.status = true;
                    return Ok(successResponse);
                }
                else
                {
                    throw new FailedToUpdateQuotationTemplate();
                }

            }
            catch (ProductIdDoesnotExists ex)
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
            catch (FailedToUpdateQuotationTemplate ex)
            {
                _logger.LogError($" Exception Message: {ex.Message}");
                _logger.LogError($" Exception Stack Trace: {ex.StackTrace}");
                errorResponse = new ErrorResponse();
                errorResponse.message = Constants.Errors.Messages.FAILED_TO_UPDATE_QUOTATION_TEMPLATE_MESSAGE;
                errorResponse.code = Constants.Errors.Codes.FAILED_TO_UPDATE_QUOTATION_TEMPLATE_DATA_ERROR_CODE;
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

        [HttpPost("DeleteQuotationTemplate")]
        public async Task<IActionResult> DeleteQuotationTemplate([FromBody] DeleteQuotationTemplateRequest deleteQuotationTemplateRequest, [FromHeader(Name = "userId")] int userId)
        {
            try
            {
                var existingQuotationTemplate = _context.QuotationTemp.Include(qt => qt.QuotationTempRecords)
                    .FirstOrDefault(p => p.TemplateId == deleteQuotationTemplateRequest.quotationTemplateId);
                if (existingQuotationTemplate != null)
                {
                    _context.QuotationTempRecord.RemoveRange(existingQuotationTemplate.QuotationTempRecords);

                    _context.QuotationTemp.Remove(existingQuotationTemplate);

                    await _context.SaveChangesAsync();

                    var success = new SuccessResponse
                    {

                        status = true,
                        data = new
                        {
                            message = Constants.SuccessMessages.QUOTATION_TEMPLATE_DELETED_MESSAGE
                        }

                    };
                    return Ok(success);
                }
                else
                {
                    throw new FailedToDeleteCustomer();
                }

            }
            catch (FailedToDeleteCustomer ex)
            {
                _logger.LogError($" Exception Message: {ex.Message}");
                _logger.LogError($" Exception Stack Trace: {ex.StackTrace}");
                errorResponse = new ErrorResponse();
                errorResponse.message = Constants.Errors.Messages.FAILED_TO_DELETE_QUOTATION_TEMPLATE_MESSAGE;
                errorResponse.code = Constants.Errors.Codes.FAILED_TO_DELETE_QUOTATION_TEMPLATE_DATA_ERROR_CODE;
                failureResponse = new FailureResponse();
                failureResponse.status = false;
                failureResponse.error = errorResponse;
                return BadRequest(failureResponse);
            }
            catch (Exception ex)
            {
                _logger.LogError($" Exception Message: {ex.Message}");
                _logger.LogError($" Exception Stack Trace: {ex.StackTrace}");
                return BadRequest(ex.Message);
            }
        }
   

    }

}


