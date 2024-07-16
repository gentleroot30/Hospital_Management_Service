using HospitalMgmtService.Database;
using HospitalMgmtService.Model;
using HospitalMgmtService.RequestResponseModel;
using HospitalMgmtService.RequestResponseModel.RequestModel;
using HospitalMgmtService.RequestResponseModel.ResponseModel;
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
    public class CustomerCategoryController : ControllerBase
    {
        private readonly DBContext _context;
        private SuccessResponse successResponse = new SuccessResponse();
        private FailureResponse failureResponse = new FailureResponse();
        private ErrorResponse errorResponse = new ErrorResponse();
        private readonly ILogger<CustomerCategoryController> _logger;

        public CustomerCategoryController(DBContext context, ILogger<CustomerCategoryController> logger)
        {
            _context = context;
            _logger = logger;

        }

        [HttpPost("GetCustomercategories")]
        public async Task<IActionResult> GetCustomercategories([FromBody] GetCustomerCategoriesRequest getCategories, [FromHeader(Name ="userId")] int userId)
        {
            try
            {
                var categoryQuery = _context.CustomerCategory.Select(x => new GetCustomerCategoryResponse
                {
                    categoryId = x.CustomerCategoryId,
                    categoryName = x.CategoryName,
                    description = x.Description,
                    createdAt = x.CreatedAt,
                    createdBy = x.CreatedBy
                }).AsQueryable();


                if (getCategories.searchByType == 1)
                {
                    categoryQuery = categoryQuery.Where(category => category.categoryName.Contains(getCategories.searchByValue));

                    var categories = await categoryQuery.ToListAsync();

                    var successResponse = new SuccessResponse();
                    successResponse.status = true;
                    successResponse.data = categories;
                    return Ok(successResponse);
                }

                else
                {
                    throw new InvalidSearchType();
                }
            }



            catch (InvalidSearchType ex)
            {

                _logger.LogError($" Exception Message: {ex.Message}");
                _logger.LogError($" Exception Stack Trace: {ex.StackTrace}");
                errorResponse = new ErrorResponse();
                errorResponse.message = Messages.INVALID_SEARCH_TYPE_MESSAGE;
                errorResponse.code = Codes.INVALID_SEARCHTYPE_ERROR_CODE;
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
        }

        [HttpPost("GetCustomerCategoryById")]
        public async Task<IActionResult> GetProductById([FromBody] GetCustomerCategoryByIdRequest getCategoryById, [FromHeader(Name = "userId")] int userId)
        {
            try
            {
                var category = await _context.CustomerCategory
            .Where(p => p.CustomerCategoryId == getCategoryById.categoryId)
            .Select(p => new GetCustomerCategoryResponse
            {
                categoryId = getCategoryById.categoryId,
                categoryName = p.CategoryName,
                description = p.Description,
                createdAt = p.CreatedAt,
                createdBy = p.CreatedBy
            })
            .FirstOrDefaultAsync();

                if (category != null)
                {
                    var successResponse = new SuccessResponse();

                    successResponse.status = true;
                    successResponse.data = category;
                    return Ok(successResponse);
                }
                else
                {
                    throw new CustomerCategoryIdDoesnotExists();
                }


            }

            catch (CustomerCategoryIdDoesnotExists ex)
            {

                _logger.LogError($" Exception Message: {ex.Message}");
                _logger.LogError($" Exception Stack Trace: {ex.StackTrace}");
                errorResponse = new ErrorResponse();
                errorResponse.message = Constants.Errors.Messages.CUSTOMER_CATEGORY_ID_DOES_NOT_EXISTS;
                errorResponse.code = Constants.Errors.Codes.CUSTOMER_CATEGORY_ID_DOES_NOT_EXISTS_ERROR_CODE;
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
        [HttpPost("AddCustomerCategoy")]
        public async Task<IActionResult> AddCustomerCategoy( [FromBody]AddCustomerCategoryRequest customerCategoryRequest , [FromHeader(Name = "userId")] int userId)
        {
            try
            {
                var existingCustomerCategory = _context.CustomerCategory.FirstOrDefault(p => p.CategoryName == customerCategoryRequest.categoryName);

                if (existingCustomerCategory == null)
                {
                    if (string.IsNullOrWhiteSpace(customerCategoryRequest.categoryName))
                    {
                        throw new CustomerCategoryNameCannotBeBlank();
                    }

                    var newCustomerCategory = new CustomerCategory();
                    newCustomerCategory.CategoryName = customerCategoryRequest.categoryName;
                    newCustomerCategory.Description = customerCategoryRequest.description;
                    newCustomerCategory.CreatedBy = userId;
                    newCustomerCategory.CreatedAt = DateTime.Now;

                    _context.CustomerCategory.Add(newCustomerCategory);
                    var category = _context.SaveChanges();
                    existingCustomerCategory = _context.CustomerCategory.Find(newCustomerCategory.CustomerCategoryId);


                    if (category <= 0)
                    {

                        throw new CustomerCategoryAlreadyExists();                    }
                    else
                    {
                        successResponse.status = true;
                        successResponse.data = new
                        {
                            id = newCustomerCategory.CustomerCategoryId,
                            message = Constants.SuccessMessages.CUSTOMER_CATEGORY_SAVED_MESSAGE,

                        };
                        return Ok(successResponse);
                    }
                }
                else
                {
                    throw new FailedToSaveCustomerCategory();
                }
            }
            catch(CustomerCategoryAlreadyExists ex)
            {

                _logger.LogError($" Exception Message: {ex.Message}");
                _logger.LogError($" Exception Stack Trace: {ex.StackTrace}");
                errorResponse = new ErrorResponse();
                errorResponse.message = Messages.CUSTOMER_CATEGORY_EXIST_MESSAGE;
                errorResponse.code = Codes.CUSTOMER_CATEGORY_EXIST_ERROR_CODE;
                failureResponse = new RequestResponseModel.FailureResponse();
                failureResponse.status = false;
                failureResponse.error = errorResponse;
                return Conflict(failureResponse);
            }
            catch(FailedToSaveCustomerCategory ex)
            {


                _logger.LogError($" Exception Message: {ex.Message}");
                _logger.LogError($" Exception Stack Trace: {ex.StackTrace}");
                errorResponse = new RequestResponseModel.ErrorResponse();
                errorResponse.message = Constants.Errors.Messages.FAILED_TO_SAVE_CUSTOMER_CATEGORY_MESSAGE;
                errorResponse.code = Constants.Errors.Codes.FAILED_TO_SAVE_CUSTOMER_CATEGORY_ERROR_CODE;
                failureResponse = new RequestResponseModel.FailureResponse();
                failureResponse.status = false;
                failureResponse.error = errorResponse;
                return BadRequest(failureResponse);

            }
            catch (CustomerCategoryNameCannotBeBlank ex)
            {


                _logger.LogError($" Exception Message: {ex.Message}");
                _logger.LogError($" Exception Stack Trace: {ex.StackTrace}");
                errorResponse = new RequestResponseModel.ErrorResponse();
                errorResponse.message = Constants.Errors.Messages.CUSTOMER_CATEGORY_NAME_CAN_NOT_BE_BLANK_MESSAGE;
                errorResponse.code = Constants.Errors.Codes.CUSTOMER_CATEGORY_NAME_CAN_NOT_BE_BLANK_CODE;
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
           
        }

        [HttpPost("UpdateCustomerCategory")]
        public async Task<IActionResult> UpdateCustomerCategory([FromBody]UpdateCustomerCategory updateCustomerCategory, [FromHeader(Name = "userId")] int userId)
        {
            try
            {
                var existingCustomerCategory = _context.CustomerCategory.FirstOrDefault(p => p.CustomerCategoryId == updateCustomerCategory.categoryId);

                if (existingCustomerCategory != null)
                {
                    if (string.IsNullOrWhiteSpace(updateCustomerCategory.categoryName))
                    {
                        throw new CustomerCategoryNameCannotBeBlank();
                    }

                    existingCustomerCategory.CustomerCategoryId = updateCustomerCategory.categoryId;
                    existingCustomerCategory.CategoryName = updateCustomerCategory.categoryName;
                    existingCustomerCategory.Description = updateCustomerCategory.description;
                    existingCustomerCategory.UpdatedBy = userId;
                    existingCustomerCategory.UpdatedAt = DateTime.Now;
                    await _context.SaveChangesAsync();

                    var successResponse = new SuccessResponse();
                    successResponse.status = true;
                    successResponse.data = new
                    {
                        message = Constants.SuccessMessages.CUSTOMER_CATEGORY_UPDATED_MESSAGE
                    };
                    return Ok(successResponse);
                }
                else
                {
                    throw new FailedToUpdateCustomerCategory();
                }
            }
            catch (CustomerCategoryNameCannotBeBlank ex)
            {


                _logger.LogError($" Exception Message: {ex.Message}");
                _logger.LogError($" Exception Stack Trace: {ex.StackTrace}");
                errorResponse = new RequestResponseModel.ErrorResponse();
                errorResponse.message = Constants.Errors.Messages.CUSTOMER_CATEGORY_NAME_CAN_NOT_BE_BLANK_MESSAGE;
                errorResponse.code = Constants.Errors.Codes.CUSTOMER_CATEGORY_NAME_CAN_NOT_BE_BLANK_CODE;
                failureResponse = new RequestResponseModel.FailureResponse();
                failureResponse.status = false;
                failureResponse.error = errorResponse;
                return BadRequest(failureResponse);

            }
            catch (FailedToUpdateCustomerCategory ex)
            {


                _logger.LogError($" Exception Message: {ex.Message}");
                _logger.LogError($" Exception Stack Trace: {ex.StackTrace}");
                errorResponse = new ErrorResponse();
                errorResponse.message = Messages.FAILED_TO_UPDATED_CUSTOMER_CATEGORY_MESSAGE;
                errorResponse.code = Codes.FAILED_TO_UPDATE_CUSTOMER_CATEGORY_ERROR_CODE;
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
           
        }

        [HttpPost("DeleteCustomerCategory")]
        public async Task<IActionResult> DeleteProductCategory([FromBody] DeleteCustomerCategoryRequest deleteCustomerCategoryRequest, [FromHeader(Name = "userId")] int userId)
        {

            try
            {
                var existingCustomerCategory = _context.CustomerCategory.FirstOrDefault(p => p.CustomerCategoryId == deleteCustomerCategoryRequest.categoryId);


                if (existingCustomerCategory != null)
                {
                    _context.CustomerCategory.Remove(existingCustomerCategory);

                    await _context.SaveChangesAsync();

                    var successResponse = new SuccessResponse();
                    {

                        successResponse.status = true;
                        successResponse.data = new
                        {
                            message = Constants.SuccessMessages.CUSTOMER_CATEGORY_DELETED_MESSAGE
                        };
                        return Ok(successResponse);
                    }
                }
                else
                {
                    throw new FailedToDeleteCustomerCategory();
                
                }

            }
            catch(FailedToDeleteCustomerCategory ex)
            {


                _logger.LogError($" Exception Message: {ex.Message}");
                _logger.LogError($" Exception Stack Trace: {ex.StackTrace}");
                errorResponse = new ErrorResponse();
                errorResponse.message = Messages.FAILED_TO_DELETED_CUSTOMER_CATEGORY_MESSAGE;
                errorResponse.code = Codes.FAILED_TO_DELETE_CUSTOMER_CATEGORY_ERROR_CODE;
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

        }
    }
}












