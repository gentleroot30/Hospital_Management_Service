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
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using static HospitalMgmtService.Controllers.CustomExceptions;
using static HospitalMgmtService.RequestResponseModel.Constants.Errors;

namespace HospitalMgmtService.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly DBContext _context;
        private SuccessResponse successResponse = new SuccessResponse();
        private FailureResponse failureResponse = new FailureResponse();
        private ErrorResponse errorResponse = new ErrorResponse();
        private object customerRequest;
        private readonly ILogger<CustomerController> _logger;


        public CustomerController(DBContext context, ILogger<CustomerController> logger)
        {
            _context = context;
            _logger = logger;

        }
        [HttpPost("GetCustomers")]
        public async Task<IActionResult> GetCustomers([FromBody] GetCustomerCategoriesRequest getCustomers, [FromHeader(Name = "userId")] int userId)
        {
            try
            {
                var customerQuery = _context.Customers.Select(p => new GetCustomerResponse
                {
                    customerId = p.CustomerId,
                    customerName = p.CustomerName,
                    customerCode = p.CustomerCode,
                    ethnicity = p.Ethnicity,
                    customField_1 = p.CustomField_1,
                    customField_2 = p.CustomField_2,
                    contactNo_1 = p.ContactNo_1,
                    contactNo_2 = p.ContactNo_2,
                    contactNo_3 = p.ContactNo_3,
                    address = p.Address,
                    categoryId = p.CustomerCategoryIdFk,
                    categoryName = p.CustomerCategory.CategoryName,
                    createdAt = p.CreatedAt,
                    createdBy = p.CreatedBy
                }).AsQueryable();

                // Apply date filters if both dates are provided
                if (getCustomers.fromtDate.HasValue && getCustomers.totDate.HasValue)
                {
                    DateTime startDate = getCustomers.fromtDate.Value;
                    DateTime endDate = getCustomers.totDate.Value.AddDays(1); // To include the end date

                    customerQuery = customerQuery.Where(p => p.createdAt >= startDate && p.createdAt < endDate);
                }

                switch (getCustomers.searchByType)
                {
                    case 1:
                        customerQuery = customerQuery.Where(customer => customer.customerName.Contains(getCustomers.searchByValue));
                        break;
                    case 2:
                        customerQuery = customerQuery.Where(customer => customer.customerCode.Contains(getCustomers.searchByValue));
                        break;
                    case 3:
                        customerQuery = customerQuery.Where(customer => customer.contactNo_1.Contains(getCustomers.searchByValue));
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

                var customers = await customerQuery.ToListAsync();

                // Check if no customers found and return a message
                if (customers.Count == 0)
                {
                    var noDataResponse = new SuccessResponse
                    {
                        status = true,
                        data = customers,
                        
                    };
                    return Ok(noDataResponse);
                }

                var successResponse = new SuccessResponse
                {
                    status = true,
                    data = customers
                };
                return Ok(successResponse);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }




        [HttpPost("GetCustomerById")]
        public async Task<IActionResult> GetCustomerById(GetCustomerByIdRequest customerById, [FromHeader(Name = "userId")] int userId)
        {
            try
            {

                var customer = await _context.Customers
           .Where(p => p.CustomerId == customerById.customerId)
          .Select(p => new GetCustomerResponse
          {
              customerId = p.CustomerId,
              categoryId = p.CustomerCategoryIdFk,
              categoryName = p.CustomerCategory.CategoryName,
              customerCode = p.CustomerCode,
              customerName = p.CustomerName,
              ethnicity = p.Ethnicity,
              address = p.Address,
              customField_1 = p.CustomField_1,
              customField_2 = p.CustomField_2,
              contactNo_1 = p.ContactNo_1,
              contactNo_2 = p.ContactNo_2,
              contactNo_3 = p.ContactNo_3,
              createdAt = p.CreatedAt,
              createdBy = p.CreatedBy
          })
           .FirstOrDefaultAsync();
                if (customer == null)
                {
                    throw new CustomerIdDoesnotExists();
                }
                else
                {
                    var successResponse = new SuccessResponse();
                    successResponse.status = true;
                    successResponse.data = customer;
                    return Ok(successResponse);
                }
            }
            catch (CustomerIdDoesnotExists ex)
            {
                _logger.LogError($" Exception Message: {ex.Message}");
                _logger.LogError($" Exception Stack Trace: {ex.StackTrace}");
                errorResponse = new ErrorResponse();
                errorResponse.message = Constants.Errors.Messages.CUSTOMER_ID_DOES_NOT_EXISTS_MESSAGE;
                errorResponse.code = Constants.Errors.Codes.CUSTOMER_ID_DOES_NOT_EXISTS_ERROR_CODE;
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

        [HttpPost("AddCustomer")]
        public async Task<IActionResult> AddCustomer([FromBody] AddCustomerRequest customerRequest, [FromHeader(Name = "userId")] int userId)
        {
            try
            {
                var existingCustomer = _context.Customers.FirstOrDefault(p => p.ContactNo_1 == customerRequest.contactNo_1);

                if (existingCustomer == null)
                {
                    if (string.IsNullOrWhiteSpace(customerRequest.customerName))
                    {
                        throw new CustomerNameCannotBeBlank();
                    }
                    CustomerCategory existingCategory = _context.CustomerCategory.FirstOrDefault(p => p.CustomerCategoryId == customerRequest.categoryId);
                    if (existingCategory == null)
                    {
                        throw new CustomerCategoryIdDoesnotExists();
                    }
                    var newCustomerCode = GenerateNewCustomerCode();

                    var newCustomer = new Customer();
                    newCustomer.Ethnicity = customerRequest.ethnicity;
                    newCustomer.CustomerName = customerRequest.customerName;
                    newCustomer.CustomerCode = newCustomerCode;
                    newCustomer.CustomerCategoryIdFk = customerRequest.categoryId;
                    newCustomer.Address = customerRequest.address;
                    newCustomer.CustomField_1 = customerRequest.customField_1;
                    newCustomer.CustomField_2 = customerRequest.customField_2;
                    newCustomer.ContactNo_1 = customerRequest.contactNo_1;
                    newCustomer.ContactNo_2 = customerRequest.contactNo_2;
                    newCustomer.ContactNo_3 = customerRequest.contactNo_3;
                    newCustomer.CreatedAt = DateTime.Now;
                    newCustomer.CreatedBy = userId;

                    if (string.IsNullOrWhiteSpace(customerRequest.customerName))
                    {
                        errorResponse = new ErrorResponse();
                        errorResponse.message = Messages.CUSTOMER_NAME_CAN_NOT_BE_BLANK_MESSAGE;
                        errorResponse.code = Codes.CUSTOMER_NAME_CAN_NOT_BE_BLANK_CODE;
                        failureResponse = new RequestResponseModel.FailureResponse();
                        failureResponse.status = false;
                        failureResponse.error = errorResponse;
                        return BadRequest(failureResponse);
                    }

                    else if (customerRequest.categoryId == null)
                    {
                        errorResponse = new ErrorResponse();
                        errorResponse.message = Messages.CATEGORY_ID_CAN_NOT_BE_BLANK_MESSAGE;
                        errorResponse.code = Codes.CATEGORY_ID_CAN_NOT_BE_BLANK_CODE;
                        failureResponse = new RequestResponseModel.FailureResponse();
                        failureResponse.status = false;
                        failureResponse.error = errorResponse;
                        return BadRequest(failureResponse);
                    }

                    else if (string.IsNullOrWhiteSpace(customerRequest.contactNo_1))
                    {
                        errorResponse = new ErrorResponse();
                        errorResponse.message = Messages.CUSTOMER_CONTACT_NO_1_CAN_NOT_BE_BLANK_MESSAGE;
                        errorResponse.code = Codes.CUSTOMER_CONATCT_NO_1_CAN_NOT_BE_BLANK_CODE;
                        failureResponse = new RequestResponseModel.FailureResponse();
                        failureResponse.status = false;
                        failureResponse.error = errorResponse;
                        return BadRequest(failureResponse);
                    }

                    _context.Customers.Add(newCustomer);
                    _context.SaveChanges();

                    successResponse.status = true;
                    successResponse.data = new
                    {
                        id = newCustomer.CustomerId,
                        message = Constants.SuccessMessages.CUSTOMER_CATEGORY_SAVED_MESSAGE,

                    };
                    return Ok(successResponse);
                }

                else
                {
                    throw new FailedToDeleteCustomer();
                }


            }
            catch (CustomerNameCannotBeBlank ex)
            {
                _logger.LogError($" Exception Message: {ex.Message}");
                _logger.LogError($" Exception Stack Trace: {ex.StackTrace}");
                errorResponse = new ErrorResponse();
                errorResponse.message = Constants.Errors.Messages.CUSTOMER_CATEGORY_NAME_CAN_NOT_BE_BLANK_MESSAGE;
                errorResponse.code = Constants.Errors.Codes.CUSTOMER_CODE_CAN_NOT_BE_BLANK_CODE;

                failureResponse = new FailureResponse();
                failureResponse.status = false;
                failureResponse.error = errorResponse;
                return BadRequest(failureResponse);
            }
            catch (CustomerCategoryIdDoesnotExists ex)
            {
                _logger.LogError($" Exception Message: {ex.Message}");
                _logger.LogError($" Exception Stack Trace: {ex.StackTrace}");
                var errorResponse = new ErrorResponse();
                errorResponse.message = Messages.CUSTOMER_CATEGORY_ID_DOES_NOT_EXISTS;
                errorResponse.code = Codes.CUSTOMER_CATEGORY_ID_DOES_NOT_EXISTS_ERROR_CODE;
                failureResponse = new FailureResponse();
                failureResponse.status = false;
                failureResponse.error = errorResponse;
                return NotFound(failureResponse);
            }
            catch (FailedToDeleteCustomer ex)
            {

                _logger.LogError($" Exception Message: {ex.Message}");
                _logger.LogError($" Exception Stack Trace: {ex.StackTrace}");
                errorResponse = new ErrorResponse();
                errorResponse.message = Messages.CUSTOMER_CONTACT_NUMBER_ALREADY_EXISTS_MESSAGE;
                errorResponse.code = Codes.CUSTOMER_CONTACT_NUMBER_ALREADY_EXISTS_CODE;
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

        private string GenerateNewCustomerCode()
        {
            var maxCustomerId = _context.Customers.Max(c => (int?)c.CustomerId);
            if (maxCustomerId.HasValue)
            {
                return "C-" + (maxCustomerId + 1);
            }

            return "C-1";
        }




        [HttpPost("UpdateCustomer")]
        public async Task<IActionResult> UpdateCustomer([FromBody] UpdateCustomerRequest updateCustomerRequest, [FromHeader(Name = "userId")] int userId)
        {
            try
            {
                var existingCustomer = _context.Customers.FirstOrDefault(p => p.CustomerId == updateCustomerRequest.CustomerId);


                if (existingCustomer != null)
                {
                    if (string.IsNullOrWhiteSpace(updateCustomerRequest.CustomerName))
                    {
                        throw new CustomerNameCannotBeBlank();
                    }
                    CustomerCategory existingCategory = _context.CustomerCategory.FirstOrDefault(p => p.CustomerCategoryId == updateCustomerRequest.CategoryId);
                    if (existingCategory == null)
                    {
                        throw new CustomerCategoryIdDoesnotExists();
                    }
                    existingCustomer.Ethnicity = updateCustomerRequest.Ethnicity;
                    existingCustomer.CustomerName = updateCustomerRequest.CustomerName;
                    existingCustomer.CustomerCategoryIdFk = updateCustomerRequest.CategoryId;
                    existingCustomer.Address = updateCustomerRequest.Address;
                    existingCustomer.CustomField_1 = updateCustomerRequest.CustomField_1;
                    existingCustomer.CustomField_2 = updateCustomerRequest.CustomField_2;
                    existingCustomer.ContactNo_1 = updateCustomerRequest.ContactNo_1;
                    existingCustomer.ContactNo_2 = updateCustomerRequest.ContactNo_2;
                    existingCustomer.ContactNo_3 = updateCustomerRequest.ContactNo_3;

                    await _context.SaveChangesAsync();

                    var success = new SuccessResponse
                    {

                        status = true,
                        data = new
                        {
                            message = Constants.SuccessMessages.CUSTOMER_UPDATED_MESSAGE
                        }

                    };
                    return Ok(success);
                }
                else
                {
                    throw new FailedToUpdateCustomer();
                }

            }
            catch (FailedToUpdateCustomer ex)
            {
                _logger.LogError($" Exception Message: {ex.Message}");
                _logger.LogError($" Exception Stack Trace: {ex.StackTrace}");
                errorResponse = new ErrorResponse();
                errorResponse.message = Constants.Errors.Messages.FAILED_TO_UPDATE_CUSTOMER_MESSAGE;
                errorResponse.code = Constants.Errors.Codes.FAILED_TO_UPDATE_CUSTOMER_ERROR_CODE;
                failureResponse = new FailureResponse();
                failureResponse.status = false;
                failureResponse.error = errorResponse;
                return BadRequest(failureResponse);
            }

            catch (CustomerNameCannotBeBlank ex)
            {
                _logger.LogError($" Exception Message: {ex.Message}");
                _logger.LogError($" Exception Stack Trace: {ex.StackTrace}");
                errorResponse = new ErrorResponse();
                errorResponse.message = Constants.Errors.Messages.CUSTOMER_CATEGORY_NAME_CAN_NOT_BE_BLANK_MESSAGE;
                errorResponse.code = Constants.Errors.Codes.CUSTOMER_CODE_CAN_NOT_BE_BLANK_CODE;

                failureResponse = new FailureResponse();
                failureResponse.status = false;
                failureResponse.error = errorResponse;
                return BadRequest(failureResponse);
            }
            catch (CustomerCategoryIdDoesnotExists ex)
            {
                _logger.LogError($" Exception Message: {ex.Message}");
                _logger.LogError($" Exception Stack Trace: {ex.StackTrace}");
                var errorResponse = new ErrorResponse();
                errorResponse.message = Messages.CUSTOMER_CATEGORY_ID_DOES_NOT_EXISTS;
                errorResponse.code = Codes.CUSTOMER_CATEGORY_ID_DOES_NOT_EXISTS_ERROR_CODE;
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


        [HttpPost("DeleteCustomer")]
        public async Task<IActionResult> DeleteCustomer([FromBody] DeleteCustomerRequest deleteCustomerRequest, [FromHeader(Name = "userId")] int userId)
        {
            try
            {
                var existingCustomer = _context.Customers.FirstOrDefault(p => p.CustomerId == deleteCustomerRequest.CustomerId);
                if (existingCustomer != null)
                {
                    _context.Customers.Remove(existingCustomer);

                    await _context.SaveChangesAsync();

                    var success = new SuccessResponse
                    {

                        status = true,
                        data = new
                        {
                            message = Constants.SuccessMessages.CUSTOMER_DELETED_MESSAGE
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
                errorResponse.message = Constants.Errors.Messages.CUSTOMER_NOT_DELETED_MESSAGE;
                errorResponse.code = Constants.Errors.Codes.FAILED_TO_DELETE_CUSTOMER_CATEGORY_ERROR_CODE;
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












