using HospitalMgmtService.Database;
using HospitalMgmtService.Model;
using HospitalMgmtService.RequestResponseModel;
using HospitalMgmtService.RequestResponseModel.RequestModel;
using HospitalMgmtService.RequestResponseModel.ResponseModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static HospitalMgmtService.RequestResponseModel.Constants.Errors;

namespace HospitalMgmtService.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class PurchaseOrderController : ControllerBase
    {
        private readonly DBContext _context;
        private SuccessResponse successResponse = new SuccessResponse();
        private FailureResponse failureResponse = new FailureResponse();
        private ErrorResponse errorResponse = new ErrorResponse();

        public PurchaseOrderController(DBContext context)
        {
            _context = context;
        }
        /*[HttpGet("GetAllPurchaseOrders")]
        public async Task<IActionResult> GetAllPurchaseOrders([FromHeader(Name = "userId")] int userId)
        {
            try
            {
                List<GetAllPurchaseOrderResponse> purchaseOrder = await _context.PurchaseOrder.
                    Select(q => new GetAllPurchaseOrderResponse
                    {
                        PoId = q.PoId,
                        PoNumber = q.PoNumber,
                        PoDate = q.PoDate,
                        SupplierIdFk = q.SupplierIdFk,
                        PoStatus = q.PoStatus,
                        purchaseOrderProductData = q.PoRecords.Select(pr => new RequestResponseModel.ResponseModel.PurchaseOrderProductData
                        {
                            ProductIdFk = pr.ProductIdFk,
                            OrderQuantity = pr.OrderQuantity,
                            PurchaseNote = pr.PurchaseNote,
                        }).ToList()
                    }).ToListAsync();
              
                if (purchaseOrder != null )
                {
                    var successResponse = new SuccessResponse();
                    successResponse.status = true;
                    successResponse.data = purchaseOrder;
                    return Ok(successResponse);
                }

                else
                {
                    errorResponse = new ErrorResponse();
                    errorResponse.message = Messages.FAILED_TO_FETCH_PURCHASE_ORDER_DATA_MESSAGE;
                    errorResponse.code = Codes.FAILED_TO_FETCH_PURCHASE_ORDER_DATA_ERROR_CODE;
                    failureResponse = new FailureResponse();
                    failureResponse.status = false;
                    failureResponse.error = errorResponse;
                    return BadRequest(failureResponse);
                }
            }
            catch (Exception ex)
            {
                errorResponse = new ErrorResponse();
                errorResponse.message = Messages.INTERNAL_SERVER_ERROR;
                errorResponse.code = Codes.INTERNAL_SERVER_ERROR_CODE;
                failureResponse = new FailureResponse();
                failureResponse.status = false;
                failureResponse.error = errorResponse;
                return BadRequest(failureResponse);
            }
        }*/
        [HttpPost("GetPurchaseOrders")]
        public async Task<IActionResult> GetPurchaseOrders([FromBody] GetPurchaseOrdersRequest getPurchaseOrderRequest, [FromHeader(Name = "userId")] int userId)
        {
            try
            {
                var purchaseOrderQuery = _context.PurchaseOrder
                    .Select(q => new GetPurchaseOrdersResponse
                    {
                        poId = q.PoId,
                        poNumber = q.PoNumber,
                        poDate = q.PoDate,
                        supplierName = q.Supplier.SupplierName,
                        supplierId = q.SupplierIdFk,
                        poStatus = q.PoStatus,
                        purchaseNote = _context.PoRecords
                                    .Where(pr => pr.PoIdFk == q.PoId)
                                    .Select(pr => pr.PurchaseNote)
                                    .FirstOrDefault(),
                        productData = q.PoRecords.Select(pr => new PurchaseOrdersProductDataResponse
                        {
                            productName = pr.Product.ProductName,
                            productId = pr.ProductIdFk,
                            orderQuantity = pr.OrderQuantity,
                            quantity = pr.Product.Quantity,
                        }).ToList(),
                    })
                    .AsQueryable();
                if (getPurchaseOrderRequest.searchByType == 1)
                {
                    if (string.IsNullOrEmpty(getPurchaseOrderRequest.searchByValue))
                    {

                    }
                    else
                    {
                        purchaseOrderQuery = purchaseOrderQuery.Where(q => q.supplierName.ToUpper() == getPurchaseOrderRequest.searchByValue.ToUpper());
                    }
                }

                else if (getPurchaseOrderRequest.searchByType == 2)
                {
                    purchaseOrderQuery = purchaseOrderQuery.Where(q => q.poNumber.ToString().Contains(getPurchaseOrderRequest.searchByValue));
                }
                else if (getPurchaseOrderRequest.searchByType == 3)
                {
                    purchaseOrderQuery = purchaseOrderQuery.Where(q => q.supplierName.ToUpper().Contains(getPurchaseOrderRequest.searchByValue.ToUpper()));
                }
                else if (getPurchaseOrderRequest.searchByType == 4)
                {
                    purchaseOrderQuery = purchaseOrderQuery.Where(q => q.poStatus.ToUpper().Contains(getPurchaseOrderRequest.searchByValue.ToUpper()));
                }
                else if (getPurchaseOrderRequest.searchByType == 5)
                {
                    purchaseOrderQuery = purchaseOrderQuery.Where(q =>
                        q.poDate >= getPurchaseOrderRequest.fromtDate && q.poDate <= getPurchaseOrderRequest.totDate);
                }
                else
                {
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

                var purchaseOrders = await purchaseOrderQuery.ToListAsync();

                var successResponse = new SuccessResponse
                {
                    status = true,
                    data = purchaseOrders
                };
                return Ok(successResponse);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("GetPurchaseOrderById")]
        public async Task<IActionResult> GetPurchaseOrderById([FromBody] GetPurchaseOrderByIdRequest getPurchaseOrderByIdRequest, [FromHeader(Name = "userId")] int userId)

        {
            try
            {
                var purchaseOrderById = await _context.PurchaseOrder.Where(p => p.PoId == getPurchaseOrderByIdRequest.PoId)
                .Include(p => p.PoRecords)
                .Select(p => new GetPurchaseOrderByIdResponse
                {
                    poId = p.PoId,
                    poNumber = p.PoNumber,
                    poDate = p.PoDate,
                    supplierId = p.SupplierIdFk,
                    supplierName = p.Supplier.SupplierName,
                    poStatus = p.PoStatus,
                    purchaseNote = _context.PoRecords
                                    .Where(pr => pr.PoIdFk == p.PoId)
                                    .Select(pr => pr.PurchaseNote)
                                    .FirstOrDefault(),
                productData = p.PoRecords.Select(pr => new RequestResponseModel.ResponseModel.PurchaseOrderByIdProductData
                    {
                        productId = pr.ProductIdFk,
                        productName = pr.Product.ProductName,
                        orderQuantity = pr.OrderQuantity,
                        quantity = pr.Product.Quantity,
                    }).ToList()
                }).FirstOrDefaultAsync();

                if (purchaseOrderById != null)
                {

                    var successResponse = new SuccessResponse();
                    successResponse.status = true;
                    successResponse.data = purchaseOrderById;
                    return Ok(successResponse);
                }
                else
                {
                    errorResponse = new RequestResponseModel.ErrorResponse();
                    errorResponse.message = Constants.Errors.Messages.RETRUN_ID_DOES_NOT_EXISTS_MESSAGE;
                    errorResponse.code = Constants.Errors.Codes.RETURN_ID_DOES_NOT_EXISTS_ERROR_CODE;
                    failureResponse = new RequestResponseModel.FailureResponse();
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
        [HttpPost("AddPurchaseOrder")]
        public IActionResult AddPurchaseOrder([FromBody] AddPurchaseOrderRequest addPurchaseOrderRequest, [FromHeader(Name = "userId")] int userId)
        {
            try
            {
                PurchaseOrder existingPurchaseOrder = _context.PurchaseOrder.Where(r => r.PoNumber == addPurchaseOrderRequest.PoNumber).FirstOrDefault();

                if (existingPurchaseOrder != null)
                {
                    errorResponse = new RequestResponseModel.ErrorResponse();
                    errorResponse.message = Constants.Errors.Messages.PURCHASE_ORDER_ALREADY_EXISTS_MESSAGE;
                    errorResponse.code = Constants.Errors.Codes.PURCHASE_ORDER_ALREADY_EXIST_ERROR_CODE;
                    failureResponse = new RequestResponseModel.FailureResponse();
                    failureResponse.status = false;
                    failureResponse.error = errorResponse;
                    return Conflict(failureResponse);

                }
                else if (existingPurchaseOrder == null)
                {
                    PurchaseOrder newPurchaseOrder = new PurchaseOrder();
                    newPurchaseOrder.PoNumber = GenerateNewPoNumber();
                    newPurchaseOrder.PoDate = addPurchaseOrderRequest.PoDate;
                    newPurchaseOrder.SupplierIdFk = addPurchaseOrderRequest.SupplierIdFk;
                    newPurchaseOrder.PoStatus = addPurchaseOrderRequest.PoStatus;
                    newPurchaseOrder.CreatedBy = userId;
                    newPurchaseOrder.CraetedAt = DateTime.Now;

                    
                    if (addPurchaseOrderRequest.PoDate == null)
                    {
                        errorResponse = new RequestResponseModel.ErrorResponse();
                        errorResponse.message = Constants.Errors.Messages.PURCHASE_ORDER_DATE_CAN_NOT_BE_BLANK;
                        errorResponse.code = Constants.Errors.Codes.PURCHASE_ORDER_DATE_CAN_NOT_BE_BLANK_ERROR_CODE;
                        failureResponse = new RequestResponseModel.FailureResponse();
                        failureResponse.status = false;
                        failureResponse.error = errorResponse;
                        return BadRequest(failureResponse);
                    }
                    if (addPurchaseOrderRequest.SupplierIdFk == 0)
                    {
                        errorResponse = new RequestResponseModel.ErrorResponse();
                        errorResponse.message = Constants.Errors.Messages.SUPPLIER_ID_OF_PO_CAN_NOT_BE_BLANK;
                        errorResponse.code = Constants.Errors.Codes.SUPPLIER_ID_OF_PO_CAN_NOT_BE_BLANK_ERROR_CODE;
                        failureResponse = new RequestResponseModel.FailureResponse();
                        failureResponse.status = false;
                        failureResponse.error = errorResponse;
                        return BadRequest(failureResponse);
                    }
                    if (addPurchaseOrderRequest.PoStatus == null)
                    {
                        errorResponse = new RequestResponseModel.ErrorResponse();
                        errorResponse.message = Constants.Errors.Messages.PURCHASE_ORDER_STATUS_CAN_NOT_BE_BLANK;
                        errorResponse.code = Constants.Errors.Codes.PURCHASE_ORDER_STATUS_CAN_NOT_BE_BLANK_ERROR_CODE;
                        failureResponse = new RequestResponseModel.FailureResponse();
                        failureResponse.status = false;
                        failureResponse.error = errorResponse;
                        return BadRequest(failureResponse);
                    }

                    _context.PurchaseOrder.Add(newPurchaseOrder);
                    _context.SaveChanges();

                    existingPurchaseOrder = _context.PurchaseOrder.Find(newPurchaseOrder.PoId);


                    foreach (var purchaseOrderProductDataDTO in addPurchaseOrderRequest.purchaseOrderProductDataDTO)
                    {
                        Product product = _context.Product.Find(purchaseOrderProductDataDTO.ProductIdFk);


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

                        if (purchaseOrderProductDataDTO.ProductIdFk == 0)
                        {
                            errorResponse = new RequestResponseModel.ErrorResponse();
                            errorResponse.message = Constants.Errors.Messages.PRODUCT_ID_FK_OF_PO_CAN_NOT_BE_BLANK;
                            errorResponse.code = Constants.Errors.Codes.PRODUCT_ID_FK_OF_PO_CAN_NOT_BE_BLANK_ERROR_CODE;
                            failureResponse = new RequestResponseModel.FailureResponse();
                            failureResponse.status = false;
                            failureResponse.error = errorResponse;
                            return BadRequest(failureResponse);
                        }
                        if (purchaseOrderProductDataDTO.OrderQuantity == null)
                        {
                            errorResponse = new RequestResponseModel.ErrorResponse();
                            errorResponse.message = Constants.Errors.Messages.ORDER_QUANTITY_OF_PO_CAN_NOT_BE_BLANK;
                            errorResponse.code = Constants.Errors.Codes.ORDER_QUANTITY_OF_PO_CAN_NOT_BE_BLANK_ERROR_CODE;
                            failureResponse = new RequestResponseModel.FailureResponse();
                            failureResponse.status = false;
                            failureResponse.error = errorResponse;
                            return BadRequest(failureResponse);
                        }

                        PoRecords purchaseOrderRecord = new PoRecords();
                        purchaseOrderRecord.Po = existingPurchaseOrder;
                        purchaseOrderRecord.Product = product;
                        purchaseOrderRecord.OrderQuantity = purchaseOrderProductDataDTO.OrderQuantity;
                        purchaseOrderRecord.PurchaseNote = addPurchaseOrderRequest.PurchaseNote;
                        purchaseOrderRecord.CreatedBy = userId;
                        purchaseOrderRecord.CreatedAt = DateTime.Now;
                        _context.PoRecords.Add(purchaseOrderRecord);
                        _context.SaveChanges();
                    }

                    var successResponse = new SuccessResponse();
                    successResponse.data = Constants.SuccessMessages.PURCHASE_ORDER_SAVED_MESSAGE;
                    successResponse.status = true;

                    return Ok(successResponse);
                }
                else
                {
                    errorResponse = new ErrorResponse();
                    errorResponse.message = Constants.Errors.Messages.FAILED_TO_SAVE_PURCHASE_ORDER_MESSAGE;
                    errorResponse.code = Constants.Errors.Codes.FAILED_TO_SAVE_PURCHASE_ORDER_DATA_ERROR_CODE;
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
        private string GenerateNewPoNumber()
        {
            var maxPurchaseOrderId = _context.PurchaseOrder.Max(c => (int?)c.PoId);
            if (maxPurchaseOrderId.HasValue)
            {
                return "po-" + (maxPurchaseOrderId + 1);
            }

            return "po-1";
        }

        [HttpPost("UpdatePurchaseOrder")]
        public IActionResult UpdatePurchaseOrder([FromBody] UpdatePurchaseOrderRequest updatePurchaseOrderRequest, [FromHeader(Name = "userId")] int userId)
        {
            try
            {
                var existingPurchaseOrder = _context.PurchaseOrder.FirstOrDefault(r => r.PoId == updatePurchaseOrderRequest.PoId);
                if (existingPurchaseOrder != null)
                {
                    existingPurchaseOrder.PoId = updatePurchaseOrderRequest.PoId;
                    existingPurchaseOrder.PoDate = updatePurchaseOrderRequest.PoDate;
                    existingPurchaseOrder.SupplierIdFk = updatePurchaseOrderRequest.SupplierIdFk;
                    existingPurchaseOrder.PoStatus = updatePurchaseOrderRequest.PoStatus;
                    existingPurchaseOrder.UpdatedBy = userId;
                    existingPurchaseOrder.UpdatedAt = DateTime.Now;

                    var existingPurchaseOrderRecord = _context.PoRecords.Where(rf => rf.PoIdFk == existingPurchaseOrder.PoId).ToList();
                    _context.SaveChanges();
                    _context.PoRecords.RemoveRange(existingPurchaseOrderRecord);

                    foreach (var updatePurchaseOrderProductDataDTO in updatePurchaseOrderRequest.updatePurchaseOrderProductDataDTO)
                    {
                        Product product = _context.Product.Find(updatePurchaseOrderProductDataDTO.ProductIdFk);
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

                        PoRecords purchaseOrderRecord = new PoRecords();
                        purchaseOrderRecord.Po = existingPurchaseOrder;
                        purchaseOrderRecord.Product = product;
                        purchaseOrderRecord.OrderQuantity = updatePurchaseOrderProductDataDTO.OrderQuantity;
                        purchaseOrderRecord.PurchaseNote = updatePurchaseOrderRequest.PurchaseNote;
                        purchaseOrderRecord.CreatedBy = userId;
                        purchaseOrderRecord.CreatedAt = DateTime.Now;
                        _context.PoRecords.Add(purchaseOrderRecord);
                        _context.SaveChanges();

                    }

                    var successResponse = new SuccessResponse();
                    successResponse.data = Constants.SuccessMessages.PURCHASE_ORDER_UPDATED_MESSAGE;
                    successResponse.status = true;

                    return Ok(successResponse);
                }
                errorResponse = new ErrorResponse();
                errorResponse.message = Constants.Errors.Messages.FAILED_TO_UPDATE_PURCHASE_ORDER_MESSAGE;
                errorResponse.code = Constants.Errors.Codes.FAILED_TO_UPDATE_PURCHASE_ORDER_DATA_ERROR_CODE;
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

       

        [HttpPost("DeletePurchaseOrder")]

        public async Task<IActionResult> DeletePurchaseOrder([FromBody] DeletePurchaseOrderRequest deletePurchaseOrderRequest, [FromHeader(Name = "userId")] int userId)
        {
            try
            {

                var deletePurchaseOrderFromPORecord = _context.PoRecords.Where(r => r.PoIdFk == deletePurchaseOrderRequest.PoId).ToList();
                var deletePurchaseOrder = _context.PurchaseOrder.Where(r => r.PoId == deletePurchaseOrderRequest.PoId).ToList();

                if (deletePurchaseOrder == null && deletePurchaseOrderFromPORecord==null)
                {

                    errorResponse = new RequestResponseModel.ErrorResponse();

                    errorResponse.message = Constants.Errors.Messages.FAILED_TO_DELETE_PURCHASE_ORDER_MESSAGE;
                    errorResponse.code = Constants.Errors.Codes.FAILED_TO_DELETE_PURCHASE_ORDER_DATA_ERROR_CODE;
                    failureResponse = new RequestResponseModel.FailureResponse();
                    failureResponse.status = false;
                    failureResponse.error = errorResponse;
                    return NotFound(failureResponse);
                }

                _context.PoRecords.RemoveRange(deletePurchaseOrderFromPORecord);
                _context.PurchaseOrder.RemoveRange(deletePurchaseOrder);
                _context.SaveChanges();

                var successResponse = new SuccessResponse();
                successResponse.data = Constants.SuccessMessages.PURCHASE_ORDER_DELETED_MESSAGE;
                successResponse.status = true;

                return Ok(successResponse);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }
        }






































































        //// DI - Dependency Inject of DBContext class using Constructor Type 

        //private readonly DBContext _Context; // Empty variable of DBContext 
        //public PurchaseOrderController(DBContext context)
        //{
        //    _Context = context;
        //}

        ////https://localhost:44379/GetPurchaseOrder
        //[HttpGet("GetPurchaseOrder")]
        ///*[ValidateAntiForgeryToken]*/
        //public IActionResult GetPurchaseOrder()
        //{
        //    try
        //    {
        //        List<PurchaseOrder> records = _Context.PurchaseOrder.ToList();

        //        if (records != null)
        //        {
        //            var success = new APIResponse()
        //            {
        //                data = records,
        //                status = true,
        //                message = "Records Found Successfully"
        //            };
        //            return Ok(success);
        //        }
        //        else
        //        {
        //            var failure = new APIResponse()
        //            {
        //                status = false,
        //                message = "No Records Founds !!!  "
        //            };

        //            return NotFound(failure);
        //        }
        //    }
        //    catch(Exception ex)
        //    {
        //        var error = new APIResponse()
        //        {
        //            status = false,
        //            message = ex.Message
        //        };

        //        return BadRequest(error);
        //    }
        //}

        ////https://localhost:44379/GetPurchaseOrderById?PoId=1
        //[HttpGet("GetPurchaseOrderById")]
        ///*[ValidateAntiForgeryToken]*/
        //public IActionResult GetPurchaseOrderById([FromQuery(Name = "PoId")]long id)
        //{
        //    try
        //    {
        //        var records = _Context.PurchaseOrder.Where(x => x.PoId == id).FirstOrDefault();

        //        if (records != null)
        //        {
        //            var success = new APIResponse()
        //            {
        //                data = records,
        //                status = true,
        //                message = "Match Record Found Successfully"
        //            };
        //            return Ok(success);
        //        }
        //        else
        //        {
        //            var failure = new APIResponse()
        //            {
        //                status = false,
        //                message = "Record Not Match Founds !!!  "
        //            };

        //            return NotFound(failure);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        var error = new APIResponse()
        //        {
        //            status = false,
        //            message = ex.Message
        //        };

        //        return BadRequest(error);
        //    }
        //}

        ////https://localhost:44379/AddPurchaseOrder
        //[HttpPost("AddPurchaseOrder")]
        ///*[ValidateAntiForgeryToken]*/
        //public IActionResult AddPurchaseOrder([FromBody] PurchaseOrder purchaseOrder)
        //{
        //    try
        //    {
        //        _Context.PurchaseOrder.AddAsync(purchaseOrder);
        //        _Context.SaveChangesAsync();

        //        var success = new APIResponse()
        //        {
        //            data = purchaseOrder,
        //            status = true,
        //            message = "Record Added Successfully"
        //        };
        //        return Ok(success);
        //    }
        //    catch (Exception ex)
        //    {
        //        var failure = new APIResponse()
        //        {
        //            status = false,
        //            message = "Failed to Add Record " + ex.Message
        //        };

        //        return NotFound(failure);
        //    }
        //}

        ////https://localhost:44379/UpdatePurchaseOrder?PoId=1
        //[HttpPut("UpdatePurchaseOrder")]
        ///*[ValidateAntiForgeryToken]*/
        //public ActionResult UpdatePurchaseOrder([FromQuery(Name = "PoId")]long id, [FromBody] PurchaseOrder purchaseOrder)
        //{
        //    try
        //    {
        //        var _existing = _Context.PurchaseOrder.Where(x => x.PoId == id).FirstOrDefault();

        //        if (_existing != null)
        //        {
        //            _existing.PoDate = purchaseOrder.PoDate;
        //            _existing.PoNo=purchaseOrder.PoNo;
        //            _existing.PoStatus=purchaseOrder.PoStatus;

        //            _existing.UpdatedAt=purchaseOrder.UpdatedAt;
        //            _existing.UpdatedBy=purchaseOrder.UpdatedBy;

        //            _existing.SupplierIdFk = purchaseOrder.SupplierIdFk;

        //            _Context.SaveChangesAsync();

        //            var success = new APIResponse()
        //            {
        //                data = _existing,
        //                status = true,
        //                message = "Update Successfully"
        //            };
        //            return Ok(success);
        //        }
        //        else
        //        {
        //            var failure = new APIResponse()
        //            {
        //                status = false,
        //                message = "Update Unsuccessfully "
        //            };

        //            return NotFound(failure);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        var error = new APIResponse()
        //        {
        //            status = false,
        //            message = ex.Message
        //        };

        //        return BadRequest(error);
        //    }
        //}

        ////https://localhost:44379/DeletePurchaseOrder?PoId=1
        //[HttpDelete("DeletePurchaseOrder")]
        ///*[ValidateAntiForgeryToken]*/
        //public IActionResult DeletePurchaseOrder([FromQuery(Name = "PoId")] long id)
        //{
        //    try
        //    {
        //        var _existing = _Context.PurchaseOrder.Where(x => x.PoId == id).FirstOrDefault();

        //        if (_existing != null)
        //        {
        //            _Context.PurchaseOrder.Remove(_existing);
        //            _Context.SaveChangesAsync();

        //            var success = new APIResponse()
        //            {
        //                data = _existing,
        //                status = true,
        //                message = "Delete Successfully"
        //            };
        //            return Ok(success);
        //        }
        //        else
        //        {
        //            var failure = new APIResponse()
        //            {
        //                status = false,
        //                message = "Delete Unsuccessfully "
        //            };

        //            return NotFound(failure);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        var error = new APIResponse()
        //        {
        //            status = false,
        //            message = ex.Message
        //        };

        //        return BadRequest(error);
        //    }
        //}

    }
}
