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
using HospitalMgmtService.RequestResponseModel.ResponseModel;
using HospitalMgmtService.RequestResponseModel;
using static HospitalMgmtService.RequestResponseModel.Constants.Errors;
using HospitalMgmtService.RequestResponseModel.RequestModel;
using Microsoft.Extensions.Logging;
using static HospitalMgmtService.Controllers.CustomExceptions;

namespace HospitalMgmtService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class SalesReturnController : ControllerBase
    {
        private readonly DBContext _context;
        private SuccessResponse successResponse = new SuccessResponse();
        private FailureResponse failureResponse = new FailureResponse();
        private ErrorResponse errorResponse = new ErrorResponse();
        private readonly ILogger<SalesReturnController> _logger;


        public SalesReturnController(DBContext context, ILogger<SalesReturnController> logger)
        {
            _context = context;
            logger = _logger;
        }

        

        [HttpPost("GetSalesRetruns")]
        public async Task<IActionResult> GetSalesRetruns([FromBody] GetSalesReturnsRequest getSalesReturns, [FromHeader(Name = "userId")] int userId)
        {
            try
            {
                var salesReturnQuery = _context.SalesReturn
                    .Select(q => new GetAllSalesReturn
                    {

                        returnId = q.ReturnId,
                        returnDate = q.ReturnDate,
                        returnRefNo = q.ReturnRefNo,
                        customerName = q.Customers.CustomerName,
                        totalReturnAmount = q.TotalReturnAmount,
                        totalReturnAmountPaid = q.TotalReturnAmountPaid,
                        contactNo = q.Customers.ContactNo_1,
                        createdAt = q.CreatedAt,
                        createdBy = q.CreatedBy
                    })
                    .AsQueryable();

                if (getSalesReturns.fromDate.HasValue && getSalesReturns.toDate.HasValue)
                {
                    var startDate = getSalesReturns.fromDate.Value.Date;
                    var toDate = getSalesReturns.toDate.Value.Date;
                    salesReturnQuery = salesReturnQuery.Where(q => q.returnDate >= startDate && q.returnDate <= toDate);
                }

                switch (getSalesReturns.searchByType)
                {
                    case 1:
                        salesReturnQuery = salesReturnQuery.Where(q => q.customerName.ToUpper().Contains(getSalesReturns.searchByValue.ToUpper()));
                        break;

                    case 2:
                        salesReturnQuery = salesReturnQuery.Where(q => q.returnRefNo.ToUpper().Contains(getSalesReturns.searchByValue.ToUpper()));
                        break;

                        case 3:
                        salesReturnQuery = salesReturnQuery.Where(q => q.contactNo.ToUpper().Contains(getSalesReturns.searchByValue.ToUpper()));
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
                
               
                var salesReturns = await salesReturnQuery.ToListAsync();
                if (salesReturns.Count == 0)
                {
                    var noDataResponse = new SuccessResponse
                    {
                        status = true,
                        data = salesReturns,

                    };
                    return Ok(noDataResponse);
                }

                var successResponse = new SuccessResponse
                {
                    status = true,
                    data = salesReturns
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



        [HttpPost("GetSalesReturnById")]
        public async Task<IActionResult> GetSalesReturnById(GetSalesReturnById getSalesReturnById, [FromHeader(Name = "userId")] int userId)
        {
            try
            {
                var salesReturn = await _context.SalesReturn.Include(q => q.SalesReturnRecord)
                    .Where(q => q.ReturnId == getSalesReturnById.returnId)
                    .Select(q => new GetSalesReturnByIdResponse
                    {
                        returnId = q.ReturnId,
                        returnDate = q.ReturnDate,
                        returnRefNo = q.ReturnRefNo,
                        customerId = q.CustomerIdFk,
                        totalReturnAmount = q.TotalReturnAmount,
                        totalReturnAmountPaid = q.TotalReturnAmountPaid,
                        customerName = q.Customers.CustomerName,
                        productDetails = (from d in q.SalesReturnRecord
                                          let batch = _context.Batches.FirstOrDefault(b => b.BatchId == d.BatchIdFk)
                                          select new SalesReturnProductDTO
                                          {
                                              productName = d.Product.ProductName,
                                              productId = d.ProductIdFk,
                                              batchId = d.BatchIdFk,
                                              batchNo = batch != null ? batch.BatchNo : null,
                                              mrpPerPack = batch != null ? (int)batch.MrpPerPack : 0, 
                                              returnQuantity = d.ReturnQuantity
                                          }).ToList(),
                paymentDetails = q.SalesReturnPayment.Select(pr => new RequestResponseModel.ResponseModel.SalesReturnPaymentDetails
                {
                    PaymentId = pr.PaymentId,
                    PaymentMethod = pr.PaymentMethod,
                    Amount = pr.Amount,
                    PaymentDate = pr.PaymentDate,

                }).ToList()
                    }).FirstOrDefaultAsync();

                if (salesReturn != null)
                {
                    var successResponse = new SuccessResponse();

                    successResponse.status = true;
                    successResponse.data = salesReturn;
                    return Ok(successResponse);
                }
                else
                {
                    errorResponse = new ErrorResponse();
                    errorResponse.message = Constants.Errors.Messages.SALESRETURN_ID_DOES_NOT_EXISTS_MESSAGE;
                    errorResponse.code = Constants.Errors.Codes.SALESRETRUN_ID_DOES_NOT_EXISTS_ERROR_CODE;
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

        [HttpPost("AddSalesReturn")]
        public async Task<IActionResult> AddSalesReturn([FromBody] AddSalesReturn addSalesReturn, [FromHeader(Name = "userId")] int userId)
        {
            try
            {
                SalesReturn existingSalesReturn = _context.SalesReturn.Where(r => r.ReturnRefNo == addSalesReturn.ReturnRefNo).FirstOrDefault();

                if (existingSalesReturn != null)
                {
                    errorResponse = new RequestResponseModel.ErrorResponse();
                    errorResponse.message = Constants.Errors.Messages.SALESRETURN_ALREADY_EXISTS_MESSAGE;
                    errorResponse.code = Constants.Errors.Codes.SALESRETRUN_ALREADY_EXISTS_ERROR_CODE;
                    failureResponse = new RequestResponseModel.FailureResponse();
                    failureResponse.status = false;
                    failureResponse.error = errorResponse;
                    return Conflict(failureResponse);

                }
                else if (existingSalesReturn == null)
                {
                    var newReturnRefNo = GenerateNewReturnRefNumber();
                    SalesReturn newSalesReturn = new SalesReturn();
                    newSalesReturn.CustomerIdFk = addSalesReturn.customerId;
                    newSalesReturn.ReturnRefNo = newReturnRefNo;
                    newSalesReturn.ReturnDate = addSalesReturn.returnDate;
                    newSalesReturn.TotalReturnAmount = addSalesReturn.totalReturnAmount;
                    newSalesReturn.TotalReturnAmountPaid = addSalesReturn.totalReturnAmountPaid;
                    newSalesReturn.CreatedBy = userId;
                    newSalesReturn.CreatedAt = DateTime.Now;

                    if (addSalesReturn.customerId == 0)
                    {
                        errorResponse = new ErrorResponse();
                        errorResponse.message = Messages.CUSTOMER_ID_CAN_NOT_BE_BLANK;
                        errorResponse.code = Codes.CUTOMER_ID_CAN_NOT_BE_BLANK_ERROR_CODE;
                        failureResponse = new FailureResponse();
                        failureResponse.status = false;
                        failureResponse.error = errorResponse;
                        return BadRequest(failureResponse);
                    }

                    if (addSalesReturn.returnDate == DateTime.MinValue)
                    {
                        errorResponse = new RequestResponseModel.ErrorResponse();
                        errorResponse.message = Constants.Errors.Messages.RETURN_DATE_CAN_NOT_BE_BLANK_MESSAGE;
                        errorResponse.code = Constants.Errors.Codes.RETURN_DATE_CAN_NOT_BE_BLANK_ERROR_CODE;
                        failureResponse = new RequestResponseModel.FailureResponse();
                        failureResponse.status = false;
                        failureResponse.error = errorResponse;
                        return BadRequest(failureResponse);
                    }

                    _context.SalesReturn.Add(newSalesReturn);
                    _context.SaveChanges();

                    existingSalesReturn = _context.SalesReturn.Find(newSalesReturn.ReturnId);


                    foreach (var productDto in addSalesReturn.productDetails)
                    {
                        Product product = _context.Product.Find(productDto.productId);

                        if (product == null)
                        {

                            errorResponse = new RequestResponseModel.ErrorResponse();
                            errorResponse.message = Constants.Errors.Messages.PRODUCT_ID_DOES_NOT_EXISTS_MESSAGE;
                            errorResponse.code = Constants.Errors.Codes.PRODUCT_ID_DOES_NOT_EXISTS_ERROR_CODE;
                            failureResponse = new RequestResponseModel.FailureResponse();
                            failureResponse.status = false;
                            failureResponse.error = errorResponse;
                            return NotFound(failureResponse);
                        }
                        Batch batch = _context.Batches.Find(productDto.batchId);

                        if (batch == null)
                        {

                            errorResponse = new RequestResponseModel.ErrorResponse();
                            errorResponse.message = Constants.Errors.Messages.BATCH_ID_DOES_NOT_EXISTS_MESSAGE;
                            errorResponse.code = Constants.Errors.Codes.BATCH_ID_DOES_NOT_EXISTS_ERROR_CODE;
                            failureResponse = new RequestResponseModel.FailureResponse();
                            failureResponse.status = false;
                            failureResponse.error = errorResponse;
                            return NotFound(failureResponse);
                        }
                        //todo in all purchase plus product quantity in purchase return it is mynus

                        //product.Quantity = product.Quantity + productDto.returnQuantity;

                        SalesReturnRecord salesReturnRecord = new SalesReturnRecord();
                        salesReturnRecord.SalesReturn = existingSalesReturn;
                        salesReturnRecord.Product = product;
                        salesReturnRecord.Amount = productDto.amount;
                        salesReturnRecord.ReturnQuantity = productDto.returnQuantity;
                        product.Quantity = product.Quantity + productDto.returnQuantity;
                        salesReturnRecord.Batch = batch;
                        salesReturnRecord.BatchIdFk = batch.BatchId;
                        salesReturnRecord.CreatedBy = userId;
                        salesReturnRecord.CreatedAt = DateTime.Now;
                        _context.SalesReturnRec.Add(salesReturnRecord);
                        _context.SaveChanges();
                    }

                    if (addSalesReturn.paymentDetails != null && addSalesReturn.paymentDetails.Any())
                    {
                        foreach (var paymentDetails in addSalesReturn.paymentDetails)
                        {

                            SalesReturnPaymentRecord newReturn = new SalesReturnPaymentRecord();

                            newReturn.Sales = existingSalesReturn;
                            newReturn.PaymentMethod = paymentDetails.paymentMethod;
                            newReturn.PaymentDate = paymentDetails.paymentDate;
                            newReturn.Amount = paymentDetails.amount;
                            newReturn.CreatedAt = DateTime.Now;
                            newReturn.CreatedBy = userId;

                            if (paymentDetails.paymentMethod == 0)
                            {
                                throw new PaymentMethodCannotBeBlank();
                            }

                            else if (paymentDetails.amount == 0)
                            {
                                throw new AmountCannotBeBlank();
                            }

                            _context.SalesReturnPayments.Add(newReturn);
                            await _context.SaveChangesAsync();

                        }
                    }

                    successResponse.status = true;
                    successResponse.data = new
                    {
                        id = newSalesReturn.ReturnId,
                        message = Constants.SuccessMessages.SALESRETURN_SAVED_MESSAGE,

                    };
                    return Ok(successResponse);
                }

                else
                {
                    errorResponse = new ErrorResponse();
                    errorResponse.message = Constants.Errors.Messages.FAILED_TO_SAVE_SALESRETURN_MESSAGE;
                    errorResponse.code = Constants.Errors.Codes.FAILED_TO_UPDATE_SALESRETRUN_DATA_ERROR_CODE;
                    failureResponse = new FailureResponse();
                    failureResponse.status = false;
                    failureResponse.error = errorResponse;
                    return BadRequest(failureResponse);
                }
            }
            catch (PaymentMethodCannotBeBlank ex)
            {

                errorResponse = new RequestResponseModel.ErrorResponse();
                errorResponse.message = Constants.Errors.Messages.PAYMENT_METHOD_CAN_NOT_BE_BLANK_MESSAGE;
                errorResponse.code = Constants.Errors.Codes.PAYMENT_METHOD_CAN_NOT_BE_BLANK_ERROR_CODE;
                failureResponse = new RequestResponseModel.FailureResponse();
                failureResponse.status = false;
                failureResponse.error = errorResponse;
                return BadRequest(failureResponse);
            }
            catch (AmountCannotBeBlank ex)
            {

                errorResponse = new RequestResponseModel.ErrorResponse();
                errorResponse.message = Constants.Errors.Messages.AMOUNT_CAN_NOT_BE_BLANK_MESSAGE;
                errorResponse.code = Constants.Errors.Codes.AMOUNT_CAN_NOT_BE_BLANK_ERROR_CODE;
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
        private string GenerateNewReturnRefNumber()
        {
            var maxSalesId = _context.SalesReturn.Max(c => (int?)c.ReturnId);
            if (maxSalesId.HasValue)
            {
                return "posr-" + (maxSalesId + 1);
            }

            return "posr-1";
        }


        [HttpPost("UpdateSalesReturn")]
        public async Task<IActionResult> UpdateSalesReturn([FromBody] UpdateSalesReturnRequest updateSalesReturn, [FromHeader(Name = "userId")] int userId)
        {
            try
            {
                var existingSalesReturn = _context.SalesReturn.FirstOrDefault(r => r.ReturnId == updateSalesReturn.returnId);
                if (existingSalesReturn != null)
                {
                    existingSalesReturn.CustomerIdFk = updateSalesReturn.customerId;
                    existingSalesReturn.ReturnDate = updateSalesReturn.returnDate;
                    existingSalesReturn.TotalReturnAmount = updateSalesReturn.totalReturnAmount;
                    existingSalesReturn.TotalReturnAmountPaid = updateSalesReturn.totalReturnAmountPaid;
                    existingSalesReturn.UpdatedBy = userId;
                    existingSalesReturn.UpdatedAt = DateTime.Now;
                    _context.SaveChanges();

                    var existingSalesReturnRecord = _context.SalesReturnRec.Where(rf => rf.SalesReturnIdFk == existingSalesReturn.ReturnId).ToList();
                    //_context.SalesReturnRec.RemoveRange(existingSalesReturnRecord);

                    foreach (var productDto in updateSalesReturn.productDetails)
                    {
                        Product product = _context.Product.Find(productDto.productId);
                       
                        if (product == null)
                        {

                            errorResponse = new RequestResponseModel.ErrorResponse();
                            errorResponse.message = Constants.Errors.Messages.BATCH_ID_DOES_NOT_EXISTS_MESSAGE;
                            errorResponse.code = Constants.Errors.Codes.BATCH_ID_DOES_NOT_EXISTS_ERROR_CODE;
                            failureResponse = new RequestResponseModel.FailureResponse();
                            failureResponse.status = false;
                            failureResponse.error = errorResponse;
                            return NotFound(failureResponse);
                        }
                        Batch updatebatch = _context.Batches.Find(productDto.batchId);

                        if (updatebatch == null)
                        {

                            errorResponse = new RequestResponseModel.ErrorResponse();
                            errorResponse.message = Constants.Errors.Messages.BATCH_ID_DOES_NOT_EXISTS_MESSAGE;
                            errorResponse.code = Constants.Errors.Codes.BATCH_ID_DOES_NOT_EXISTS_ERROR_CODE;
                            failureResponse = new RequestResponseModel.FailureResponse();
                            failureResponse.status = false;
                            failureResponse.error = errorResponse;
                            return NotFound(failureResponse);
                        }

                            SalesReturnRecord salesReturnRec = new SalesReturnRecord();
                            salesReturnRec.SalesReturn = existingSalesReturn;
                            salesReturnRec.Product = product;
                            salesReturnRec.Amount = productDto.amount;
                            salesReturnRec.ReturnQuantity = productDto.returnQuantity;
                            product.Quantity = product.Quantity + productDto.returnQuantity;
                            salesReturnRec.BatchIdFk = productDto.batchId;
                            salesReturnRec.CreatedBy = userId;
                            salesReturnRec.CreatedAt = DateTime.Now;

                        switch (productDto.opType)
                        {
                            case 1: // Add
                                _context.SalesReturnRec.Add(salesReturnRec);
                                break;
                            case 2: // Update
                                var existingReturnRecord = existingSalesReturnRecord.FirstOrDefault(r => r.ProductIdFk == productDto.productId);
                                if (existingReturnRecord != null)
                                {
                                    existingReturnRecord.ReturnQuantity = productDto.returnQuantity;
                                    product.Quantity = product.Quantity + productDto.returnQuantity;
                                    existingReturnRecord.BatchIdFk = productDto.batchId;
                                    existingReturnRecord.Amount = productDto.amount;
                                    existingReturnRecord.UpdatedBy = userId;
                                    existingReturnRecord.UpdatedAt = DateTime.Now;
                                }
                                break;
                            case 3: // Delete
                                var recordToDelete = existingSalesReturnRecord.FirstOrDefault(r => r.ProductIdFk == productDto.productId);
                                if (recordToDelete != null)
                                {
                                    _context.SalesReturnRec.Remove(recordToDelete);
                                }
                                break;
                        }
                    }

                    _context.SaveChanges();



                }

                foreach (var paymentDetails in updateSalesReturn.paymentDetails)
                {
                    SalesReturnPaymentRecord salesReturnPaymentRecord = _context.SalesReturnPayments.Find(paymentDetails.paymentId);
                    if (salesReturnPaymentRecord != null)
                    {
                        switch (paymentDetails.opType)
                        {
                            case 2: // Update
                                salesReturnPaymentRecord.PaymentMethod = paymentDetails.paymentMethod;
                                salesReturnPaymentRecord.Amount = paymentDetails.amount;
                                salesReturnPaymentRecord.PaymentDate = paymentDetails.paymentDate;
                                salesReturnPaymentRecord.UpdatedBy = userId;
                                salesReturnPaymentRecord.UpdatedAt = DateTime.Now;
                                break;
                            case 3: // Delete
                                _context.SalesReturnPayments.Remove(salesReturnPaymentRecord);
                                break;
                        }
                    }
                    else
                    {
                        switch (paymentDetails.opType)
                        {
                            case 1: // Add
                                SalesReturnPaymentRecord newReturn = new SalesReturnPaymentRecord
                                {
                                    Sales = existingSalesReturn,
                                    PaymentMethod = paymentDetails.paymentMethod,
                                    PaymentDate = paymentDetails.paymentDate,
                                    Amount = paymentDetails.amount,
                                    CreatedAt = DateTime.Now,
                                    CreatedBy = userId
                                };

                                if (paymentDetails.paymentMethod == 0)
                                {
                                    throw new PaymentMethodCannotBeBlank();
                                }
                                else if (paymentDetails.amount == 0)
                                {
                                    throw new AmountCannotBeBlank();
                                }

                                _context.SalesReturnPayments.Add(newReturn);
                                break;
                                
                        }
                    }
                }

                await _context.SaveChangesAsync();

                var successResponse = new SuccessResponse();
                    successResponse.data = Constants.SuccessMessages.SALESRETURN_UPDATED_MESSAGE;
                    successResponse.status = true;

                    return Ok(successResponse);
                
                errorResponse = new ErrorResponse();
                errorResponse.message = Constants.Errors.Messages.FAILED_TO_UPDATE_SALESRETURN_MESSAGE;
                errorResponse.code = Constants.Errors.Codes.FAILED_TO_UPDATE_SALESRETRUN_DATA_ERROR_CODE;
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

        [HttpPost("DeleteSalesReturn")]

        public async Task<IActionResult> DeleteSalesReturn([FromBody] DeleteSalesReturnIdRequest deleteSalesReturnRequest, [FromHeader(Name = "userId")] int userId)
        {
            try
            {
                var existingSalesReturn = _context.SalesReturn
                    .Include(qt => qt.SalesReturnRecord)
                    .Include(qt => qt.SalesReturnPayment)
                    .FirstOrDefault(p => p.ReturnId == deleteSalesReturnRequest.returnId);

                if (existingSalesReturn != null)
                {
                    _context.SalesReturnRec.RemoveRange(existingSalesReturn.SalesReturnRecord);

                    _context.SalesReturn.Remove(existingSalesReturn);

                    await _context.SaveChangesAsync();

                    var success = new SuccessResponse
                    {

                        status = true,
                        data = new
                        {
                            message = Constants.SuccessMessages.SALESRETURN_DELETED_MESSAGE
                        }

                    };
                    return Ok(success);
                }
                else
                {
                    throw new FailedToDeleteCustomer();
                }

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


