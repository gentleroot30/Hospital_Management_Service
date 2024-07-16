using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HospitalMgmtService.Database;
using Microsoft.AspNetCore.Authorization;
using ServiceStack.Web;
using HospitalMgmtService.Model;
using HospitalMgmtService.Controllers;
using HospitalMgmtService.RequestResponseModel;
using HospitalMgmtService.RequestResponseModel.ResponseModel;
using static HospitalMgmtService.RequestResponseModel.Constants.Errors;
using HospitalMgmtService.RequestResponseModel.RequestModel;
using System.Net;

namespace HospitalMgmtService
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupplierController : ControllerBase
    {
        private readonly DBContext _context;
        private SuccessResponse successResponse = new SuccessResponse();
        private FailureResponse failureResponse = new FailureResponse();
        private ErrorResponse errorResponse = new ErrorResponse();
        public SupplierController(DBContext context)
        {
            _context = context;
        }
        [HttpPost("GetSuppliers")]
        public async Task<IActionResult> GetSuppliers([FromBody] GetSuppliers getSuppliers, [FromHeader(Name = "userId")] int userId)
        {
            try
            {
                var supplierQuery = _context.Suppliers.Select(p => new GetSupplierResponse
                {
                    SupplierId = p.SupplierId,
                    SupplierName = p.SupplierName,
                    SupplierCode = p.SupplierCode,
                    Address = p.Address,
                    ContactNo1 = p.ContactNo1,
                    ContactNo2 = p.ContactNo2,
                    ContactNo3 = p.ContactNo3,
                    PurchaseDue = p.Purchases.Sum(po => po.TotalBill) - p.Purchases.Sum(po => po.TotalPaid),
                    PurchaseReturns = p.PurchaseReturns.Sum(p => p.TotalReturnPaid),
                    createdBy = p.CreatedBy,
                    createdAt = p.CreatedAt
                }).AsQueryable();

                // Apply date filters if both dates are provided
                if (getSuppliers.fromtDate.HasValue && getSuppliers.totDate.HasValue)
                {
                    DateTime startDate = getSuppliers.fromtDate.Value;
                    DateTime endDate = getSuppliers.totDate.Value.AddDays(1); // To include the end date

                    supplierQuery = supplierQuery.Where(p => p.createdAt >= startDate && p.createdAt < endDate);
                }

                switch (getSuppliers.searchByType)
                {
                    case 1:
                        supplierQuery = supplierQuery.Where(supplier => supplier.SupplierName.Contains(getSuppliers.searchByValue));
                        break;
                    case 2:
                        supplierQuery = supplierQuery.Where(supplier => supplier.SupplierCode.Contains(getSuppliers.searchByValue));
                        break;
                    case 3:
                        supplierQuery = supplierQuery.Where(supplier => supplier.ContactNo1.Contains(getSuppliers.searchByValue));
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

                var suppliers = await supplierQuery.ToListAsync();

                // Check if no suppliers found and return a message
                if (suppliers.Count == 0)
                {
                    var noDataResponse = new SuccessResponse
                    {
                        status = true,
                        data = suppliers,
                        
                    };
                    return Ok(noDataResponse);
                }

                var successResponse = new SuccessResponse
                {
                    status = true,
                    data = suppliers
                };
                return Ok(successResponse);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        //[HttpPost("GetUsers")]
        //public async Task<ActionResult<User>> GetUsers([FromBody] GetUsersRequest getUsers)
        //{
        //    try
        //    {
        //        var userQuery = _context.Users.Select(x => new GetAllUsersResponse
        //        {
        //            userId = x.UserId,
        //            roleId = x.RoleIdFk,
        //            name = x.Name,
        //            address = x.Address,
        //            emailId = x.Email,
        //            roleName = x.Roles.RoleName,
        //            contactNo_1 = x.ContactNo1,
        //            contactNo_2 = x.ContactNo2,
        //            contactNo_3 = x.ContactNo3,
        //            createdAt = x.CreatedAt,
        //            createdBy = x.CreatedBy
        //        }).AsQueryable();

        //        if (getUsers.fromtDate.HasValue && getUsers.totDate.HasValue)
        //        {
        //            DateTime startDate = getUsers.fromtDate.Value;
        //            DateTime endDate = getUsers.totDate.Value.AddDays(1);

        //            userQuery = userQuery.Where(x => x.createdAt >= startDate && x.createdAt < endDate);
        //        }

        //        switch (getUsers.searchByType)
        //        {
        //            case 1:
        //                userQuery = userQuery.Where(role => role.roleName.Contains(getUsers.searchByValue));
        //                break;
        //            case 2:
        //                userQuery = userQuery.Where(user => user.name.Contains(getUsers.searchByValue));
        //                break;
        //            case 3:
        //                userQuery = userQuery.Where(user => user.contactNo_1.Contains(getUsers.searchByValue));
        //                break;
        //            case 4:
        //                userQuery = userQuery.Where(user => user.emailId.Contains(getUsers.searchByValue));
        //                break;
        //            default:
        //                var errorResponse = new ErrorResponse
        //                {
        //                    message = Constants.Errors.Messages.INVALID_SEARCH_TYPE_MESSAGE,
        //                    code = Constants.Errors.Codes.INVALID_SEARCHTYPE_ERROR_CODE
        //                };
        //                var failureResponse = new FailureResponse
        //                {
        //                    status = false,
        //                    error = errorResponse
        //                };
        //                return BadRequest(failureResponse);
        //        }

        //        var user = await userQuery.ToListAsync();

        //        var successResponse = new SuccessResponse
        //        {
        //            status = true,
        //            data = user
        //        };
        //        return Ok(successResponse);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}

       



        [HttpPost("GetSupplierById")]
        public async Task<IActionResult> GetSupplierById(GetSupplierByIdRequest getSupplierById, [FromHeader(Name = "userId")] int userId)
        {
            try
            {

                var supplier = await _context.Suppliers
           .Where(p => p.SupplierId == getSupplierById.SupplierId)
          .Select(p => new GetSupplierResponse
          {
              SupplierId = p.SupplierId,
              SupplierName = p.SupplierName,
              SupplierCode = p.SupplierCode,
              Address = p.Address,
              ContactNo1 = p.ContactNo1,
              ContactNo2 = p.ContactNo2,
              ContactNo3 = p.ContactNo3,
              PurchaseDue = p.Purchases.Sum(po => po.TotalBill - po.TotalPaid),
              PurchaseReturns = p.Purchases.Sum(po => po.TotalBill - (po.TotalBill - po.TotalPaid))
              ,
              createdBy = p.CreatedBy,
              createdAt = p.CreatedAt
          })
           .FirstOrDefaultAsync();


                if (supplier != null)
                {
                    var successResponse = new SuccessResponse();
                    successResponse.status = true;
                    successResponse.data = supplier;
                    return Ok(successResponse);
                }
                else
                {
                    var errorResponse = new ErrorResponse();
                    errorResponse.message = Messages.SUPPLIER_ID_DOES_NOT_EXISTS_MESSAGE;
                    errorResponse.code = Codes.SUPPLIER_ID_DOES_NOT_EXISTS_ERROR_CODE;
                    failureResponse = new FailureResponse();
                    failureResponse.status = false;
                    failureResponse.error = errorResponse;
                    return NotFound(failureResponse);
                }

            }catch(Exception ex)
            {
                return BadRequest(ex.Message);

            }
        }


        [HttpPost("AddSupplier")]
        public async Task<IActionResult> AddSupplier([FromBody] AddSupplierRequest supplierRequest, [FromHeader(Name = "userId")] int userId)
        {
            try
            {
                var existingSupplier = _context.Suppliers.FirstOrDefault(p => p.ContactNo1 == supplierRequest.contactNo_1);

                if (existingSupplier == null)
                {
                    var newSupplierCode = GenerateNewSupplierCode();
                    var newSupplier = new Supplier();
                    newSupplier.SupplierName = supplierRequest.supplierName;
                    newSupplier.SupplierCode = newSupplierCode;
                    newSupplier.Address = supplierRequest.address;
                    newSupplier.ContactNo1 = supplierRequest.contactNo_1;
                    newSupplier.ContactNo2 = supplierRequest.contactNo_2;
                    newSupplier.ContactNo3 = supplierRequest.contactNo_3;
                    newSupplier.CreatedAt = DateTime.Now;
                    newSupplier.CreatedBy = userId;
                    if (string.IsNullOrWhiteSpace(supplierRequest.supplierName))
                    {
                        errorResponse = new ErrorResponse();
                        errorResponse.message = Messages.SUPPLIER_NAME_CAN_NOT_BE_BLANK_MESSAGE;
                        errorResponse.code = Codes.SUPPLIER_FULLNAME_CAN_NOT_BE_BLANKERROR_CODE;
                        failureResponse = new FailureResponse();
                        failureResponse.status = false;
                        failureResponse.error = errorResponse;
                        return BadRequest(failureResponse);
                    }
                    else if (string.IsNullOrWhiteSpace(supplierRequest.address))
                    {
                        errorResponse = new ErrorResponse();
                        errorResponse.message = Messages.SUPPLIER_ADDRESS_CAN_NOT_BE_BLANK_MESSAGE;
                        errorResponse.code = Codes.SUPPLIER_ADDRESS_CAN_NOT_BE_BLANK_ERROR_CODE;
                        failureResponse = new FailureResponse();
                        failureResponse.status = false;
                        failureResponse.error = errorResponse;
                        return BadRequest(failureResponse);
                    }
                    else if (string.IsNullOrWhiteSpace(supplierRequest.contactNo_1))
                    {
                        errorResponse = new ErrorResponse();
                        errorResponse.message = Messages.SUPPLIER_CONTACT_NO_CAN_NOT_BE_BLANK_MESSAGE;
                        errorResponse.code = Codes.SUPPLIER_CONTACT_NO_CAN_NOT_BE_BLANKERROR_CODE;
                        failureResponse = new FailureResponse();
                        failureResponse.status = false;
                        failureResponse.error = errorResponse;
                        return BadRequest(failureResponse);
                    }


                    _context.Suppliers.Add(newSupplier);
                    var supplier = _context.SaveChanges();
                    if (supplier != null)
                    {
                        var successResponse = new SuccessResponse();
                        successResponse.data = new
                        {
                            id=newSupplier.SupplierId,
                            message = Constants.SuccessMessages.SUPPLIER_SAVED_MESSAGE
                        };
                        successResponse.status = true;
                        return Ok(successResponse);
                    }
                    else
                    {

                        var errorResponse = new ErrorResponse();
                        errorResponse.message = Messages.SUPPLIER_ALREADY_EXIST_MESSAGE;
                        errorResponse.code = Codes.SUPPLIER_ALREADY_EXIST_ERROR_CODE;
                        failureResponse = new FailureResponse();
                        failureResponse.status = false;
                        failureResponse.error = errorResponse;
                        return Conflict(failureResponse);
                    }
                }
                else
                {
                    var errorResponse = new ErrorResponse();
                    errorResponse.message = Messages.FAILED_TO_SAVE_SUPPLIER_MESSAGE;
                    errorResponse.code = Codes.FAILED_TO_SAVE_SUPPLIER_DATA_ERROR_CODE;
                    failureResponse = new FailureResponse();
                    failureResponse.status = false;
                    failureResponse.error = errorResponse;
                    return BadRequest(failureResponse);

                }
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }
        private string GenerateNewSupplierCode()
        {
            var maxSupplierCode = _context.Suppliers
                .Max(c => c.SupplierCode);

            if (!string.IsNullOrEmpty(maxSupplierCode))
            {
                if (int.TryParse(maxSupplierCode.Replace("S-", ""), out int codeNumber))
                {
                    codeNumber++; 
                    return "S-" + codeNumber; 
                }
            }

            return "S-1"; 
        }




        [HttpPost("UpdateSupplier")]
        public async Task<IActionResult> UpdateSupplier([FromBody] UpdateSupplierRequest updateSupplier, [FromHeader(Name = "userId")] int userId)
        {
            try
            {
                var existingSupplier = _context.Suppliers.FirstOrDefault(p => p.SupplierId == updateSupplier.SupplierId);


                if (existingSupplier != null)
                {

                    existingSupplier.SupplierName = updateSupplier.SupplierName;                    
                    existingSupplier.Address = updateSupplier.Address;
                    existingSupplier.ContactNo1 = updateSupplier.ContactNo1;
                    existingSupplier.ContactNo2 = updateSupplier.ContactNo2;
                    existingSupplier.ContactNo3 = updateSupplier.ContactNo3;
                    existingSupplier.UpdatedAt = DateTime.Now;
                    existingSupplier.UpdatedBy = userId;

                    await _context.SaveChangesAsync();

                    var successResponse = new SuccessResponse();
                    successResponse.data = new
                    {
                        message = Constants.SuccessMessages.SUPPLIER_UPDATED_MESSAGE
                    };
                    successResponse.status = true;
                    return Ok(successResponse);
                }
                else
                {
                    var errorResponse = new ErrorResponse();
                    errorResponse.message = Messages.FAILED_TO_UPDATE_SUPPLIER_MESSAGE;
                    errorResponse.code = Codes.FAILED_TO_UPDATE_SUPPLIER_DATA_ERROR_CODE;
                    failureResponse = new FailureResponse();
                    failureResponse.status = false;
                    failureResponse.error = errorResponse;
                    return BadRequest(failureResponse);
                }

                }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



        [HttpPost("DeleteSupplier")]
        public async Task<IActionResult> DeleteSupplier([FromBody] DeleteSupplierRequest deleteSupplier, [FromHeader(Name = "userId")] int userId)
        {
            var existingSupplier = _context.Suppliers.FirstOrDefault(p => p.SupplierId == deleteSupplier.SupplierID);


            if (existingSupplier != null)
            {
                _context.Suppliers.Remove(existingSupplier);

                await _context.SaveChangesAsync();


                var successResponse = new SuccessResponse();
                successResponse.data = new
                {
                    message = Constants.SuccessMessages.SUPPLIER_UPDATED_MESSAGE
                };
                successResponse.status = true;
                return Ok(successResponse);
            }
            else
            {
                var errorResponse = new ErrorResponse();
                errorResponse.message = Messages.FAILED_TO_DELETE_SUPPLIER_MESSAGE;
                errorResponse.code = Codes.FAILED_TO_UPDATE_SUPPLIER_DATA_ERROR_CODE;
                failureResponse = new FailureResponse();
                failureResponse.status = false;
                failureResponse.error = errorResponse;
                return BadRequest(failureResponse);
            }

        }

    }

}