using HospitalMgmtService.Database;
using HospitalMgmtService.Model;
using HospitalMgmtService.RequestResponseModel;
using HospitalMgmtService.RequestResponseModel.RequestModel;
using HospitalMgmtService.RequestResponseModel.ResponseModel;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
    public class BrandController : ControllerBase
    {
        private readonly DBContext _context;
        private SuccessResponse successResponse = new SuccessResponse();
        private FailureResponse failureResponse = new FailureResponse();
        private ErrorResponse errorResponse = new ErrorResponse();
        private readonly ILogger<BrandController> _logger;

        public BrandController(DBContext context, ILogger<BrandController> logger)
        {
            _context = context;
            _logger = logger;
            _logger.LogDebug("Log Integrated to Brand Controller");
        }
        [HttpPost("GetBrands")]
        public async Task<IActionResult> GetBrands([FromBody] GetBrands getBrands, [FromHeader(Name = "userId")] int userId)
        {
            try
            {
                _logger.LogInformation("GetBrands action is called.");

                var brandQuery = _context.Brand
                    .Select(x => new GetBrandResponse
                    {
                        brandId = x.BrandId,
                        brandName = x.BrandName,
                        description = x.Description,
                        createdAt = x.CreatedAt,
                        createdBy = x.CreatedBy
                    })
                    .AsQueryable();
                if (getBrands.searchByType == 1)
                {
                    brandQuery = brandQuery.Where(b => b.brandName.Contains(getBrands.searchByValue));

                }

                else
                {

                    throw new InvalidSearchType();
                }

                var brands = await brandQuery.ToListAsync();
                var successResponse = new SuccessResponse();
                successResponse.status = true;
                successResponse.data = brands;
                return Ok(successResponse);
            }

            catch (InvalidSearchType e)
            {
                _logger.LogError(e.Message);

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
                return BadRequest(ex.Message);
            }
        }




        [HttpPost("GetBrandById")]
        public async Task<IActionResult> GetBrandById(GetBrandByIdRequest getBrandByIdRequest, [FromHeader(Name = "userId")] int userId)
        {
            try
            {
                _logger.LogInformation("Get brand by Id started executing");
                var brand = await _context.Brand
                 .Where(p => p.BrandId == getBrandByIdRequest.BrandId)
                 .Select(p => new GetBrandResponse
                 {
                     brandId = p.BrandId,
                     brandName = p.BrandName,
                     description = p.Description,
                     createdAt = p.CreatedAt,
                     createdBy = p.CreatedBy


                 }).FirstOrDefaultAsync();


                if (brand != null)
                {
                    var successResponse = new SuccessResponse();

                    successResponse.status = true;
                    successResponse.data = brand;
                    return Ok(successResponse);
                }
                else
                {
                    throw new BrandIdDoesnotExists();

                }

            }
            catch (BrandIdDoesnotExists e)
            {
                _logger.LogError($" Exception Message: {e.Message}");
                _logger.LogError($" Exception Stack Trace: {e.StackTrace}");
                errorResponse = new ErrorResponse();
                errorResponse.message = Constants.Errors.Messages.BRAND_ID_DOES_NOT_EXISTS_MESSAGE;
                errorResponse.code = Constants.Errors.Codes.BRAND_ID_DOES_NOT_EXISTS_ERROR_CODE;
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
        [HttpPost("AddBrand")]
        public async Task<IActionResult> AddBrand([FromBody] AddBrandRequest addBrand, [FromHeader(Name = "userId")] int userId)
        {
            try
            {
                _logger.LogInformation("Add Brand is executing");
                var existingBrand = _context.Brand.FirstOrDefault(p => p.BrandName == addBrand.brandName);
                if (existingBrand != null)
                {
                    throw new BrandAlreadyExists();

                }
                if (existingBrand == null)
                {
                    if (string.IsNullOrWhiteSpace(addBrand.brandName))
                    {
                        throw new BrandNameCannotBeBlank();
                    }
                    var newBrand = new Brand();


                    newBrand.BrandName = addBrand.brandName;
                    newBrand.Description = addBrand.description;
                    newBrand.CreatedBy = userId;
                    newBrand.CreatedAt = DateTime.Now;

                    _context.Brand.Add(newBrand);
                    var customer = _context.SaveChanges();
                    var successResponse = new SuccessResponse();
                    existingBrand = _context.Brand.Find(newBrand.BrandId);

                    successResponse.status = true;
                    successResponse.data = new
                    {
                        id = newBrand.BrandId,
                        message = Constants.SuccessMessages.BRAND_SAVED_MESSAGE,

                    };
                    return Ok(successResponse);
                }
                else
                {
                    throw new FailedToSaveBrand();
                }


            }
            catch (BrandAlreadyExists e)
            {

                _logger.LogError($" Exception Message: {e.Message}");
                _logger.LogError($" Exception Stack Trace: {e.StackTrace}");
                errorResponse = new ErrorResponse();
                errorResponse.message = Constants.Errors.Messages.BRAND_ALREADY_EXISTS_MESSAGE;
                errorResponse.code = Constants.Errors.Codes.BRAND_NAME_ALREADY_EXIST_ERROR_CODE;
                failureResponse = new FailureResponse();
                failureResponse.status = false;
                failureResponse.error = errorResponse;
                return Conflict(failureResponse);
            }
            catch (BrandNameCannotBeBlank e)
            {

                _logger.LogError($" Exception Message: {e.Message}");
                _logger.LogError($" Exception Stack Trace: {e.StackTrace}");
                errorResponse = new ErrorResponse();
                errorResponse.message = Constants.Errors.Messages.BRANDNAME_CAN_NOT_BE_BLANK_MESSAGE;
                errorResponse.code = Constants.Errors.Codes.BRAND_NAME_CAN_NOT_BE_BLANK_ERROR_CODE;
                failureResponse = new FailureResponse();
                failureResponse.status = false;
                failureResponse.error = errorResponse;
                return NotFound(failureResponse);
            }
            catch (FailedToSaveBrand e)
            {
                _logger.LogError($" Exception Message: {e.Message}");
                _logger.LogError($" Exception Stack Trace: {e.StackTrace}");
                errorResponse = new ErrorResponse();
                errorResponse.message = Constants.Errors.Messages.FAILED_TO_SAVE_BRAND_MESSAGE;
                errorResponse.code = Constants.Errors.Codes.FAILED_TO_SAVE_BRAND_ERROR_CODE;
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



        [HttpPost("UpdateBrand")]
        public async Task<IActionResult> UpdateBrand([FromBody] UpdateBrandRequest updateBrandRequest, [FromHeader(Name = "userId")] int userId)
        {
            try
            {
                _logger.LogInformation("Update Brand is executing");

                var existingBrand = _context.Brand.FirstOrDefault(p => p.BrandId == updateBrandRequest.brandId);


                if (existingBrand != null)
                {
                    if (string.IsNullOrWhiteSpace(updateBrandRequest.brandName))
                    {
                        throw new BrandNameCannotBeBlank();
                    }

                    existingBrand.BrandId = updateBrandRequest.brandId;
                    existingBrand.BrandName = updateBrandRequest.brandName;
                    existingBrand.Description = updateBrandRequest.description;
                    existingBrand.UpdatedAt = DateTime.Now;
                    existingBrand.UpdatedBy = userId;
                    await _context.SaveChangesAsync();

                    var successResponse = new SuccessResponse();


                    successResponse.status = true;
                    successResponse.data = new
                    {
                        message = Constants.SuccessMessages.BRAND_UPDATED_MESSAGE
                    };
                    return Ok(successResponse);
                }
                else
                {
                    throw new FailedToUpdateBrand();

                }

            }
            catch (FailedToUpdateBrand ex)
            {

                _logger.LogError($" Exception Message: {ex.Message}");
                _logger.LogError($" Exception Stack Trace: {ex.StackTrace}");
                errorResponse = new ErrorResponse();
                errorResponse.message = Constants.Errors.Messages.FAILED_TOUPDATE_BRAND_MESSAGE;
                errorResponse.code = Constants.Errors.Codes.FAILED_TO_UPDATE_BRAND_ERROR_CODE;
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


        [HttpPost("DeleteBrand")]
        public async Task<IActionResult> DeleteBrand([FromBody] DeleteBrandRequest deleteBrandRequest, [FromHeader(Name = "userId")] int userId)
        {
            try
            {
                _logger.LogInformation("Delete Brand is executing");
                var existingBrand = _context.Brand.FirstOrDefault(p => p.BrandId == deleteBrandRequest.brandId);


                if (existingBrand != null)
                {
                    _context.Brand.Remove(existingBrand);

                    await _context.SaveChangesAsync();

                    var successResponse = new SuccessResponse();


                    successResponse.status = true;
                    successResponse.data = new
                    {
                        message = Constants.SuccessMessages.BRAND_DELETED_MESSAGE
                    };
                    return Ok(successResponse);
                }
                else
                {
                    throw new FailedToDeleteBrand();
                }

            }
            catch (BrandIdCannotBeBlank e)
            {

                _logger.LogError($" Exception Message: {e.Message}");
                _logger.LogError($" Exception Stack Trace: {e.StackTrace}");
                errorResponse = new ErrorResponse();
                errorResponse.message = Constants.Errors.Messages.BRANDNAME_CAN_NOT_BE_BLANK_MESSAGE;
                errorResponse.code = Constants.Errors.Codes.BRAND_ID_CAN_NOT_BE_BLANK;
                failureResponse = new FailureResponse();
                failureResponse.status = false;
                failureResponse.error = errorResponse;
                return NotFound(failureResponse);
            }
            catch (FailedToDeleteBrand ex)
            {

                _logger.LogError($" Exception Message: {ex.Message}");
                _logger.LogError($" Exception Stack Trace: {ex.StackTrace}");
                errorResponse = new ErrorResponse();
                errorResponse.message = Constants.Errors.Messages.FAILED_TO_DELETE_BRAND_MESSAGE;
                errorResponse.code = Constants.Errors.Codes.FAILED_TO_DELETE_BRAND_ERROR_CODE;
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












