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
    public class ExpenseCategoryController : ControllerBase
    {
        private readonly DBContext _context;
        private SuccessResponse successResponse = new SuccessResponse();
        private FailureResponse failureResponse = new FailureResponse();
        private ErrorResponse errorResponse = new ErrorResponse(); 
        private readonly ILogger<CustomerController> _logger;

        public ExpenseCategoryController(DBContext context, ILogger<CustomerController> logger)
        {
            _context = context;
        }

        [HttpPost("GetExpenseCategories")]
        public async Task<IActionResult> GetCustomercategories([FromBody] GetExpenseCategoriesRequest getExpenseCategoriesRequest, [FromHeader(Name = "userId")] int userId)
        {
            try
            {
                var categoryQuery = _context.ExpenseCategory.Select(x => new GetExpenseAllCategoryResponse
                {
                    categoryId = x.CategoryId,
                    categoryName = x.CategoryName,
                    description = x.Description,
                    createdAt = x.CreatedAt,
                    createdBy = x.CreatedBy
                }).AsQueryable();


                if (getExpenseCategoriesRequest.searchByType == 1)
                {
                    categoryQuery = categoryQuery.Where(category => category.categoryName.Contains(getExpenseCategoriesRequest.searchByValue));

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

        [HttpPost("GetExpenseCategoryById")]
        public async Task<IActionResult> GetProductById([FromBody] GetExpenseCategoryByIdRequest getCategoryById, [FromHeader(Name = "userId")] int userId)
        {
            try {
                var category = await _context.ExpenseCategory
                .Where(p => p.CategoryId == getCategoryById.categoryId)
                .Select(p => new GetExpenseAllCategoryResponse
                {
                    categoryId = p.CategoryId,
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
                    throw new ExpenseCategoryIdDoesnotExists();
                }

            }
            catch(ExpenseCategoryIdDoesnotExists ex)
            {
                _logger.LogError($" Exception Message: {ex.Message}");
                _logger.LogError($" Exception Stack Trace: {ex.StackTrace}");
                errorResponse = new ErrorResponse();


                errorResponse.message = Constants.Errors.Messages.EXPENSE_CATEGORY_ID_DOES_NOT_EXISTS;
                errorResponse.code = Constants.Errors.Codes.EXPENSE_CATEGORY_ID_DOES_NOT_EXISTS_ERROR_CODE;
                failureResponse = new RequestResponseModel.FailureResponse();
                failureResponse.status = false;
                failureResponse.error = errorResponse;
                return NotFound(failureResponse);
            }
            
            catch(Exception ex)
            {

                _logger.LogError($" Exception Message: {ex.Message}");
                _logger.LogError($" Exception Stack Trace: {ex.StackTrace}");
                return BadRequest(ex.Message);
            }
            } 
        [HttpPost("AddExpenseCategory")]
        public async Task<IActionResult> AddExpenseCategory([FromBody] AddExpenseCategoryRequest addExpenseCategoryRequest, [FromHeader(Name = "userId")] int userId)
        {
            try
            {
                var existingExpenseCategory = _context.ExpenseCategory.FirstOrDefault(p => p.CategoryName == addExpenseCategoryRequest.categoryName);

                if (existingExpenseCategory == null)
                {
                    if (string.IsNullOrWhiteSpace(addExpenseCategoryRequest.categoryName))
                    {
                        throw new ExpenseCategoryCannotBeBlank();
                    }


                    ExpenseCategory newExpenseCategory = new ExpenseCategory();


                    newExpenseCategory.CategoryName = addExpenseCategoryRequest.categoryName;
                    newExpenseCategory.Description = addExpenseCategoryRequest.description;
                    newExpenseCategory.CreatedBy = userId;
                    newExpenseCategory.CreatedAt = DateTime.Now;


                    _context.ExpenseCategory.Add(newExpenseCategory);
                    var category = _context.SaveChanges();


                    if (category <= 0)
                    {
                        errorResponse = new ErrorResponse();
                        errorResponse.message = Messages.EXPENSE_CATEGORY_EXIST_MESSAGE;
                        errorResponse.code = Codes.EXPENSE_CATEGORY_EXIST_ERROR_CODE;
                        failureResponse = new RequestResponseModel.FailureResponse();
                        failureResponse.status = false;
                        failureResponse.error = errorResponse;
                        return Conflict(failureResponse);

                    }
                    else
                    {
                        successResponse.status = true;
                        successResponse.data = new
                        {
                            id = newExpenseCategory.CategoryId,
                            message = Constants.SuccessMessages.EXPENSE_CATEGORY_SAVED_MESSAGE,

                        };
                        return Ok(successResponse);
                    }

                }
                else
                {
                    throw new FailedToSaveExpensecategory();
                }
            }
            catch(ExpenseCategoryCannotBeBlank ex)
            {

                _logger.LogError($" Exception Message: {ex.Message}");
                _logger.LogError($" Exception Stack Trace: {ex.StackTrace}");
                errorResponse = new ErrorResponse();
                errorResponse.message = Messages.EXPENSE_CATEGORY_NAME_CAN_NOT_BE_BLANK_MESSAGE;
                errorResponse.code = Codes.CUSTOMER_CATEGORY_NAME_CAN_NOT_BE_BLANK_CODE;
                failureResponse = new RequestResponseModel.FailureResponse();
                failureResponse.status = false;
                failureResponse.error = errorResponse;
                return BadRequest(failureResponse);
            }
            catch(FailedToSaveExpensecategory ex)
            {
                _logger.LogError($" Exception Message: {ex.Message}");
                _logger.LogError($" Exception Stack Trace: {ex.StackTrace}");
                errorResponse = new RequestResponseModel.ErrorResponse();
                errorResponse.message = Constants.Errors.Messages.FAILED_TO_SAVE_EXPENSE_CATEGORY_MESSAGE;
                errorResponse.code = Constants.Errors.Codes.FAILED_TO_DELETE_EXPENSE_CATEGORY_ERROR_CODE;
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

        [HttpPost("UpdateExpenseCategory")]
        public async Task<IActionResult> UpdateExpenseCategory([FromBody] UpdateExpenseCategoryRequest updateExpenseCategoryRequest, [FromHeader(Name = "userId")] int userId)
        {
            try
            {
                var existingExpenseCategory = _context.ExpenseCategory.FirstOrDefault(p => p.CategoryId == updateExpenseCategoryRequest.categoryId);

                if (existingExpenseCategory != null)
                {
                    if (string.IsNullOrWhiteSpace(updateExpenseCategoryRequest.categoryName))
                    {
                        throw new ExpenseCategoryCannotBeBlank();
                    }

                    existingExpenseCategory.CategoryId = updateExpenseCategoryRequest.categoryId;
                    existingExpenseCategory.CategoryName = updateExpenseCategoryRequest.categoryName;
                    existingExpenseCategory.Description = updateExpenseCategoryRequest.description;
                    existingExpenseCategory.UpdatedBy = userId;
                    existingExpenseCategory.UpdatedAt = DateTime.Now;
                    await _context.SaveChangesAsync();

                    var successResponse = new SuccessResponse();
                    successResponse.status = true;
                    successResponse.data = new
                    {
                        message = Constants.SuccessMessages.EXPENSE_CATEGORY_UPDATED_MESSAGE
                    };
                    return Ok(successResponse);
                }
                else
                {
                    throw new FailedToUpdateExpenseCategory();
                }
            }
            catch(ExpenseCategoryCannotBeBlank ex)
            {

                _logger.LogError($" Exception Message: {ex.Message}");
                _logger.LogError($" Exception Stack Trace: {ex.StackTrace}");
                errorResponse = new ErrorResponse();
                errorResponse.message = Messages.EXPENSE_CATEGORY_NAME_CAN_NOT_BE_BLANK_MESSAGE;
                errorResponse.code = Codes.EXPENSE_CATEGORY_NAME_CAN_NOT_BE_BLANK_CODE;
                failureResponse = new RequestResponseModel.FailureResponse();
                failureResponse.status = false;
                failureResponse.error = errorResponse;
                return BadRequest(failureResponse);
            }
           
            catch(FailedToUpdateExpenseCategory ex)
            {

                _logger.LogError($" Exception Message: {ex.Message}");
                _logger.LogError($" Exception Stack Trace: {ex.StackTrace}");
                errorResponse = new ErrorResponse();
                errorResponse.message = Messages.FAILED_TO_UPDATED_EXPENSE_CATEGORY_MESSAGE;
                errorResponse.code = Codes.FAILED_TO_UPDATE_EXPENSE_CATEGORY_ERROR_CODE;
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

        [HttpPost("DeleteExpenseCategory")]
        public async Task<IActionResult> DeleteExpenseCategory([FromBody] DeleteExpenseCategoryRequest deleteExpenseCategory, [FromHeader(Name = "userId")] int userId)
        {
            try {
                var existingExpenseCategory = _context.ExpenseCategory.FirstOrDefault(p => p.CategoryId == deleteExpenseCategory.categoryId);


                if (existingExpenseCategory != null)
                {
                    _context.ExpenseCategory.Remove(existingExpenseCategory);

                    await _context.SaveChangesAsync();

                    var successResponse = new SuccessResponse();
                    {

                        successResponse.status = true;
                        successResponse.data = new
                        {
                            message = Constants.SuccessMessages.EXPENSE_CATEGORY_DELETED_MESSAGE
                        };
                        return Ok(successResponse);
                    }
                }
                else
                {
                    throw new FailedToDeleteExpenseCategory();
                }

            }
            catch(FailedToDeleteExpenseCategory ex)
            {

                _logger.LogError($" Exception Message: {ex.Message}");
                _logger.LogError($" Exception Stack Trace: {ex.StackTrace}");
                errorResponse = new ErrorResponse();
                errorResponse.message = Messages.FAILED_TO_DELETED_EXPENSE_CATEGORY_MESSAGE;
                errorResponse.code = Codes.FAILED_TO_DELETE_EXPENSE_CATEGORY_ERROR_CODE;
                failureResponse = new RequestResponseModel.FailureResponse();
                failureResponse.status = false;
                failureResponse.error = errorResponse;
                return BadRequest(failureResponse);
            }
            
            
            catch(Exception ex) {

                _logger.LogError($" Exception Message: {ex.Message}");
                _logger.LogError($" Exception Stack Trace: {ex.StackTrace}");
                return BadRequest(ex.Message);

            }
            }
    }
}
