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
using HospitalMgmtService.RequestResponseModel.RequestModel;
using HospitalMgmtService.Database;
using static HospitalMgmtService.Controllers.CustomExceptions;

namespace HospitalMgmtService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class POSController : ControllerBase
    {
        private readonly DBContext _context;
        private SuccessResponse successResponse = new SuccessResponse();
        private FailureResponse failureResponse = new FailureResponse();
        private ErrorResponse errorResponse = new ErrorResponse();

        public POSController(DBContext context)
        {
            _context = context;
        }
        [HttpPost("GetPOS")]
        public async Task<IActionResult> GetPOS([FromBody] GetCustomerCategoriesRequest getCustomers, [FromHeader(Name = "userId")] int userId)
        {
            try
            {
                var posQuery = _context.Sales.Select(q => new GetPOSResponse
                {

                    salesId = q.SalesId,
                    customerCategoryName = q.Customer.CustomerCategory.CategoryName,
                    customerName = q.Customer.CustomerName,
                    totalBill = q.TotalBill,
                    totalPaid = q.TotalPaid,
                    paymentDue = q.TotalBill - q.TotalPaid,
                    contactNo = q.Customer.ContactNo_1,
                    createdBy = q.CreatedBy,
                    createdAt = q.CreatedAt

                }).AsQueryable();


                if (getCustomers.searchByType == 1)
                {
                    posQuery = posQuery.Where(category => category.customerName.Contains(getCustomers.searchByValue));
                }

                else if (getCustomers.searchByType == 2)
                {
                    posQuery = posQuery.Where(category => category.customerCategoryName.Contains(getCustomers.searchByValue));
                }
                else if (getCustomers.searchByType == 3)
                {
                    posQuery = posQuery.Where(category => category.contactNo.Contains(getCustomers.searchByValue));
                }

                else
                {
                    throw new InvalidSearchType();
                }

                var pos = await posQuery.ToListAsync();


                var successResponse = new SuccessResponse();
                successResponse.status = true;
                successResponse.data = pos;
                return Ok(successResponse);

            }
            catch (InvalidSearchType exx)
            {

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

        [HttpPost("GetPOSById")]
        public async Task<IActionResult> GetPOSById([FromBody] GetPOSByIdRequest getPOSById, [FromHeader(Name = "userId")] int userId)
        {
            try
            {
                var posdata = await _context.Sales
                                  .Where(x => x.SalesId == getPOSById.SalesId)
                                  .Select(x => new GetPOSByIDResponse
                                  {
                                      SalesId = x.SalesId,
                                      CategoryName = x.Customer.CustomerCategory.CategoryName,
                                      CustomerId = x.Customer.CustomerId,
                                      CustomerName = x.Customer.CustomerName,
                                      PosDate = x.PosDate,
                                      TotalBill = x.TotalBill,
                                      TotalPaid = x.TotalPaid,
                                      ContactNo_1 = x.Customer.ContactNo_1,
                                      CreatedAt = x.CreatedAt,
                                      CreatedBy = x.CreatedBy,
                                      productDetails = x.SalesRecords.Select(pr => new RequestResponseModel.ResponseModel.POSProductDataDTO
                                      {
                                          ProductId = pr.ProductIdFk,
                                          Quantity = pr.Quantity,
                                          Amount = pr.Amount,
                                          DiscountPercent=pr.Product.DiscountPercent,
                                          ProductName = pr.Product.ProductName,
                                          BatchId = pr.BatchIdFk,

                                      }).ToList(),
                                      payementDetails = x.SalesPayments.Select(pr => new RequestResponseModel.ResponseModel.POSPaymentDetailsDTO
                                      {
                                          PaymentId = pr.PaymentId,
                                          PaymentMethod = pr.PaymentMethod,
                                          Amount = pr.Amount,
                                          PaymentDate = pr.PaymentDate,

                                      }).ToList()
                                  })
                                  .FirstOrDefaultAsync();
                if (posdata != null)
                {
                    var successResponse = new SuccessResponse();
                    successResponse.status = true;
                    successResponse.data = posdata;
                    return Ok(successResponse);
                }
                else
                {
                    throw new ProductIdDoesnotExists();
                }

            }
            catch (ProductIdDoesnotExists)
            {

                errorResponse = new ErrorResponse();
                errorResponse.message = Messages.PRODUCT_ID_DOES_NOT_EXISTS_MESSAGE;
                errorResponse.code = Codes.PRODUCT_ID_DOES_NOT_EXISTS_ERROR_CODE;
                failureResponse = new FailureResponse();
                failureResponse.status = false;
                failureResponse.error = errorResponse;
                return NotFound(failureResponse);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }


        [HttpPost("AddPOS")]
        public async Task<IActionResult> AddPOS([FromBody] AddPOSRequest addPOSRequest, [FromHeader(Name = "userId")] int userId)
        {
            try
            {
                if (addPOSRequest.customerId <= 0)
                {
                    throw new CustomerIdCannotBeBlank();
                }

                else if (addPOSRequest.posDate == DateTime.MinValue)
                {
                    throw new POSDateCannotBeBlank();
                }

                if (addPOSRequest.totalBill <= 0)
                {
                    throw new TotalBillCannotBeBlank();
                }
                Sales newPOS = new Sales();
                newPOS.PosNo = GenerateNewPOSNumber();
                newPOS.CustomerIdFk = addPOSRequest.customerId;
                newPOS.PosDate = addPOSRequest.posDate;
                newPOS.TotalBill = addPOSRequest.totalBill;
                newPOS.TotalPaid = addPOSRequest.totalPaid;
                newPOS.CreatedBy = userId;
                newPOS.CreatedAt = DateTime.Now;
              
                _context.Sales.Add(newPOS);
                _context.SaveChanges();

                Sales existingPOS = _context.Sales.Find(newPOS.SalesId);


                foreach (var productDto in addPOSRequest.productDetails)
                {
                    Product product = _context.Product.Find(productDto.productId);

                    if (product == null)
                    {
                        throw new ProductCategoryIdDoesnotExists();
                    }
                    //todo
                    //product.Quantity = product.Quantity - productDto.quantity;
                    //update product quantity
                    SalesRecord salesRecord = new SalesRecord();
                    salesRecord.Sales = existingPOS;
                    salesRecord.Product = product;
                    //salesRecord.Quantity = productDto.quantity;
                    product.Quantity = product.Quantity - productDto.quantity;
                    salesRecord.BatchIdFk = productDto.batchId;
                    salesRecord.Quantity= productDto.quantity;
                    salesRecord.Amount = productDto.amount;
                    salesRecord.AppliedDiscount = product.DiscountPercent;
                    salesRecord.CreatedBy = userId;
                    salesRecord.CreatedAt = DateTime.Now;
                    _context.SalesRecord.Add(salesRecord);
                    _context.SaveChanges();
                }

                if (addPOSRequest.paymentDetails != null && addPOSRequest.paymentDetails.Any())
                {
                    foreach (var paymentDetails in addPOSRequest.paymentDetails)
                    {

                        SalesPayment newSalesPayment = new SalesPayment();

                        newSalesPayment.Sales = existingPOS;
                        newSalesPayment.PaymentMethod = paymentDetails.paymentMethod;
                        newSalesPayment.PaymentDate = paymentDetails.paymentDate;
                        newSalesPayment.Amount = paymentDetails.amount;
                        newSalesPayment.CreatedAt = DateTime.Now;
                        newSalesPayment.CreatedBy = userId;

                        if (paymentDetails.paymentMethod == 0)
                        {
                            throw new PaymentMethodCannotBeBlank();
                        }

                        else if (paymentDetails.amount == 0)
                        {
                            throw new AmountCannotBeBlank();
                        }

                        _context.SalesPayment.Add(newSalesPayment);
                        await _context.SaveChangesAsync();

                    }
                }

                if (newPOS.SalesId != 0)
                {
                    successResponse.status = true;
                    successResponse.data = new
                    {
                        id = newPOS.SalesId,
                        message = Constants.SuccessMessages.POS_SAVED_MESSAGE,

                    };
                    return Ok(successResponse);
                }

                else
                {
                    throw new FailedToSavePOS();
                }

            }
            catch (POSDateCannotBeBlank)
            {

                errorResponse = new RequestResponseModel.ErrorResponse();
                errorResponse.message = Constants.Errors.Messages.POS_DATE_CAN_NOT_BE_BLANK_MESSAGE;
                errorResponse.code = Constants.Errors.Codes.POS_DATE_CAN_NOT_BE_BlANK_ERROR_CODE;
                failureResponse = new RequestResponseModel.FailureResponse();
                failureResponse.status = false;
                failureResponse.error = errorResponse;
                return BadRequest(failureResponse);

            }
            catch (TotalBillCannotBeBlank)
            {

                errorResponse = new RequestResponseModel.ErrorResponse();
                errorResponse.message = Constants.Errors.Messages.TOTAL_BILL_CAN_NOT_BE_BLANK_MESSAGE;
                errorResponse.code = Constants.Errors.Codes.TOTAL_BILL_CAN_NOT_BE_BlANK_ERROR_CODE;
                failureResponse = new RequestResponseModel.FailureResponse();
                failureResponse.status = false;
                failureResponse.error = errorResponse;
                return BadRequest(failureResponse);
            }
            catch (ProductIdDoesnotExists)
            {


                errorResponse = new RequestResponseModel.ErrorResponse();
                errorResponse.message = Constants.Errors.Messages.PRODUCT_ID_DOES_NOT_EXISTS_MESSAGE;
                errorResponse.code = Constants.Errors.Codes.PRODUCT_ID_DOES_NOT_EXISTS_ERROR_CODE;
                failureResponse = new RequestResponseModel.FailureResponse();
                failureResponse.status = false;
                failureResponse.error = errorResponse;
                return NotFound(failureResponse);
            }
            catch (PaymentMethodCannotBeBlank)
            {

                errorResponse = new RequestResponseModel.ErrorResponse();
                errorResponse.message = Constants.Errors.Messages.PAYMENT_METHOD_CAN_NOT_BE_BLANK_MESSAGE;
                errorResponse.code = Constants.Errors.Codes.PAYMENT_METHOD_CAN_NOT_BE_BLANK_ERROR_CODE;
                failureResponse = new RequestResponseModel.FailureResponse();
                failureResponse.status = false;
                failureResponse.error = errorResponse;
                return BadRequest(failureResponse);
            }
            catch (AmountCannotBeBlank )
            {

                errorResponse = new RequestResponseModel.ErrorResponse();
                errorResponse.message = Constants.Errors.Messages.AMOUNT_CAN_NOT_BE_BLANK_MESSAGE;
                errorResponse.code = Constants.Errors.Codes.AMOUNT_CAN_NOT_BE_BLANK_ERROR_CODE;
                failureResponse = new RequestResponseModel.FailureResponse();
                failureResponse.status = false;
                failureResponse.error = errorResponse;
                return BadRequest(failureResponse);
            }
            catch (CustomerIdCannotBeBlank )
            {

                errorResponse = new RequestResponseModel.ErrorResponse();
                errorResponse.message = Constants.Errors.Messages.CUTOMER_ID_CAN_NOT_BE_BLANK_MESSAGE;
                errorResponse.code = Constants.Errors.Codes.CUSTOMER_ID_CAN_NOT_BE_BlANK_ERROR_CODE;
                failureResponse = new RequestResponseModel.FailureResponse();
                failureResponse.status = false;
                failureResponse.error = errorResponse;
                return BadRequest(failureResponse);

            }
            catch (FailedToSavePOS )
            {

                errorResponse = new ErrorResponse();
                errorResponse.message = Constants.Errors.Messages.FAILED_TO_SAVE_QUOTATION_MESSAGE;
                errorResponse.code = Constants.Errors.Codes.FAILED_TO_SAVE_POS_DATA_ERROR_CODE;
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

        private string GenerateNewPOSNumber()
        {
            var maxSalesId = _context.Sales.Max(c => (int?)c.SalesId);
            if (maxSalesId.HasValue)
            {
                return "pos-" + (maxSalesId + 1);
            }

            return "pos-1";
        }

        [HttpPost("UpdatePOS")]
        public async Task<IActionResult> UpdatePOS([FromBody] UpdatePOSRequest updatePOSRequest, [FromHeader(Name = "userId")] int userId)
        {
            try
            {
                var existingPOS = _context.Sales.FirstOrDefault(r => r.SalesId == updatePOSRequest.salesId);
                if (existingPOS != null)
                {
                    existingPOS.CustomerIdFk = updatePOSRequest.customerId;
                    existingPOS.PosDate = updatePOSRequest.posDate;
                    existingPOS.TotalBill = updatePOSRequest.totalBill;
                    existingPOS.TotalPaid = updatePOSRequest.totalPaid;
                    existingPOS.UpdatedBy = userId;
                    existingPOS.UpdatedAt = DateTime.Now;

                    var existingSalesRecord = _context.SalesRecord.Where(rf => rf.SaleIdFk == existingPOS.SalesId).ToList();

                    foreach (var productDto in updatePOSRequest.productDetails)
                    {
                        Product product = _context.Product.Find(productDto.productId);
                        if (product == null)
                        {
                            throw new ProductCategoryIdDoesnotExists();
                        }

                        SalesRecord newSalesRecord = new SalesRecord();
                        newSalesRecord.Sales = existingPOS;
                        newSalesRecord.Product = product;
                        newSalesRecord.Product.Quantity = productDto.quantity;
                        newSalesRecord.Quantity = productDto.quantity;
                        newSalesRecord.BatchIdFk = productDto.batchId;
                        newSalesRecord.Amount = productDto.amount;
                        newSalesRecord.AppliedDiscount = product.DiscountPercent;
                        newSalesRecord.UpdatedAt = DateTime.Now;
                        newSalesRecord.UpdatedBy = userId;

                        switch (productDto.opType)
                        {
                            case 1: // Add
                                _context.SalesRecord.Add(newSalesRecord);
                                break;
                            case 2: // Update
                                    
                                var existingRecord = existingSalesRecord.FirstOrDefault(r => r.ProductIdFk == productDto.productId);
                                if (existingRecord != null)
                                {
                                    existingRecord.Quantity = productDto.quantity;
                                    existingRecord.BatchIdFk = productDto.batchId;
                                    existingRecord.Amount = productDto.amount;
                                }
                                break;
                            case 3: // Delete
                                    
                                var recordToDelete = existingSalesRecord.FirstOrDefault(r => r.ProductIdFk == productDto.productId);
                                if (recordToDelete != null)
                                {
                                    _context.SalesRecord.Remove(recordToDelete);
                                }
                                break;
                           
                        }
                    }

                    foreach (var paymentDetails in updatePOSRequest.paymentDetails)
                    {
                        if(paymentDetails.opType == 2)
                        {
                            SalesPayment salesPayment = _context.SalesPayment.Find(paymentDetails.paymentId);
                            if (salesPayment != null)
                            {
                                salesPayment.PaymentMethod = paymentDetails.paymentMethod;
                                salesPayment.Amount = paymentDetails.amount;
                                salesPayment.PaymentDate = paymentDetails.paymentDate;
                                salesPayment.CreatedAt = DateTime.Now;
                                salesPayment.UpdatedBy = userId;
                                salesPayment.UpdatedAt = DateTime.Now;
                            }
                        }
                        
                        else if (paymentDetails.opType == 1)
                        {
                            SalesPayment newSalesPayment = new SalesPayment();
                            newSalesPayment.Sales = existingPOS;
                            newSalesPayment.PaymentMethod = paymentDetails.paymentMethod;
                            newSalesPayment.PaymentDate = paymentDetails.paymentDate;
                            newSalesPayment.Amount = paymentDetails.amount;
                            newSalesPayment.CreatedAt = DateTime.Now;
                            newSalesPayment.CreatedBy = userId;

                            if (paymentDetails.paymentMethod == 0)
                            {
                                throw new PaymentMethodCannotBeBlank();
                            }

                            else if (paymentDetails.amount == 0)
                            {
                                throw new AmountCannotBeBlank();
                            }

                            _context.SalesPayment.Add(newSalesPayment);
                        }

                        else if (paymentDetails.opType == 3)
                        {
                            SalesPayment salesPayment = _context.SalesPayment.Find(paymentDetails.paymentId);
                            if (salesPayment != null)
                            {
                                _context.SalesPayment.Remove(salesPayment);
                            }
                        }

                            await _context.SaveChangesAsync();
                    }

                    var successResponse = new SuccessResponse();
                    successResponse.data = Constants.SuccessMessages.POS_UPDATED_MESSAGE;
                    successResponse.status = true;

                    return Ok(successResponse);
                }
                else
                {
                    throw new FailedTOUpdatePOS();
                }
            }
            catch (ProductCategoryIdDoesnotExists ex)
            {
                errorResponse = new ErrorResponse();
                errorResponse.message = Constants.Errors.Messages.PRODUCT_ID_DOES_NOT_EXISTS_MESSAGE;
                errorResponse.code = Constants.Errors.Codes.PRODUCT_ID_DOES_NOT_EXISTS_ERROR_CODE;
                failureResponse = new RequestResponseModel.FailureResponse();
                failureResponse.status = false;
                failureResponse.error = errorResponse;
                return NotFound(failureResponse);

            }
            catch (FailedTOUpdatePOS ex)
            {

                errorResponse = new ErrorResponse();
                errorResponse.message = Constants.Errors.Messages.FAILED_TO_UPDATE_POS_MESSAGE;
                errorResponse.code = Constants.Errors.Codes.FAILED_TO_UPDATE_POS_DATA_ERROR_CODE;
                failureResponse = new RequestResponseModel.FailureResponse();
                failureResponse.status = false;
                failureResponse.error = errorResponse;
                return BadRequest(failureResponse);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPost("DeleteSales")]

        public async Task<IActionResult> DeleteSalesAsync([FromBody] DeletePOSRequest deletePOSRequest, [FromHeader(Name = "userId")] int userId)
        {
            try
            {
                var existingSales = _context.Sales
                   .Include(qt => qt.SalesRecords)
                   .Include(qt => qt.SalesPayments)
                   .FirstOrDefault(p => p.SalesId == deletePOSRequest.salesId);

                if (existingSales != null)
                {
                    if (existingSales.SalesPayments.Any())
                    {
                        errorResponse = new ErrorResponse();
                        errorResponse.message = Constants.Errors.Messages.CAN_NOT_DELETE_SALES_WITH_PAYMENTS_MESSAGE;
                        errorResponse.code = Constants.Errors.Codes.CAN_NOT_DELETE_SALES_WITH_PAYMENTS_ERROR_CODE;
                        failureResponse = new FailureResponse();
                        failureResponse.status = false;
                        failureResponse.error = errorResponse;
                        return BadRequest(failureResponse);
                    }

                    _context.SalesRecord.RemoveRange(existingSales.SalesRecords);
                    _context.Sales.Remove(existingSales);

                    await _context.SaveChangesAsync();

                    var success = new SuccessResponse
                    {
                        status = true,
                        data = new
                        {
                            message = Constants.SuccessMessages.POS_DELETED_MESSAGE
                        }
                    };

                    return Ok(success);
                }
                else
                {
                    throw new FailedToDeletePurchase();
                }

            }
            catch (FailedToDeletePOS ex)
            {

                errorResponse = new RequestResponseModel.ErrorResponse();
                errorResponse.message = Constants.Errors.Messages.FAILED_TO_DELETE_POS_MESSAGE;
                errorResponse.code = Constants.Errors.Codes.FAILED_TO_DELETE_POS_DATA_ERROR_CODE;
                failureResponse = new RequestResponseModel.FailureResponse();
                failureResponse.status = false;
                failureResponse.error = errorResponse;
                return NotFound(failureResponse);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }
        }

        [HttpPost("GetExpiredProducts")]
        public async Task<IActionResult> GetExpiredProducts([FromHeader(Name = "userId")] int userId)
        {
            try
            {
                DateTime currentDate = DateTime.Now;

                List<GetExpiredProductResponse> expiredProducts = await _context.Batches
                    .Where(b => b.ExpiryDate <= currentDate)
                    .Select(q => new GetExpiredProductResponse
                    {
                        productName = q.Product.ProductName,
                        expiredDate = q.ExpiryDate,
                        createdAt = q.CreatedAt,
                        createdBy = q.CreatedBy
                    })
                    .ToListAsync();
                if (expiredProducts != null)
                {

                    var successResponse = new SuccessResponse();
                    successResponse.status = true;
                    successResponse.data = expiredProducts;
                    return Ok(successResponse);
                }
                else
                {
                    throw new FailedToFetchExpiredData();
                }

            }
            catch (FailedToFetchExpiredData ex)
            {

                errorResponse = new ErrorResponse();
                errorResponse.message = Messages.FAILED_TO_FETCH_POS_DATA_MESSAGE;
                errorResponse.code = Codes.FAILED_TO_FETCH_POS_DATA_ERROR_CODE;
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

        [HttpPost("GetNearExpieryProducts")]
        public async Task<IActionResult> GetNearExpieryProducts([FromBody] GetNearExpieryProductsRequest nearExpieryProducts, [FromHeader(Name = "userId")] int userId)
        {
            try
            {
                DateTime currentDate = DateTime.Now;
                DateTime thresholdDate = currentDate.AddDays(nearExpieryProducts.daysThreshold);


                List<GetNearExpiredProductsResponse> expiredProducts = await _context.Batches
        .Where(b => b.ExpiryDate > currentDate && b.ExpiryDate <= thresholdDate)
                    .Select(q => new GetNearExpiredProductsResponse
                    {
                        productName = q.Product.ProductName,
                        expiredDate = q.ExpiryDate,
                        createdBy = q.CreatedBy,
                        createdAt = q.CreatedAt
                    })
                    .ToListAsync();
                if (expiredProducts != null)
                {

                    var successResponse = new SuccessResponse();
                    successResponse.status = true;
                    successResponse.data = expiredProducts;
                    return Ok(successResponse);
                }
                else
                {
                    throw new NearExpiryDataNotFound();
                }

            }
            catch (NearExpiryDataNotFound ex)
            {

                errorResponse = new ErrorResponse();
                errorResponse.message = Messages.FAILED_TO_FETCH_POS_DATA_MESSAGE;
                errorResponse.code = Codes.FAILED_TO_FETCH_POS_DATA_ERROR_CODE;
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
        [HttpPost("GetLowerProducts")]
        public async Task<IActionResult> GetLowerStockProducts([FromHeader(Name = "userId")] int userId)
        {
            try
            {

                var lowStockProducts = await _context.Batches.Include(b => b.Product)
            .Where(p => p.Product.Quantity < p.Product.AlertQuantity)
                    .Select(q => new GetNearExpiredProductsResponse
                    {
                        productName = q.Product.ProductName,
                        expiredDate = q.ExpiryDate,
                        createdAt = q.CreatedAt,
                        createdBy = q.CreatedBy

                    })
                    .ToListAsync();
                if (lowStockProducts != null)
                {

                    var successResponse = new SuccessResponse();
                    successResponse.status = true;
                    successResponse.data = lowStockProducts;
                    return Ok(successResponse);
                }
                else
                {
                    throw new LowStockDataNotFound();
                }

            }
            catch (LowStockDataNotFound ex)
            {

                errorResponse = new ErrorResponse();
                errorResponse.message = Messages.FAILED_TO_FETCH_POS_DATA_MESSAGE;
                errorResponse.code = Codes.FAILED_TO_FETCH_POS_DATA_ERROR_CODE;
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

    }
}
