using HospitalMgmtService.Database;
using HospitalMgmtService.RequestResponseModel.RequestModel;
using HospitalMgmtService.RequestResponseModel.ResponseModel;
using HospitalMgmtService.RequestResponseModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static HospitalMgmtService.RequestResponseModel.Constants.Errors;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using HospitalMgmtService.Model;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;


namespace HospitalMgmtService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpenseController : ControllerBase
    {
        private readonly DBContext _context;
        private readonly ILogger<ExpenseController> _logger;
        private SuccessResponse successResponse = new SuccessResponse();
        private FailureResponse failureResponse = new FailureResponse();
        private ErrorResponse errorResponse = new ErrorResponse();
        public ExpenseController(DBContext context , ILogger<ExpenseController> logger)
        {
            _context = context;
            _logger = logger;
        }



        [HttpPost("GetExpenses")]
        public async Task<IActionResult> GetExpenses([FromBody] GetExpenses getExpenses, [FromHeader(Name = "userId")] int userId)
        {
            try
            {
                var expenseQuery = _context.Expenses
                    .Select(x => new GetAllExpenses
                    {
                        expenseId = x.ExpenseId,
                        categoryName = x.ExpenseCategory.CategoryName,
                        amount = x.Amount,
                        expenseDate = x.Date,
                        expenseNote = x.Note,
                        createdAt = x.CreatedAt,
                        createdBy = x.CreatedBy
                    })
                    .AsQueryable();

                // Apply search filters based on searchByType
                switch (getExpenses.searchByType)
                {
                    case 1:
                        expenseQuery = expenseQuery.Where(e => EF.Functions.Like(e.categoryName, $"%{getExpenses.searchByValue}%"));
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

                // Apply date filters if both dates are provided
                if (getExpenses.fromtDate.HasValue && getExpenses.totDate.HasValue)
                {
                    DateTime startDate = getExpenses.fromtDate.Value;
                    DateTime endDate = getExpenses.totDate.Value.AddDays(1); // To include the end date

                    expenseQuery = expenseQuery.Where(e => e.createdAt >= startDate && e.createdAt < endDate);
                }

                var expenses = await expenseQuery.ToListAsync();

                var successResponse = new SuccessResponse
                {
                    status = true,
                    data = expenses
                };

                return Ok(successResponse);
            }
            catch (Exception ex)
            {
                _logger.LogError($" Exception Message: {ex.Message}");
                _logger.LogError($" Exception Stack Trace: {ex.StackTrace}");
                return BadRequest(ex.Message);
            }
        }


        [HttpPost("GetExpensesById")]
        public async Task<IActionResult> GetExpensesById(GetExpenseByIdRequest getExpenseById, [FromHeader(Name = "userId")] int userId)
        {
            try
            {

                var expense = await _context.Expenses
           .Where(p => p.ExpenseId == getExpenseById.expenseId)
          .Select(p => new GetAllExpenses
          {
              expenseId = p.ExpenseId,
              categoryId = p.ExpenseCategory.CategoryId,
              categoryName = p.ExpenseCategory.CategoryName,
              amount = p.Amount,
              expenseDate = p.Date,
              expenseNote = p.Note,
              createdAt = p.CreatedAt,
              createdBy = p.CreatedBy

          })
           .FirstOrDefaultAsync();

                var successResponse = new SuccessResponse();
                successResponse.status = true;
                successResponse.data = expense;
                return Ok(successResponse);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("AddExpense")]
        public async Task<IActionResult> AddExpense([FromBody] AddExpenseRequest addExpenseRequest, [FromHeader(Name = "userId")] int userId)
        {
            try
            {

                Expense newExpense = new Expense();
                newExpense.ExpenseCategoryIdFK = addExpenseRequest.expenseCategoryId;
                newExpense.Amount = addExpenseRequest.amount;
                newExpense.Note = addExpenseRequest.expenseNote;
                newExpense.Date = addExpenseRequest.expenseDate;
                newExpense.CreatedAt = DateTime.Now;
                newExpense.CreatedBy = userId;

                if (string.IsNullOrWhiteSpace(addExpenseRequest.expenseNote))
                {
                    errorResponse = new ErrorResponse();
                    errorResponse.message = Messages.EXPENSE_NOTE_CAN_NOT_BE_NULL_MESSAGE;
                    errorResponse.code = Codes.EXPENSE_NOTE_CAN_NOT_BE_NULL_ERROR_CODE;
                    failureResponse = new RequestResponseModel.FailureResponse();
                    failureResponse.status = false;
                    failureResponse.error = errorResponse;
                    return BadRequest(failureResponse);
                }

                else if (addExpenseRequest.expenseDate == null)
                {
                    errorResponse = new ErrorResponse();
                    errorResponse.message = Messages.EXPENSE_DATE_CAN_NOT_BE_NULL_MESSAGE;
                    errorResponse.code = Codes.EXPENSE_DATE_CAN_NOT_BE_NULL_ERROR_CODE;
                    failureResponse = new RequestResponseModel.FailureResponse();
                    failureResponse.status = false;
                    failureResponse.error = errorResponse;
                    return BadRequest(failureResponse);
                }

                else if (addExpenseRequest.amount == null)
                {
                    errorResponse = new ErrorResponse();
                    errorResponse.message = Messages.EXPENSE_AMOUNT_CAN_NOT_BE_NULL_MESSAGE;
                    errorResponse.code = Codes.EXPENSE_AMOUNT_CAN_NOT_BE_NULL_ERROR_CODE;
                    failureResponse = new RequestResponseModel.FailureResponse();
                    failureResponse.status = false;
                    failureResponse.error = errorResponse;
                    return BadRequest(failureResponse);
                }

                else if (addExpenseRequest.expenseCategoryId <= 0)
                {
                    errorResponse = new ErrorResponse();
                    errorResponse.message = Messages.EXPENSE_CATEGORY_ID_CAN_NOT_BE_NULL_MESSAGE;
                    errorResponse.code = Codes.EXPENSE_CATEGORY_ID_CAN_NOT_BE_NULL_ERROR_CODE;
                    failureResponse = new RequestResponseModel.FailureResponse();
                    failureResponse.status = false;
                    failureResponse.error = errorResponse;
                    return BadRequest(failureResponse);
                }

                _context.Expenses.Add(newExpense);
                _context.SaveChanges();


                //newExpense.ExpenseCategoryIdFK = addExpenseRequest.expenseCategoryId;
                //newExpense.Amount = addExpenseRequest.amount;
                //newExpense.Note = addExpenseRequest.expenseNote;
                //newExpense.Date = addExpenseRequest.expenseDate;
                //newExpense. CreatedAt = DateTime.Now;
                //newExpense. CreatedBy = userId;

                    
                //    _context.Expenses.Add(newExpense);
                //    _context.SaveChanges();

                Expense newExpenseId = _context.Expenses.Find(newExpense.ExpenseId);

                if (newExpense != null)
                {
                    successResponse.status = true;
                    successResponse.data = new
                    {
                        id = newExpense.ExpenseId,
                        message = Constants.SuccessMessages.EXPENSE_CATEGORY_SAVED_MESSAGE,

                    };
                    return Ok(successResponse);
                }
            
                
                else
                {
                    var errorResponse = new ErrorResponse();
                    errorResponse.message = Messages.FAILED_TO_SAVE_EXPENSE_MESSAGE;
                    errorResponse.code = Codes.FAILED_TO_SAVE_EXPENSE_ERROR_CODE;
                    failureResponse = new FailureResponse();
                    failureResponse.status = false;
                    failureResponse.error = errorResponse;
                    return BadRequest(failureResponse);

                }

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPost("UpdateExpense")]
        public async Task<IActionResult> UpdateExpense([FromBody] UpdateExpenseRequest updateExpenseRequest, [FromHeader(Name = "userId")] int userId)
        {
            try
            {
                var existingExpense = _context.Expenses.FirstOrDefault(p => p.ExpenseId == updateExpenseRequest.expenseId);


                if (existingExpense != null)
                {

                    existingExpense.ExpenseId = updateExpenseRequest.expenseId;
                    existingExpense.ExpenseCategoryIdFK = updateExpenseRequest.expenseCategoryId;
                    existingExpense.Amount = updateExpenseRequest.amount;
                    existingExpense.Date = updateExpenseRequest.expenseDate;
                    existingExpense.Note = updateExpenseRequest.expenseNote;
                    existingExpense.UpdatedAt = DateTime.Now;
                    existingExpense.UpdatedBy = userId;

                    await _context.SaveChangesAsync();

                    var success = new SuccessResponse
                    {

                        status = true,
                        data = new
                        {
                            message = Constants.SuccessMessages.EXPENSES_UPDATED_MESSAGE
                        }

                    };
                    return Ok(success);
                }
                else
                {
                    var errorResponse = new ErrorResponse();
                    errorResponse.message = Messages.FAILED_TO_UPDATED_EXPENSE_MESSAGE;
                    errorResponse.code = Codes.FAILED_TO_UPDATE_EXPENSE_ERROR_CODE;
                    failureResponse = new FailureResponse();
                    failureResponse.status = false;
                    failureResponse.error = errorResponse;
                    return BadRequest(failureResponse);
                }

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPost("DeleteExpense")]
        public async Task<IActionResult> DeleteExpense([FromBody] DeleteExpenseRequest deleteExpenseRequest, [FromHeader(Name = "userId")] int userId)
        {
            try
            {
                var existingExpense = _context.Expenses.FirstOrDefault(p => p.ExpenseId == deleteExpenseRequest.expenseId);
                if (existingExpense != null)
                {
                    _context.Expenses.Remove(existingExpense);

                    await _context.SaveChangesAsync();

                    var success = new SuccessResponse
                    {

                        status = true,
                        data = new
                        {
                            message = Constants.SuccessMessages.EXPENSES_DELETED_MESSAGE
                        }

                    };
                    return Ok(success);
                }
                else
                {
                    failureResponse = new FailureResponse()
                    {
                        status = false,
                        error = new ErrorResponse()
                        {
                            message = Constants.Errors.Messages.FAILED_TO_DELETED_EXPENSE_MESSAGE,
                            code = Constants.Errors.Codes.FAILED_TO_DELETE_EXPENSE_ERROR_CODE
                        }
                    };
                    return BadRequest(failureResponse);
                }

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
