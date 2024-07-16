using HospitalMgmtService.RequestResponseModel;
using Microsoft.AspNetCore.Mvc;
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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static HospitalMgmtService.RequestResponseModel.Constants.Errors;
using static HospitalMgmtService.Controllers.CustomExceptions;

namespace HospitalMgmtService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PurchaseReturnController : ControllerBase
    {
        private readonly DBContext _context;
        private SuccessResponse successResponse = new SuccessResponse();
        private FailureResponse failureResponse = new FailureResponse();
        private ErrorResponse errorResponse = new ErrorResponse();

        public PurchaseReturnController(DBContext context)
        {
            _context = context;
        }



        [HttpPost("GetPurchaseReturns")]
        public async Task<IActionResult> GetPurchaseReturns([FromBody] GetPurchaseReturnRequest getPurchaseReturnRequest, [FromHeader(Name = "userId")] int userId)
        {
            try
            {
                var purchaseReturnQuery = _context.PurchaseReturns
                    .Select(q => new GetPurchaseReturnResponse
                    {
                        returnId = q.ReturnId,
                        supplierName = q.Supplier.SupplierName,
                        returnRefNo = q.ReturnRefNo,
                        returnDate = q.ReturnDate,
                        totalReturnBill = q.TotalReturnBill,
                        totalReturnPaid = q.TotalReturnPaid,
                        totalSupplierPendingPayement = q.TotalReturnBill - q.TotalReturnPaid,
                        CreatedBy = q.CreatedBy,
                    })
                    .AsQueryable();
                if (getPurchaseReturnRequest.fromDate.HasValue && getPurchaseReturnRequest.toDate.HasValue)
                {
                    var startDate = getPurchaseReturnRequest.fromDate.Value.Date;
                    var toDate = getPurchaseReturnRequest.toDate.Value.Date;
                    purchaseReturnQuery = purchaseReturnQuery.Where(q => q.returnDate >= startDate && q.returnDate <= toDate);
                }
                switch (getPurchaseReturnRequest.searchByType)
                {
                    case 1:
                        purchaseReturnQuery = purchaseReturnQuery.Where(q => q.supplierName.Contains(getPurchaseReturnRequest.searchByValue));
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
                var purchaseReturn = await purchaseReturnQuery.ToListAsync();

                if (purchaseReturn.Count == 0)
                {
                    var noDataResponse = new SuccessResponse
                    {
                        status = true,
                        data = purchaseReturn,

                    };
                    return Ok(noDataResponse);
                }

                var successResponse = new SuccessResponse
                {
                    status = true,
                    data = purchaseReturn
                };

                return Ok(successResponse);

               
               
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("GetPurchaseReturnById")]
        public async Task<IActionResult> GetPurchaseReturnById([FromBody] GetPurchaseReturnByIdRequest getPurchaseReturnByIdRequest, [FromHeader(Name = "userId")] int userId)
        {
            try
            {
                bool purchaseReturnExists = await _context.PurchaseReturns
         .AnyAsync(p => p.ReturnId == getPurchaseReturnByIdRequest.returnId);

                if (purchaseReturnExists)
                {
                    var purchaseReturnById = await _context.PurchaseReturns
                        .Where(p => p.ReturnId == getPurchaseReturnByIdRequest.returnId)
                        .Include(p => p.PurchaseReturnRecords)
                            .ThenInclude(pr => pr.Product)
                        .Include(p => p.PurchaseReturnPaymentRecords)
                        .Include(p => p.Supplier)
                        .Select(p => new GetPurchaseReturnByIdResponse
                        {
                            returnId = p.ReturnId,
                            supplierName = p.Supplier.SupplierName,
                            returnDate = p.ReturnDate,
                            supplierId = p.SupplierIdFk,
                            returnRefNo = p.ReturnRefNo,
                            totalReturnBill = p.TotalReturnBill,
                            totalReturnPaid = p.TotalReturnPaid,
                            totalSupplierPendingPayement = p.TotalReturnBill- p.TotalReturnPaid,
                            returnProductData = p.PurchaseReturnRecords.Select(d => new ReturnProductDataDTO
                            {
                                productName = d.Product.ProductName,
                                productId = d.ProductIdFk,
                                batchId = d.BatchIdFk,
                                batchNo = _context.Batches
                                            .Where(b => b.BatchId == d.BatchIdFk)
                                            .Select(b => b.BatchNo)
                                            .FirstOrDefault(),
                                mrpPerPack = (int)(_context.Batches
                                                   .Where(b => b.BatchId == d.BatchIdFk)
                                                   .Select(b => b.MrpPerPack)
                                                   .FirstOrDefault()),
                                returnQuantity = d.ReturnQuantity
                            }).ToList(),
                            paymentDetails = p.PurchaseReturnPaymentRecords.Select(pr => new RequestResponseModel.ResponseModel.RetrunPaymentDetails
                            {
                                paymentId = pr.PaymentId,
                                recieverName = pr.ReceiverName,
                                recieverContact = pr.ReceiverContact,
                                paymentMethod = pr.PaymentMethod,
                                amount = pr.Amount,
                                paymentDate = pr.PaymentDate,
                            }).ToList()
                        })
                        .FirstOrDefaultAsync();

                    var successResponse = new SuccessResponse
                    {
                        status = true,
                        data = purchaseReturnById
                    };

                    return Ok(successResponse);
                }
                else
                {
                    var errorResponse = new RequestResponseModel.ErrorResponse
                    {
                        message = Constants.Errors.Messages.RETRUN_ID_DOES_NOT_EXISTS_MESSAGE,
                        code = Constants.Errors.Codes.RETURN_ID_DOES_NOT_EXISTS_ERROR_CODE
                    };
                    var failureResponse = new RequestResponseModel.FailureResponse
                    {
                        status = false,
                        error = errorResponse
                    };

                    return NotFound(failureResponse);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPost("AddPurchaseReturn")]
        public IActionResult AddPurchaseReturn([FromBody] AddPurchaseReturnRequest addPurchaseReturnRequest, [FromHeader(Name = "userId")] int userId)
        {
            try
            {

                    var newReturnRefNo = GenerateNewReturnRefNo();
                    PurchaseReturn newPurchaseReturn = new PurchaseReturn();
                    newPurchaseReturn.SupplierIdFk = addPurchaseReturnRequest.supplierId;
                    newPurchaseReturn.ReturnRefNo = newReturnRefNo;
                    newPurchaseReturn.ReturnDate = addPurchaseReturnRequest.returnDate;
                    newPurchaseReturn.TotalReturnBill = addPurchaseReturnRequest.totalReturnBill;
                    newPurchaseReturn.TotalReturnPaid = addPurchaseReturnRequest.totalReturnPaid;
                    newPurchaseReturn.CreatedBy = userId;
                    newPurchaseReturn.CreatedAt = DateTime.Now;

                    if (addPurchaseReturnRequest.supplierId == 0)
                    {
                        errorResponse = new RequestResponseModel.ErrorResponse();
                        errorResponse.message = Constants.Errors.Messages.SUPPLIER_ID_CAN_NOT_BE_BLANK;
                        errorResponse.code = Constants.Errors.Codes.SUPPLIER_ID_CAN_NOT_BE_BLANK_ERROR_CODE;
                        failureResponse = new RequestResponseModel.FailureResponse();
                        failureResponse.status = false;
                        failureResponse.error = errorResponse;
                        return BadRequest(failureResponse);
                    }
                    if (newReturnRefNo == null)
                    {
                        errorResponse = new RequestResponseModel.ErrorResponse();
                        errorResponse.message = Constants.Errors.Messages.RETURN_REF_NO_CAN_NOT_BE_BLANK;
                        errorResponse.code = Constants.Errors.Codes.RETURN_REF_NO_CAN_NOT_BE_BLANK_ERROR_CODE;
                        failureResponse = new RequestResponseModel.FailureResponse();
                        failureResponse.status = false;
                        failureResponse.error = errorResponse;
                        return BadRequest(failureResponse);
                    }
                    if (addPurchaseReturnRequest.returnDate == null)
                    {
                        errorResponse = new RequestResponseModel.ErrorResponse();
                        errorResponse.message = Constants.Errors.Messages.PURCHASE_RETURN_DATE_CAN_NOT_BE_BLANK;
                        errorResponse.code = Constants.Errors.Codes.RETURN_DATE_CAN_NOT_BE_BLANK_ERROR_CODE;
                        failureResponse = new RequestResponseModel.FailureResponse();
                        failureResponse.status = false;
                        failureResponse.error = errorResponse;
                        return BadRequest(failureResponse);
                    }

                    _context.PurchaseReturns.Add(newPurchaseReturn);
                    _context.SaveChanges();

                    newPurchaseReturn = _context.PurchaseReturns.Find(newPurchaseReturn.ReturnId);


                    foreach (var totalReturnDataDTO in addPurchaseReturnRequest.totalReturnDataDTO)
                    {
                        Product product = _context.Product.Find(totalReturnDataDTO.productId);


                        if (product == null)
                        {

                            errorResponse = new RequestResponseModel.ErrorResponse();
                            errorResponse.message = Constants.Errors.Messages.PRODUCT_ID_FK_DOES_NOT_EXISTS_MESSAGE;
                            errorResponse.code = Constants.Errors.Codes.PRODUCT_ID_FK_DOES_NOT_EXISTS_ERROR_CODE;
                            failureResponse = new RequestResponseModel.FailureResponse();
                            failureResponse.status = false;
                            failureResponse.error = errorResponse;
                            return NotFound(failureResponse);

                        }

                        Batch batch = _context.Batches.Find(totalReturnDataDTO.batchId);

                        if (batch == null)
                        {

                            errorResponse = new RequestResponseModel.ErrorResponse();
                            errorResponse.message = Constants.Errors.Messages.BATCH_ID_FK_DOES_NOT_EXISTS_MESSAGE;
                            errorResponse.code = Constants.Errors.Codes.BATCH_ID_FK_DOES_NOT_EXISTS_ERROR_CODE;
                            failureResponse = new RequestResponseModel.FailureResponse();
                            failureResponse.status = false;
                            failureResponse.error = errorResponse;
                            return NotFound(failureResponse);
                        }

                        if (totalReturnDataDTO.productId == 0)
                        {
                            errorResponse = new RequestResponseModel.ErrorResponse();
                            errorResponse.message = Constants.Errors.Messages.PRODUCT_ID_FK_CAN_NOT_BE_BLANK;
                            errorResponse.code = Constants.Errors.Codes.PRODUCT_ID_FK_CAN_NOT_BE_BLANK_ERROR_CODE;
                            failureResponse = new RequestResponseModel.FailureResponse();
                            failureResponse.status = false;
                            failureResponse.error = errorResponse;
                            return BadRequest(failureResponse);
                        }
                        if (totalReturnDataDTO.batchId == 0)
                        {
                            errorResponse = new RequestResponseModel.ErrorResponse();
                            errorResponse.message = Constants.Errors.Messages.BATCH_ID_FK_CAN_NOT_BE_BLANK;
                            errorResponse.code = Constants.Errors.Codes.BATCH_ID_FK_CAN_NOT_BE_BLANK_ERROR_CODE;
                            failureResponse = new RequestResponseModel.FailureResponse();
                            failureResponse.status = false;
                            failureResponse.error = errorResponse;
                            return BadRequest(failureResponse);
                        }
                        if (totalReturnDataDTO.returnQuantity == null)
                        {
                            errorResponse = new RequestResponseModel.ErrorResponse();
                            errorResponse.message = Constants.Errors.Messages.RETURN_QUANTITY_CAN_NOT_BE_BLANK;
                            errorResponse.code = Constants.Errors.Codes.RETURN_QUANTITY_CAN_NOT_BE_BLANK_ERROR_CODE;
                            failureResponse = new RequestResponseModel.FailureResponse();
                            failureResponse.status = false;
                            failureResponse.error = errorResponse;
                            return BadRequest(failureResponse);
                        }

                        PurchaseReturnRecord purchaseReturnRecord = new PurchaseReturnRecord();
                    purchaseReturnRecord.PurchaseReturn = newPurchaseReturn;
                    purchaseReturnRecord.Product = product;
                    purchaseReturnRecord.Batch = batch;
                    purchaseReturnRecord.BatchIdFk = batch.BatchId;
                    purchaseReturnRecord.ReturnQuantity =  totalReturnDataDTO.returnQuantity;
                    product.Quantity = product.Quantity - totalReturnDataDTO.returnQuantity;
                    purchaseReturnRecord.Amount = totalReturnDataDTO.amount;
                    //do mynus here
                    purchaseReturnRecord.CreatedBy = userId;
                        purchaseReturnRecord.CreatedAt = DateTime.Now;
                        _context.PurchaseReturnRecord.Add(purchaseReturnRecord);
                        _context.SaveChanges();
                    }

                if (addPurchaseReturnRequest.paymentDetails != null && addPurchaseReturnRequest.paymentDetails.Any())
                {
                    foreach (var addPaymentDetails in addPurchaseReturnRequest.paymentDetails)
                    {

                        if (addPaymentDetails.recieverName == null)
                        {
                            errorResponse = new RequestResponseModel.ErrorResponse();
                            errorResponse.message = Constants.Errors.Messages.RECEIVER_NAME_CAN_NOT_BE_BLANK;
                            errorResponse.code = Constants.Errors.Codes.RECEIVER_NAME_CAN_NOT_BE_BLANK_ERROR_CODE;
                            failureResponse = new RequestResponseModel.FailureResponse();
                            failureResponse.status = false;
                            failureResponse.error = errorResponse;
                            return BadRequest(failureResponse);

                        }
                        if (addPaymentDetails.recieverContact == null)
                        {
                            errorResponse = new RequestResponseModel.ErrorResponse();
                            errorResponse.message = Constants.Errors.Messages.RECEIVER_CONTACT_CAN_NOT_BE_BLANK;
                            errorResponse.code = Constants.Errors.Codes.RECEIVER_CONTACT_CAN_NOT_BE_BLANK_ERROR_CODE;
                            failureResponse = new RequestResponseModel.FailureResponse();
                            failureResponse.status = false;
                            failureResponse.error = errorResponse;
                            return BadRequest(failureResponse);
                        }
                        if (addPaymentDetails.paymentDate == null)
                        //search this date time is not null check validations
                        {
                            errorResponse = new RequestResponseModel.ErrorResponse();
                            errorResponse.message = Constants.Errors.Messages.PAYMENT_DATE_CAN_NOT_BE_BLANK;
                            errorResponse.code = Constants.Errors.Codes.PAYMENT_DATE_CAN_NOT_BE_BLANK_ERROR_CODE;
                            failureResponse = new RequestResponseModel.FailureResponse();
                            failureResponse.status = false;
                            failureResponse.error = errorResponse;
                            return BadRequest(failureResponse);
                        }
                        if (addPaymentDetails.amount == 0)
                        {
                            errorResponse = new RequestResponseModel.ErrorResponse();
                            errorResponse.message = Constants.Errors.Messages.AMOUNT_CAN_NOT_BE_BLANK;
                            errorResponse.code = Constants.Errors.Codes.AMOUNT_CAN_NOT_BE_BLANK_ERROR_CODE;
                            failureResponse = new RequestResponseModel.FailureResponse();
                            failureResponse.status = false;
                            failureResponse.error = errorResponse;
                            return BadRequest(failureResponse);
                        }
                        if (addPaymentDetails.paymentMethod == 0)
                        {
                            errorResponse = new RequestResponseModel.ErrorResponse();
                            errorResponse.message = Constants.Errors.Messages.PAYMENT_METHOD_CAN_NOT_BE_BLANK;
                            errorResponse.code = Constants.Errors.Codes.PAYMENT_METHOD_CAN_NOT_BE_BLANK_ERROR_CODE;
                            failureResponse = new RequestResponseModel.FailureResponse();
                            failureResponse.status = false;
                            failureResponse.error = errorResponse;
                            return BadRequest(failureResponse);
                        }

                        PurchaseReturnPaymentRecord payment = new PurchaseReturnPaymentRecord();
                        payment.ReceiverName = addPaymentDetails.recieverName;
                        payment.Purchase = newPurchaseReturn;
                        payment.ReceiverContact = addPaymentDetails.recieverContact;
                        payment.PaymentDate = (DateTime)addPaymentDetails.paymentDate;
                        payment.Amount = addPaymentDetails.amount;
                        payment.PaymentMethod = addPaymentDetails.paymentMethod;
                        payment.CreatedBy = userId;
                        payment.CreatedAt = DateTime.Now;
                        _context.PurchaseReturnPayment.Add(payment);
                        _context.SaveChanges();
                    }
                }

                var successResponse = new SuccessResponse();
                successResponse.data = new
                {
                    id = newPurchaseReturn.ReturnId,
                    message = Constants.SuccessMessages.PURCHASE_RETURN_SAVED_MESSAGE,

                };
                successResponse.status = true;

                    return Ok(successResponse);
               
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        private string GenerateNewReturnRefNo()
        {
            var maxReturnRefNo = _context.PurchaseReturns.Max(c => (int?)c.ReturnId);
            if (maxReturnRefNo.HasValue)
            {
                return "RR-" + (maxReturnRefNo + 1);
            }

            return "RR-1";
        }

        /*
         {
  "supplierIdFk": 1,
  "returnRefNo": "1001",
  "returnDate": "2023-10-20T10:37:10.965Z",
  "totalBill": 200,
  "totalReturnDataDTO": [
    {
      "productIdFk": 2,
      "batchIdFk": 3,
      "returnQuantity": 2
    }
  ]
} 
         */

        [HttpPost("UpdatePurchaseReturn")]
        public IActionResult UpdatePurchaseReturn([FromBody] UpdatePurchaseReturnRequest updatePurchaseReturnRequest, [FromHeader(Name = "userId")] int userId)
        {
            try
            {
                var existingPurchaseReturn = _context.PurchaseReturns.FirstOrDefault(r => r.ReturnId == updatePurchaseReturnRequest.returnId);
                if (existingPurchaseReturn != null)
                {
                    existingPurchaseReturn.SupplierIdFk = updatePurchaseReturnRequest.supplierId;
                    existingPurchaseReturn.ReturnDate = updatePurchaseReturnRequest.returnDate;
                    existingPurchaseReturn.TotalReturnBill = updatePurchaseReturnRequest.totalReturnBill;
                    existingPurchaseReturn.TotalReturnPaid = updatePurchaseReturnRequest.totalReturnPaid;
                    existingPurchaseReturn.UpdatedBy = userId;
                    existingPurchaseReturn.UpdatedAt = DateTime.Now;
                    var existingPurchaseReturnRecord = _context.PurchaseReturnRecord.Where(rf => rf.ReturnIdFk == existingPurchaseReturn.ReturnId).ToList();
                    _context.SaveChanges();
                   

                    foreach (var updateTotalReturnProductDataDTOproductDto in updatePurchaseReturnRequest.returnProductData)
                    {
                        Product product = _context.Product.Find(updateTotalReturnProductDataDTOproductDto.productId);
                        if (product == null)
                        {

                            errorResponse = new RequestResponseModel.ErrorResponse();
                            errorResponse.message = Constants.Errors.Messages.PRODUCT_ID_DOES_NOT_EXISTS_MESSAGE;
                            errorResponse.code = Constants.Errors.Codes.PRODUCT_CATEGORY_ID_DOES_NOT_EXISTS_ERROR_CODE;
                            failureResponse = new RequestResponseModel.FailureResponse();
                            failureResponse.status = false;
                            failureResponse.error = errorResponse;
                            return NotFound(failureResponse);
                        }

                        Batch batch = _context.Batches.Find(updateTotalReturnProductDataDTOproductDto.batchId);

                        if (batch == null)
                        {

                            errorResponse = new RequestResponseModel.ErrorResponse();
                            errorResponse.message = Constants.Errors.Messages.BATCH_ID_FK_DOES_NOT_EXISTS_MESSAGE;
                            errorResponse.code = Constants.Errors.Codes.BATCH_ID_FK_DOES_NOT_EXISTS_ERROR_CODE;
                            failureResponse = new RequestResponseModel.FailureResponse();
                            failureResponse.status = false;
                            failureResponse.error = errorResponse;
                            return NotFound(failureResponse);
                        }

                       


                        switch (updateTotalReturnProductDataDTOproductDto.opType)
                        {
                            case 1: // Add
                                PurchaseReturnRecord purchaseReturnRecord = new PurchaseReturnRecord();
                                purchaseReturnRecord.PurchaseReturn = existingPurchaseReturn;
                                purchaseReturnRecord.Product = product;
                                purchaseReturnRecord.Batch = batch;
                                purchaseReturnRecord.BatchIdFk = updateTotalReturnProductDataDTOproductDto.batchId;
                                purchaseReturnRecord.ReturnQuantity = updateTotalReturnProductDataDTOproductDto.returnQuantity;
                                product.Quantity = product.Quantity - updateTotalReturnProductDataDTOproductDto.returnQuantity;
                                purchaseReturnRecord.CreatedBy = userId;
                                purchaseReturnRecord.CreatedAt = DateTime.Now;
                                _context.PurchaseReturnRecord.Add(purchaseReturnRecord);
                                break;

                            case 2: //Update
                                var existingReturnRecord = existingPurchaseReturnRecord.FirstOrDefault(r => r.ProductIdFk == updateTotalReturnProductDataDTOproductDto.productId);
                                if(existingReturnRecord != null)
                                {
                                    existingReturnRecord.ReturnQuantity = updateTotalReturnProductDataDTOproductDto.returnQuantity;
                                    product.Quantity = product.Quantity - updateTotalReturnProductDataDTOproductDto.returnQuantity;
                                    existingReturnRecord.BatchIdFk = updateTotalReturnProductDataDTOproductDto.batchId;
                                    existingReturnRecord.Amount = updateTotalReturnProductDataDTOproductDto.amount;
                                    existingReturnRecord.UpdatedBy = userId;
                                    existingReturnRecord.UpdatedAt = DateTime.Now;
                                }
                                break;

                                case 3://Delete 
                                var recordToDelete = existingPurchaseReturnRecord.FirstOrDefault(r => r.ProductIdFk == updateTotalReturnProductDataDTOproductDto.productId);
                                if (recordToDelete != null)
                                {
                                    _context.PurchaseReturnRecord.Remove(recordToDelete);
                                }
                                break;


                        }
                        _context.SaveChanges();

                    }

                    foreach (var paymentDetails in updatePurchaseReturnRequest.paymentDetails )
                    {
                        PurchaseReturnPaymentRecord purchaseReturnPaymentRecord = _context.PurchaseReturnPayment.Find(paymentDetails.paymentId);
                        if (purchaseReturnPaymentRecord != null) 
                        {
                            switch (paymentDetails.opType)
                            {
                                case 2: // Update
                                    purchaseReturnPaymentRecord.ReceiverName = paymentDetails.recieverName;
                                    purchaseReturnPaymentRecord.ReceiverContact = paymentDetails.recieverContact;
                                    purchaseReturnPaymentRecord.PaymentMethod = paymentDetails.paymentMethod;
                                    purchaseReturnPaymentRecord.Amount = paymentDetails.amount;
                                    purchaseReturnPaymentRecord.PaymentDate = paymentDetails.paymentDate;
                                    purchaseReturnPaymentRecord.UpdatedBy = userId;
                                    purchaseReturnPaymentRecord.UpdatedAt = DateTime.Now;
                                    break;
                                case 3: // Delete
                                    _context.PurchaseReturnPayment.Remove(purchaseReturnPaymentRecord);
                                    break;
                            }
                        }

                        else
                        {
                            switch (paymentDetails.opType)
                            {
                                case 1: // Add
                                    PurchaseReturnPaymentRecord payment = new PurchaseReturnPaymentRecord();
                                    payment.ReceiverName = paymentDetails.recieverName;
                                    payment.ReceiverContact = paymentDetails.recieverContact;
                                    payment.Purchase = existingPurchaseReturn;
                                    payment.ReceiverContact = paymentDetails.recieverContact;
                                    payment.PaymentDate = (DateTime)paymentDetails.paymentDate;
                                    payment.Amount = paymentDetails.amount;
                                    payment.PaymentMethod = paymentDetails.paymentMethod;
                                    payment.CreatedBy = userId;
                                    payment.CreatedAt = DateTime.Now;
                                    _context.PurchaseReturnPayment.Add(payment);
                                   
                                    break;
                            }
                        }
                        _context.SaveChanges();
                    }


                    var successResponse = new SuccessResponse();
                    successResponse.data = Constants.SuccessMessages.PURCHASE_RETURN_UPDATED_MESSAGE;
                    successResponse.status = true;

                    return Ok(successResponse);
                }
                errorResponse = new ErrorResponse();
                errorResponse.message = Constants.Errors.Messages.FAILED_TO_UPDATE_PURCHASE_RETURN_MESSAGE;
                errorResponse.code = Constants.Errors.Codes.FAILED_TO_UPDATE_PURCHASE_RETURN_DATA_ERROR_CODE;
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

        [HttpPost("DeletePurchaseReturn")]

        public async Task<IActionResult> DeletePurchaseReturn([FromBody] DeletePurchaseReturnRequest deletePurchaseReturnRequest, [FromHeader(Name = "userId")] int userId)
        {
            try
            {
                var existingPurchaseReturn = _context.PurchaseReturns.Include(qt => qt.PurchaseReturnRecords).Include(qt => qt.PurchaseReturnPaymentRecords)
                    .FirstOrDefault(p => p.ReturnId == deletePurchaseReturnRequest.ReturnId);
                if (existingPurchaseReturn != null)
                {
                    _context.PurchaseReturnRecord.RemoveRange(existingPurchaseReturn.PurchaseReturnRecords);

                    _context.PurchaseReturns.Remove(existingPurchaseReturn);

                    await _context.SaveChangesAsync();

                    var success = new SuccessResponse
                    {

                        status = true,
                        data = new
                        {
                            message = Constants.SuccessMessages.PURCHASE_RETURN_DELETED_MESSAGE
                        }

                    };
                    return Ok(success);
                }
                else
                {
                    throw new FailedToDeletePurchaseReturn();
                }

            }
            catch (FailedToDeletePurchaseReturn ex)
            {

                errorResponse = new ErrorResponse();
                errorResponse.message = Constants.Errors.Messages.FAILED_TO_DELETE_PURCHASE_RETURN_MESSAGE;
                errorResponse.code = Constants.Errors.Codes.FAILED_TO_DELETE_PURCHASE_RETURN_DATA_ERROR_CODE;
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

