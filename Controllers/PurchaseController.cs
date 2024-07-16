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
using static HospitalMgmtService.RequestResponseModel.RequestModel.AddPurchaseReturnRequest;
using static HospitalMgmtService.Controllers.CustomExceptions;
using Microsoft.CodeAnalysis;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace HospitalMgmtService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    // [Authorize]
    public class PurchaseController : ControllerBase
    {


        private readonly DBContext _context;
        private SuccessResponse successResponse = new SuccessResponse();
        private FailureResponse failureResponse = new FailureResponse();
        private ErrorResponse errorResponse = new ErrorResponse();

        public PurchaseController(DBContext context)
        {
            _context = context;
        }


        [HttpPost("GetPurchases")]
        public async Task<IActionResult> GetPurchases([FromBody] GetPurchasesRequest getPurchasesRequest, [FromHeader(Name = "userId")] int userId)
        {
            try
            {
                var purchaseQuery = _context.Purchases
                    .Select(q => new GetAllPurchasesResponse
                    {
                        purchaseId = q.PurchaseId,
                        suppliername = q.Supplier.SupplierName,
                        purchaseDate = q.PurchaseDate,
                        createdAt = q.CreatedAt,
                        createdBy = q.CreatedBy,
                        invoiceNumber = q.InvoiceNumber,
                        totalBill = q.TotalBill,
                        totalPaid = q.TotalPaid
                    })
                    .AsQueryable();
                if (getPurchasesRequest.fromDate.HasValue && getPurchasesRequest.toDate.HasValue)
                {
                    var startDate = getPurchasesRequest.fromDate.Value.Date;
                    var toDate = getPurchasesRequest.toDate.Value.Date;
                    purchaseQuery = purchaseQuery.Where(q => q.purchaseDate >= startDate && q.purchaseDate <= toDate);
                }

                switch (getPurchasesRequest.searchByType)
                {
                    case 1:
                        purchaseQuery = purchaseQuery.Where(q => q.suppliername.Contains(getPurchasesRequest.searchByValue));
                        break;
                    case 2:
                        purchaseQuery = purchaseQuery.Where(q => q.invoiceNumber.Contains(getPurchasesRequest.searchByValue));
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
                        var purchase = await purchaseQuery.ToListAsync();
         
                if (purchase.Count == 0)
                {
                    var noDataResponse = new SuccessResponse
                    {
                        status = true,
                        data = purchase,

                    };
                    return Ok(noDataResponse);
                }

                var successResponse = new SuccessResponse
                {
                    status = true,
                    data = purchase
                };
           
                return Ok(successResponse);
            }
                
  
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPost ("GetPurchaseDocuments")]
        public async Task<IActionResult> GetPurchaseDocuments(GetPurchaseByIdRequest getPurchaseByIdRequest, [FromHeader(Name = "userId")] int userId)
        {
            try
            {
                var purchase = await _context.Purchases.Include(q => q.PurchaseDocuments)
                    .Include(q => q.PurchaseRecords)
                    .Where(q => q.PurchaseId == getPurchaseByIdRequest.purchaseId)
                    .Select(q => new GetPurchaseDocumentResponse
                    {
                        documents = q.PurchaseDocuments.Select(p => new GetPurchaseDocumentDTO
                        {
                            documentId = p.PurchaseDocumentId,
                            documentType = p.DocumentTypes,
                            documentName = p.DocumentName,

                        }).ToList(),
                    }).FirstOrDefaultAsync();

                if (purchase != null)
                {
                    var successResponse = new SuccessResponse();

                    successResponse.status = true;
                    successResponse.data = purchase;
                    return Ok(successResponse);
                }
                else
                {
                    errorResponse = new ErrorResponse();
                    errorResponse.message = Constants.Errors.Messages.PURCHASE_ID_DOES_NOT_EXISTS_MESSAGE;
                    errorResponse.code = Constants.Errors.Codes.PURCHASE_ID_DOES_NOT_EXISTS_ERROR_CODE;
                    failureResponse = new FailureResponse();
                    failureResponse.status = false;
                    failureResponse.error = errorResponse;
                    return NotFound(failureResponse);
                }

            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);

            }
        }



        [HttpPost("GetPurchaseById")]
        public async Task<IActionResult> GetPurchaseById(GetPurchaseByIdRequest getPurchaseByIdRequest, [FromHeader(Name = "userId")] int userId)
        {
            try
            {
                var purchase = await _context.Purchases.Include(q => q.PurchaseDocuments)
                    .Include(q => q.PurchaseRecords)
                    .Where(q => q.PurchaseId == getPurchaseByIdRequest.purchaseId)
                    .Select(q => new GetPurchaseByIdResponsr
                    {

                        purchaseId = q.PurchaseId,
                        supplierId = q.Supplier.SupplierId,
                        suppliername = q.Supplier.SupplierName,
                        purchaseDate = q.PurchaseDate,
                        createdAt = q.CreatedAt,
                        createdBy = q.CreatedBy,
                        invoiceNumber = q.InvoiceNumber,
                        totalBill = q.TotalBill,
                        totalPaid = q.TotalPaid,

                        productDetails = q.PurchaseRecords.Select(p => new PurchaseProductDTO
                        {
                            productId = p.ProductIdFk,
                            productName = p.Product.ProductName,
                            batchId = p.BatchIdFk,
                            batchNo = p.Batch.BatchNo,
                            expiryDate = p.Batch.ExpiryDate,
                            quantity = p.OrderQuantity,
                            mrpPerPack = p.Batch.MrpPerPack,
                            packOf = p.Batch.PackOf,
                            discountPercent = p.Product.DiscountPercent

                        }).ToList(),

                         documents = q.PurchaseDocuments.Select(p => new GetPurchaseDocumentDTO
                         {
                             documentId = p.PurchaseDocumentId,
                             documentType = p.DocumentTypes,
                             documentName = p.DocumentName,

                         }).ToList(),

                        paymentDetails = q.PurchasePayments.Select(p => new PurchasePaymentDTO
                        {
                             PaymentId= p.PaymentId,
                             recieverName=p.ReceiverName,
                             recieverContact = p.ReceiverContact,
                            paymentMethod = p.PaymentMethod,
                            amount = p.Amount,
                            paymentDate = p.PaymentDate,
                        }).ToList()
                       


                    }).FirstOrDefaultAsync();

                if (purchase != null)
                {
                    var successResponse = new SuccessResponse();

                    successResponse.status = true;
                    successResponse.data = purchase;
                    return Ok(successResponse);
                }
                else
                {
                    errorResponse = new ErrorResponse();
                    errorResponse.message = Constants.Errors.Messages.PURCHASE_ID_DOES_NOT_EXISTS_MESSAGE;
                    errorResponse.code = Constants.Errors.Codes.PURCHASE_ID_DOES_NOT_EXISTS_ERROR_CODE;
                    failureResponse = new FailureResponse();
                    failureResponse.status = false;
                    failureResponse.error = errorResponse;
                    return NotFound(failureResponse);
                }

            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);

            }
        }

        [HttpPost("AddPurchase")]
        public IActionResult AddPurchase([FromBody] AddPurchaseRequest addPurchaseRequest, [FromHeader(Name = "userId")] int userId)
        {
            try
            {
                if (addPurchaseRequest.supplierId == 0)
                {                   
                    throw new SupplierContactNoCannotBeBlank();
                }
                if (addPurchaseRequest.totalBill == 0)
                {                    
                    throw new TotalBillCannotBeBlank();
                }
               
                if (addPurchaseRequest.purchaseDate == DateTime.MinValue)
                {
                    throw new PurchaseDateCannotBeBlank();
                }
                    Purchase newPurchase = new Purchase();
                     newPurchase.InvoiceNumber = GenerateNewInvoiceNumber();
                    newPurchase.SupplierIdFk = addPurchaseRequest.supplierId;
                    newPurchase.TotalBill = addPurchaseRequest.totalBill;
                     newPurchase.TotalPaid = addPurchaseRequest.totalPaid;
                    //newPurchase.TotalPaid = addPurchaseRequest.addPaymentDetails.Sum(payment => payment.amount);
                    //newPurchase.TotalPaid = totalPaid;
                    newPurchase.PurchaseDate = addPurchaseRequest.purchaseDate;
                    newPurchase.CreatedBy = userId;
                    newPurchase.CreatedAt = DateTime.Now;
                   
                    _context.Purchases.Add(newPurchase);
                    _context.SaveChanges();

                   


                    foreach (var addProductDetails in addPurchaseRequest.addProductDetails)
                    {
                        Product product = _context.Product.Find(addProductDetails.productId);


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

                        Batch batch = _context.Batches.FirstOrDefault(b => b.BatchNo == addProductDetails.batchNo);

                        if (batch == null)
                        {
                            Batch newbatch = new Batch();
                            newbatch.BatchNo = addProductDetails.batchNo;
                            newbatch.ExpiryDate = (DateTime)addProductDetails.expiryDate;
                            newbatch.PackOf = addProductDetails.packOf;
                            newbatch.MrpPerPack = addProductDetails.mrpPerPack;
                            newbatch.ProductIdFk = addProductDetails.productId;
                            newbatch.CreatedAt = DateTime.Now; ;
                            newbatch.CreatedBy = userId;
                            _context.Batches.Add(newbatch);
                            _context.SaveChanges();
                            batch = _context.Batches.Find(newbatch.BatchId);

                        }
                        
                        PurchaseRecord purchaseRecord = new PurchaseRecord();
                        purchaseRecord.Purchase = newPurchase;
                        purchaseRecord.Product = product;
                        purchaseRecord.Batch = batch;
                       purchaseRecord.OrderQuantity = addProductDetails.quantity;
                        product.Quantity = product.Quantity + addProductDetails.quantity;
                        purchaseRecord.CreatedBy = userId;
                        purchaseRecord.CreatedAt = DateTime.Now;
                        _context.PurchaseRecords.Add(purchaseRecord);
                        _context.SaveChanges();
                    }

                    //add if statement in  purchase payment
                    if (addPurchaseRequest.addPaymentDetails != null && addPurchaseRequest.addPaymentDetails.Any())
                    {
                        foreach (var addPaymentDetails in addPurchaseRequest.addPaymentDetails)
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

                            PurchasePayment payment = new PurchasePayment();
                            payment.Purchase = newPurchase;
                            payment.ReceiverName = addPaymentDetails.recieverName;
                            payment.ReceiverContact = addPaymentDetails.recieverContact;
                            payment.PaymentDate = (DateTime)addPaymentDetails.paymentDate;
                            payment.Amount = addPaymentDetails.amount;
                            payment.PaymentMethod = addPaymentDetails.paymentMethod;
                            payment.CreatedBy = userId;
                            payment.CreatedAt = DateTime.Now;
                            _context.Purchasepayment.Add(payment);
                            _context.SaveChanges();
                        }
                  
                }
                    successResponse.status = true;
                    successResponse.data = new
                    {
                        id = newPurchase.PurchaseId,
                        message = Constants.SuccessMessages.PURCHASE_SAVED_MESSAGE,

                    };
                    return Ok(successResponse);
                
                
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
           
        }
        private string GenerateNewInvoiceNumber()
        {
            var maxPurchaseId = _context.Purchases.Max(c => (int?)c.PurchaseId);
            if (maxPurchaseId.HasValue)
            {
                return "ap-" + (maxPurchaseId + 1);
            }

            return "ap-1";
        }

        [HttpPost("UpdatePurchase")]
        public IActionResult UpdatePurchase([FromBody] UpdatePurchaseRequest updatePurchaseRequest, [FromHeader(Name = "userId")] int userId)
        {
            try
            {
                var existingPurchase = _context.Purchases.FirstOrDefault(r => r.PurchaseId == updatePurchaseRequest.purchaseId);
                if (existingPurchase != null)
                {
                    existingPurchase.SupplierIdFk = updatePurchaseRequest.supplierId;
                    existingPurchase.PurchaseDate = updatePurchaseRequest.purchaseDate;
                    existingPurchase.TotalBill = updatePurchaseRequest.totalBill;
                    existingPurchase.TotalPaid = updatePurchaseRequest.totalPaid;
                    //existingPurchase.TotalPaid = updatePurchaseRequest.updatePurchasePaymentDetails.Sum(payment => payment.amount);
                    existingPurchase.UpdatedBy = userId;
                    existingPurchase.UpdatedAt = DateTime.Now;
                }
                var existingPurchaseRecord = _context.PurchaseRecords.Where(rf => rf.PurchaseIdFk == existingPurchase.PurchaseId).ToList();
                _context.SaveChanges();
                foreach (var updatePurchaseProductDetails in updatePurchaseRequest.updatePurchaseProductDetails)
                {
                    Product product = _context.Product.Find(updatePurchaseProductDetails.productId);
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

                    Batch batch = _context.Batches.FirstOrDefault(b => b.BatchId == updatePurchaseProductDetails.batchId);

                    if (batch == null)
                    {

                        Batch newbatch = new Batch();
                        newbatch.BatchNo = updatePurchaseProductDetails.batchNo;
                        newbatch.ExpiryDate = (DateTime)updatePurchaseProductDetails.expiryDate;
                        newbatch.PackOf = updatePurchaseProductDetails.packOf;
                        newbatch.MrpPerPack = updatePurchaseProductDetails.mrpPerPack;
                        newbatch.ProductIdFk = updatePurchaseProductDetails.productId;
                        newbatch.CreatedAt = DateTime.Now;
                        newbatch.CreatedBy = userId;
                        _context.Batches.Add(newbatch);
                        _context.SaveChanges();
                        batch = _context.Batches.Find(newbatch.BatchId);
                    }
                    //else
                    //{

                    //    batch.PackOf = updatePurchaseProductDetails.packOf;
                    //    batch.MrpPerPack = updatePurchaseProductDetails.mrpPerPack;
                    //    batch.UpdatedAt = DateTime.Now;
                    //    batch.UpdatedBy = userId;
                    //    _context.SaveChanges();
                    //}

                    //PurchaseRecord purchaseRecord = new PurchaseRecord();
                    //purchaseRecord.Purchase = existingPurchase;
                    //product.Quantity = product.Quantity + updatePurchaseProductDetails.quantity;
                    //purchaseRecord.Product = product;
                    ////purchaseRecord.Batch = batch;                        
                    //purchaseRecord.OrderQuantity = updatePurchaseProductDetails.quantity;
                    //purchaseRecord.CreatedBy = userId;
                    //purchaseRecord.CreatedAt = DateTime.Now;
                    //_context.SaveChanges();

                    switch (updatePurchaseProductDetails.opType)
                    {
                        case 1: // Add
                            PurchaseRecord purchaseRecord = new PurchaseRecord();
                            purchaseRecord.Purchase = existingPurchase;
                            product.Quantity = product.Quantity + updatePurchaseProductDetails.quantity;
                            purchaseRecord.Product = product;
                            purchaseRecord.Batch = batch;
                            purchaseRecord.OrderQuantity = updatePurchaseProductDetails.quantity;
                            product.Quantity = product.Quantity + updatePurchaseProductDetails.quantity;
                            purchaseRecord.CreatedBy = userId;
                            purchaseRecord.CreatedAt = DateTime.Now;
                            _context.SaveChanges();
                            _context.PurchaseRecords.Add(purchaseRecord);
                            break;

                        case 2: //Update
                                 var existingRecord = existingPurchase.PurchaseRecords.FirstOrDefault(r => r.ProductIdFk == updatePurchaseProductDetails.productId);
                            if (existingRecord != null)
                            {
                                existingRecord.OrderQuantity = updatePurchaseProductDetails.quantity;
                                product.Quantity = product.Quantity + updatePurchaseProductDetails.quantity;
                                batch.BatchId = updatePurchaseProductDetails.batchId;
                                batch.BatchNo = updatePurchaseProductDetails.batchNo;
                                batch.PackOf = updatePurchaseProductDetails.packOf;
                                batch.MrpPerPack = updatePurchaseProductDetails.mrpPerPack;

                            }
                            break;

                        case 3://Delete 
                            var recordToDelete = existingPurchase.PurchaseRecords.FirstOrDefault(r => r.ProductIdFk == updatePurchaseProductDetails.productId);
                            if (recordToDelete != null)
                            {
                                _context.PurchaseRecords.Remove(recordToDelete);
                            }
                            break;


                    }
                    _context.SaveChanges();
                }

                if (updatePurchaseRequest.updatePurchasePaymentDetails != null && updatePurchaseRequest.updatePurchasePaymentDetails.Any())
                {

                    foreach (var updatePurchasePaymentDetails in updatePurchaseRequest.updatePurchasePaymentDetails)
                    {
                        PurchasePayment updatePurchasePayment = _context.Purchasepayment.Find(updatePurchasePaymentDetails.paymentId);

                        switch (updatePurchasePaymentDetails.opType)
                        {
                            case 1: // Add
                                PurchasePayment payment = new PurchasePayment();
                                payment.PurchaseIdFk = existingPurchase.PurchaseId;
                                payment.ReceiverName = updatePurchasePaymentDetails.recieverName;
                                payment.ReceiverContact = updatePurchasePaymentDetails.recieverContact;

                                payment.PaymentDate = (DateTime)updatePurchasePaymentDetails.paymentDate;
                                payment.Amount = updatePurchasePaymentDetails.amount;
                                payment.PaymentMethod = updatePurchasePaymentDetails.paymentMethod;
                                payment.CreatedAt = DateTime.Now;
                                payment.CreatedBy = userId;
                                _context.Purchasepayment.Add(payment);

                                break;

                            case 2: // Update


                                updatePurchasePayment.ReceiverName = updatePurchasePaymentDetails.recieverName;
                                updatePurchasePayment.ReceiverContact = updatePurchasePaymentDetails.recieverContact;
                                updatePurchasePayment.PaymentDate = (DateTime)updatePurchasePaymentDetails.paymentDate;
                                updatePurchasePayment.Amount = updatePurchasePaymentDetails.amount;
                                updatePurchasePayment.PaymentMethod = updatePurchasePaymentDetails.paymentMethod;
                                updatePurchasePayment.UpdatedBy = userId;
                                updatePurchasePayment.UpdatedAt = DateTime.Now;

                                break;

                            case 3: // Delete
                                _context.Purchasepayment.Remove(updatePurchasePayment);
                                break;



                        }

                        _context.SaveChanges();
                    }


                }
                var successResponse = new SuccessResponse();
                successResponse.data = Constants.SuccessMessages.PURCHASE_UPDATED_MESSAGE;
                successResponse.status = true;
                return Ok(successResponse);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



        [HttpPost("DeletePurchase")]
        public async Task<IActionResult> DeletePurchase([FromBody] DeletePurchaseRequest deletePurchaseRequest, [FromHeader(Name = "userId")] int userId)
        {
            try
            {
                var existingPurchase = _context.Purchases
                    .Include(qt => qt.PurchaseRecords)
                    .Include(qt => qt.PurchasePayments)
                    .FirstOrDefault(p => p.PurchaseId == deletePurchaseRequest.purchaseId);

                if (existingPurchase != null)
                {
                    if (existingPurchase.PurchasePayments.Any())
                    {
                        errorResponse = new ErrorResponse();
                        errorResponse.message = Constants.Errors.Messages.CAN_NOT_DELETE_PURCHASE_WITH_PAYMENTS_MESSAGE;
                        errorResponse.code = Constants.Errors.Codes.CAN_NOT_DELETE_PURCHASE_WITH_PAYMENTS_ERROR_CODE;
                        failureResponse = new FailureResponse();
                        failureResponse.status = false;
                        failureResponse.error = errorResponse;
                        return BadRequest(failureResponse);                        
                    }

                    _context.PurchaseRecords.RemoveRange(existingPurchase.PurchaseRecords);
                    _context.Purchases.Remove(existingPurchase);

                    await _context.SaveChangesAsync();

                    var success = new SuccessResponse
                    {
                        status = true,
                        data = new
                        {
                            message = Constants.SuccessMessages.PURCHASE_DELETED_MESSAGE
                        }
                    };

                    return Ok(success);
                }
                else
                {
                    throw new FailedToDeletePurchase();
                }
            }
            catch (FailedToDeletePurchase ex)
            {
                errorResponse = new ErrorResponse();
                errorResponse.message = Constants.Errors.Messages.FAILED_TO_DELETE_PURCHASE_MESSAGE;
                errorResponse.code = Constants.Errors.Codes.FAILED_TO_DELETE_PURCHASE_DATA_ERROR_CODE;
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

        [HttpPost("GetPurchaseByProductIdAndSupplierId")]
        public async Task<IActionResult> GetPurchaseByProductIdAndSupplierId(GetPurchaseByProductIdAndSupplierIdRequest getPurchaseByProductIdAndSupplierId, [FromHeader(Name = "userId")] int userId)
        {

            long? productId = getPurchaseByProductIdAndSupplierId.ProductId;
            long? supplierId = getPurchaseByProductIdAndSupplierId.SupplierId;

            try
            {
                var purchase =await (from pu in _context.Purchases
                               join pr in _context.PurchaseRecords on pu.PurchaseId equals pr.PurchaseIdFk
                               join p in _context.Product on pr.ProductIdFk equals p.ProductId
                               join s in _context.Suppliers on pu.SupplierIdFk equals s.SupplierId
                               join b in _context.Batches on pr.BatchIdFk equals b.BatchId
                               where pr.ProductIdFk == productId && pu.SupplierIdFk == supplierId
                               group new { pu, pr, p, s, b } by p.ProductName into grouped
                               select new
                               {
                                   purchaseId = grouped.FirstOrDefault().pu.PurchaseId,
                                   ProductId = grouped.FirstOrDefault().pr.ProductIdFk,
                                   ProductName = grouped.Key,
                                   supplierId = grouped.FirstOrDefault().s.SupplierId,
                                   supplierName = grouped.FirstOrDefault().s.SupplierName,
                                   orderQuantity = grouped.FirstOrDefault().pr.OrderQuantity,
                                   purchaseDate = grouped.FirstOrDefault().pu.PurchaseDate,
                                   totalPaid = grouped.FirstOrDefault().pu.TotalPaid,
                                   Batches = grouped.Select(g => new
                                   {
                                       g.b.BatchId,
                                       g.b.BatchNo,
                                       g.b.ExpiryDate,
                                       g.b.PackOf,
                                       g.b.MrpPerPack
                                   }).ToList()
                               }).ToListAsync();

                var result = purchase.FirstOrDefault();

                if (result != null)
                {
                    var successResponse = new SuccessResponse();

                    successResponse.status = true;
                    successResponse.data = result;
                    return Ok(successResponse);
                }
                else
                {
                    errorResponse = new ErrorResponse();
                    errorResponse.message = Constants.Errors.Messages.PURCHASE_ID_DOES_NOT_EXISTS_MESSAGE;
                    errorResponse.code = Constants.Errors.Codes.PURCHASE_ID_DOES_NOT_EXISTS_ERROR_CODE;
                    failureResponse = new FailureResponse();
                    failureResponse.status = false;
                    failureResponse.error = errorResponse;
                    return NotFound(failureResponse);
                }

            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);

            }
        }
    }
}


