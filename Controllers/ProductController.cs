using AutoMapper;
using HospitalMgmtService.Database;
using HospitalMgmtService.Model;
using HospitalMgmtService.RequestResponseModel;
using HospitalMgmtService.RequestResponseModel.RequestModel;
using HospitalMgmtService.RequestResponseModel.ResponseModel;
using Humanizer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using static HospitalMgmtService.Controllers.CustomExceptions;
using static HospitalMgmtService.RequestResponseModel.Constants.Errors;

namespace HospitalMgmtService.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly DBContext _context;
        private SuccessResponse successResponse = new SuccessResponse();
        private FailureResponse failureResponse = new FailureResponse();
        private ErrorResponse errorResponse = new ErrorResponse();
        private readonly ILogger<ProductController> _logger;

        public ProductController(DBContext context, ILogger<ProductController> logger)
        {
            _context = context;
            _logger = logger;

        }



        [HttpPost("GetProducts")]
        public async Task<IActionResult> GetProducts([FromBody] GetProductsRequest getProducts, [FromHeader(Name = "userId")] int userId)
        {
            try
            {
                var productsQuery = _context.Set<Product>() // Assuming Product is your entity
                    .Include(p => p.ProductCategory)
                    .Include(p => p.Brand)
                    .Select(x => new GetProductResponse
                    {
                        productId = x.ProductId,
                        productName = x.ProductName,
                        productCategoryId = x.ProductCategoryIdFk,
                        productCategoryName = x.ProductCategory.ProductCategoryName,
                        brandName = x.Brand.BrandName,
                        brandId = x.BrandIdFk,
                        quantity = x.Quantity,
                        alertQuantity = x.AlertQuantity,
                        sequenceStoring = x.SequenceStoring,
                        discountPercent = x.DiscountPercent,
                        customField1 = x.CustomField1,
                        customField2 = x.CustomField2,
                        CustomField3 = x.CustomField3,
                        createdBy = x.CreatedBy,
                        createdAt = x.CreatedAt
                    })
                    .AsQueryable();

                // Apply date filters if both dates are provided
                if (getProducts.fromtDate.HasValue && getProducts.totDate.HasValue)
                {
                    DateTime startDate = getProducts.fromtDate.Value;
                    DateTime endDate = getProducts.totDate.Value.AddDays(1); // To include the end date

                    productsQuery = productsQuery.Where(p => p.createdAt >= startDate && p.createdAt < endDate);
                }

                switch (getProducts.searchByType)
                {
                    case 1:
                        productsQuery = productsQuery.Where(product => product.productName.Contains(getProducts.searchByValue));
                        break;
                    case 2:
                        productsQuery = productsQuery.Where(product => product.productCategoryName.Contains(getProducts.searchByValue));
                        break;
                    case 3:
                        productsQuery = productsQuery.Where(product => product.brandName.Contains(getProducts.searchByValue));
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

                var products = await productsQuery.ToListAsync();

                // Check if no products found and return a message
                if (products.Count == 0)
                {
                    var noDataResponse = new SuccessResponse
                    {
                        status = true,
                        data = products,
                       
                    };
                    return Ok(noDataResponse);
                }

                var successResponse = new SuccessResponse
                {
                    status = true,
                    data = products
                };
                return Ok(successResponse);
            }
            catch (InvalidSearchType ex)
            {
                _logger.LogError($" Exception Message: {ex.Message}");
                _logger.LogError($" Exception Stack Trace: {ex.StackTrace}");
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
            catch (Exception ex)
            {
                _logger.LogError($" Exception Message: {ex.Message}");
                _logger.LogError($" Exception Stack Trace: {ex.StackTrace}");
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("GetProductById")]
        public async Task<IActionResult> GetProductById([FromBody] GetProductByIdRequest getProductById, [FromHeader(Name = "userId")] int userId)
        {
            try
            {
                var products = await _context.Product
                                  .Where(x => x.ProductId == getProductById.ProductId)
                                  .Select(x => new GetProductResponse
                                  {
                                      productId = x.ProductId,
                                      productName = x.ProductName,
                                      productCategoryId = x.ProductCategoryIdFk,
                                      productCategoryName = x.ProductCategory.ProductCategoryName,
                                      brandName = x.Brand.BrandName,
                                      brandId = x.BrandIdFk,
                                      quantity = x.Quantity,
                                      alertQuantity = x.AlertQuantity,
                                      sequenceStoring = x.SequenceStoring,
                                      discountPercent = x.DiscountPercent,
                                      customField1 = x.CustomField1,
                                      customField2 = x.CustomField2,
                                      CustomField3 = x.CustomField3,
                                  })
                                  .FirstOrDefaultAsync();
                if (products != null)
                {
                    var successResponse = new SuccessResponse();
                    successResponse.status = true;
                    successResponse.data = products;
                    return Ok(successResponse);
                }
                else
                {
                    throw new ProductIdDoesnotExists();
                }

            }
            catch (ProductIdDoesnotExists ex)
            {

                _logger.LogError($" Exception Message: {ex.Message}");
                _logger.LogError($" Exception Stack Trace: {ex.StackTrace}");
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
                _logger.LogError($" Exception Message: {ex.Message}");
                _logger.LogError($" Exception Stack Trace: {ex.StackTrace}");
                return BadRequest(ex.Message);
            }

        }

        [HttpPost("GetPurchaseHistoryByProductId")]
        public async Task<IActionResult> GetPurchaseHistoryByProductId([FromBody] GetPurchaseHistoryByProductIdRequest getPurchaseHistoryByProductIdRequest, [FromHeader(Name = "userId")] int userId)
        {
            try
            {
                var productsQuery = _context.PurchaseRecords
                                      .Where(x => x.ProductIdFk == getPurchaseHistoryByProductIdRequest.productId)
                                      .GroupBy(x => new { x.Product.ProductName, x.Product.Quantity })
                                      .Select(g => new GetPurchaseHistoryByProductIdResponse
                                      {
                                          productName = g.Key.ProductName,
                                          currentStock = g.Key.Quantity,
                                          purchaseHistory = g.Select(x => new PurchaseHistory
                                          {
                                              purchaseDate = x.Purchase.PurchaseDate,
                                              batchNo = x.Batch.BatchNo,
                                              expiryDate = x.Batch.ExpiryDate,
                                              packOf = x.Batch.PackOf,
                                              mrpPerPack = x.Batch.MrpPerPack,
                                              orderQuantity = x.OrderQuantity,
                                              unitPrice = x.Batch.MrpPerPack / x.Batch.PackOf,
                                              totalMRP = x.Product.Quantity * x.Batch.MrpPerPack
                                          }).ToList()
                                      });

                var product = await productsQuery.FirstOrDefaultAsync();

                if (product != null)
                {
                    var successResponse = new SuccessResponse();
                    successResponse.status = true;
                    successResponse.data = product;
                    return Ok(successResponse);
                }
                else
                {
                    throw new ProductIdDoesnotExists();
                }
            }
            catch (ProductIdDoesnotExists ex)
            {

                _logger.LogError($" Exception Message: {ex.Message}");
                _logger.LogError($" Exception Stack Trace: {ex.StackTrace}");
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
                _logger.LogError($" Exception Message: {ex.Message}");
                _logger.LogError($" Exception Stack Trace: {ex.StackTrace}");
                return BadRequest(ex.Message);
            }

        }

        [HttpPost("GetBatchesByProductId")]
        public async Task<IActionResult> GetProductBatchesById([FromBody] GetProductBatchesByIdRequest getProductBatchesById, [FromHeader(Name = "userId")] int userId)
        {
            try
            {
                var productsbatches = await _context.Batches
                                .Where(x => x.ProductIdFk == getProductBatchesById.ProductId)
                                 .Select(x => new GetProductBatchesResponse
                                 {
                                     productId = x.Product.ProductId,
                                     BatchId = x.BatchId,
                                     BatchNo = x.BatchNo,
                                     mrpPerPack = x.MrpPerPack

                                 }).ToListAsync();//this print data in array
               

                if (productsbatches != null)
                {
                    var successResponse = new SuccessResponse();
                    successResponse.status = true;
                    successResponse.data = new { productsbatches }; 
                    return Ok(successResponse);
                }
                else
                {
                    throw new ProductIdDoesnotExists();
                }

            }
            catch (ProductIdDoesnotExists ex)
            {

                _logger.LogError($" Exception Message: {ex.Message}");
                _logger.LogError($" Exception Stack Trace: {ex.StackTrace}");
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
                _logger.LogError($" Exception Message: {ex.Message}");
                _logger.LogError($" Exception Stack Trace: {ex.StackTrace}");
                return BadRequest(ex.Message);
            }

        }  

        [HttpPost("AddProduct")]
        public IActionResult AddProduct([FromBody] AddProductRequest addProduct, [FromHeader(Name = "userId")] int userId)
        {
            try
            {
                var brand = _context.Brand.FirstOrDefault(b => b.BrandId == addProduct.brandId);
                var productCategory = _context.ProductCategory.FirstOrDefault(pc => pc.ProductCategoryId == addProduct.productCategoryId);

                if (brand == null)
                {
                    throw new BrandIdDoesnotExists();

                }
                else if (productCategory == null)
                {
                    throw new ProductCategoryIdDoesnotExists();

                }
                else
                {
                    var existingProduct = _context.Product.FirstOrDefault(p => p.ProductName == addProduct.productName);

                    if (existingProduct == null)
                    {
                        if (string.IsNullOrWhiteSpace(addProduct.productName))
                        {
                            throw new ProductNameCannotBeBlank();
                        }
                        if (addProduct.brandId == null)
                        {

                            throw new BrandIdCannotBeBlank();
                        }

                        if (addProduct.productCategoryId == null)
                        {

                            throw new ProductCategoryIdCannotBeBlank();
                        }

                        if (addProduct.alertQuantity == null)
                        {
                            throw new AlertQuanttyCannotBeBlank();
                        }

                        var newProduct = new Product();

                        newProduct.ProductName = addProduct.productName;
                        newProduct.BrandIdFk = addProduct.brandId;
                        newProduct.ProductCategoryIdFk = addProduct.productCategoryId;
                        newProduct.AlertQuantity = addProduct.alertQuantity;
                        newProduct.SequenceStoring = addProduct.sequenceStoring;
                        newProduct.DiscountPercent = addProduct.discountPercent;
                        newProduct.CustomField1 = addProduct.customeField1;
                        newProduct.CustomField2 = addProduct.customeField2;
                        newProduct.CustomField3 = addProduct.customeField3;
                        newProduct.CreatedAt = DateTime.Now;

                        _context.Product.Add(newProduct);
                        var product = _context.SaveChanges();

                        successResponse.status = true;
                        successResponse.data = new
                        {
                            id = newProduct.ProductId,
                            message = Constants.SuccessMessages.PRODUCT_SAVED_MESSAGE,

                        };
                        return Ok(successResponse);
                    }

                    else
                    {
                        throw new FailedToSaveProduct();
                    }

                }
            }

            catch (AlertQuanttyCannotBeBlank ex)
            {

                _logger.LogError($" Exception Message: {ex.Message}");
                _logger.LogError($" Exception Stack Trace: {ex.StackTrace}");
                errorResponse = new ErrorResponse();
                errorResponse.message = Messages.ALERT_QUANTITY_CAN_NOT_BE_BLANK_MESSAGE;
                errorResponse.code = Codes.ALERT_QUANTITY_CAN_NOT_BE_BLANK_ERROR_CODE;
                failureResponse = new FailureResponse();
                failureResponse.status = false;
                failureResponse.error = errorResponse;
                return BadRequest(failureResponse);

            }
            catch (ProductCategoryIdCannotBeBlank ex)
            {
                _logger.LogError($" Exception Message: {ex.Message}");
                _logger.LogError($" Exception Stack Trace: {ex.StackTrace}");

                errorResponse = new ErrorResponse();
                errorResponse.message = Messages.PRODUCT_CATEGORY_ID_CAN_NOT_BE_BLANK_MESSAGE;
                errorResponse.code = Codes.PRODUCT_CATEGORY_CAN_NOT_BE_BLANK_ERROR_CODE;
                failureResponse = new FailureResponse();
                failureResponse.status = false;
                failureResponse.error = errorResponse;
                return BadRequest(failureResponse);
            }
            catch (BrandIdCannotBeBlank ex)
            {

                _logger.LogError($" Exception Message: {ex.Message}");
                _logger.LogError($" Exception Stack Trace: {ex.StackTrace}");
                errorResponse = new ErrorResponse();
                errorResponse.message = Messages.BRAND_ID_CAN_NOT_BE_BLANK_MESSAGE;
                errorResponse.code = Codes.BRAND_ID_CAN_NOT_BE_BLANK;
                failureResponse = new FailureResponse();
                failureResponse.status = false;
                failureResponse.error = errorResponse;
                return BadRequest(failureResponse);

            }
            catch (ProductCategoryIdDoesnotExists ex)
            {
                _logger.LogError($" Exception Message: {ex.Message}");
                _logger.LogError($" Exception Stack Trace: {ex.StackTrace}");
                errorResponse = new ErrorResponse();
                errorResponse.message = Messages.PRODUCT_CATEGORY_ID_DOES_NOT_EXISTS_MESSAGE;
                errorResponse.code = Codes.PRODUCT_CATEGORY_ID_DOES_NOT_EXISTS_ERROR_CODE;
                failureResponse = new FailureResponse();
                failureResponse.status = false;
                failureResponse.error = errorResponse;
                return NotFound(failureResponse);
            }
            catch (BrandIdDoesnotExists ex)
            {
                _logger.LogError($" Exception Message: {ex.Message}");
                _logger.LogError($" Exception Stack Trace: {ex.StackTrace}");
                errorResponse = new ErrorResponse();
                errorResponse.message = Messages.BRAND_ID_DOES_NOT_EXISTS_MESSAGE;
                errorResponse.code = Codes.BRAND_ID_DOES_NOT_EXISTS_ERROR_CODE;
                failureResponse = new FailureResponse();
                failureResponse.status = false;
                failureResponse.error = errorResponse;
                return NotFound(failureResponse);
            }
            catch (ProductNameCannotBeBlank ex)
            {

                _logger.LogError($" Exception Message: {ex.Message}");
                _logger.LogError($" Exception Stack Trace: {ex.StackTrace}");
                errorResponse = new ErrorResponse();
                errorResponse.message = Messages.PRODUCT_NAME_CAN_NOT_BE_BLANK;
                errorResponse.code = Codes.PRODUCT_NAME_CAN_NOT_BE_BLANK_ERROR_CODE;
                failureResponse = new FailureResponse();
                failureResponse.status = false;
                failureResponse.error = errorResponse;
                return BadRequest(failureResponse);
            }
            catch (FailedToSaveProduct ex)
            {

                _logger.LogError($" Exception Message: {ex.Message}");
                _logger.LogError($" Exception Stack Trace: {ex.StackTrace}");
                errorResponse = new ErrorResponse();
                errorResponse.message = Messages.FAILED_TO_SAVE_PRODUCT_MESSAGE;
                errorResponse.code = Codes.FAILED_TO_SAVE_PRODUCT_DATA_ERROR_CODE;
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

        [HttpPost("UpdateProduct")]
        public IActionResult UpdateProduct([FromBody] UpdateProductRequest productRequest, [FromHeader(Name = "userId")] int userId)
        {
            try
            {
                var existingProduct = _context.Product.FirstOrDefault(p => p.ProductId == productRequest.productId);


                var brand = _context.Brand.FirstOrDefault(b => b.BrandId == productRequest.brandId);
                var productCategory = _context.ProductCategory.FirstOrDefault(pc => pc.ProductCategoryId == productRequest.productCategoryId);

                if (brand == null)
                {
                    throw new BrandIdDoesnotExists();
                }
                else if (productCategory == null)
                {
                    throw new ProductCategoryIdDoesnotExists();
                }
                else
                {
                    if (existingProduct != null)
                    {
                        existingProduct.ProductName = productRequest.productName;
                        existingProduct.BrandIdFk = productRequest.brandId;
                        existingProduct.ProductCategoryIdFk = productRequest.productCategoryId;
                        existingProduct.AlertQuantity = productRequest.alertQuantity;
                        existingProduct.SequenceStoring = productRequest.sequenceStoring;
                        existingProduct.DiscountPercent = productRequest.discountPercent;
                        existingProduct.CustomField1 = productRequest.customField1;
                        existingProduct.CustomField2 = productRequest.customField2;
                        existingProduct.CustomField3 = productRequest.customField3;
                        existingProduct.UpdatedAt = DateTime.Now;
                        existingProduct.UpdatedBy = userId;

                        _context.Product.Update(existingProduct);
                        var products = _context.SaveChanges();
                        var successResponse = new SuccessResponse();
                        successResponse.data = new
                        {
                            message = Constants.SuccessMessages.PRODUCT_UPDATED_MESSAGE,
                        };
                        successResponse.status = true;
                        return Ok(successResponse);
                    }
                    else
                    {
                        throw new FailedToUpdateProduct();
                    }

                }
            }

            catch (BrandIdDoesnotExists ex)
            {
                _logger.LogError($" Exception Message: {ex.Message}");
                _logger.LogError($" Exception Stack Trace: {ex.StackTrace}");
                errorResponse = new ErrorResponse();
                errorResponse.message = Messages.BRAND_ID_DOES_NOT_EXISTS_MESSAGE;
                errorResponse.code = Codes.BRAND_ID_DOES_NOT_EXISTS_ERROR_CODE;
                failureResponse = new FailureResponse();
                failureResponse.status = false;
                failureResponse.error = errorResponse;
                return NotFound(failureResponse);

            }
            catch (ProductCategoryIdDoesnotExists ex)
            {

                _logger.LogError($" Exception Message: {ex.Message}");
                _logger.LogError($" Exception Stack Trace: {ex.StackTrace}");
                errorResponse = new ErrorResponse();
                errorResponse.message = Messages.PRODUCT_CATEGORY_ID_DOES_NOT_EXISTS_MESSAGE;
                errorResponse.code = Codes.PRODUCT_ID_DOES_NOT_EXISTS_ERROR_CODE;
                failureResponse = new FailureResponse();
                failureResponse.status = false;
                failureResponse.error = errorResponse;
                return NotFound(failureResponse);
            }
            catch (FailedToUpdateProduct ex)
            {

                _logger.LogError($" Exception Message: {ex.Message}");
                _logger.LogError($" Exception Stack Trace: {ex.StackTrace}");
                errorResponse = new ErrorResponse();
                errorResponse.message = Messages.FAILED_TO_UPDATE_PRODUCT_MESSAGE;
                errorResponse.code = Codes.FAILED_TO_UPDATE_PRODUCT_DATA_ERROR_CODE;
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

        [HttpPost("DeleteProduct")]
        public IActionResult DeleteProduct([FromBody] DeleteProductRequest productRequest, [FromHeader(Name = "userId")] int userId)
        {
            try
            {
                var existingProduct = _context.Product.FirstOrDefault(p => p.ProductId == productRequest.productId);

                if (existingProduct != null)
                {
                    _context.Product.Remove(existingProduct);
                    var products = _context.SaveChanges();

                    var successResponse = new SuccessResponse();
                    successResponse.data = new
                    {
                        message = Constants.SuccessMessages.PRODUCT_DELETED_MESSAGE
                    };
                    successResponse.status = true;
                    return Ok(successResponse);
                }

                else
                {
                    throw new FailedToDeleteProduct();
                }

            }
            catch (FailedToDeleteProduct ex)
            {

                _logger.LogError($" Exception Message: {ex.Message}");
                _logger.LogError($" Exception Stack Trace: {ex.StackTrace}");
                errorResponse = new ErrorResponse();
                errorResponse.message = Messages.FAILED_TO_DELETE_PRODUCT_MESSAGE;
                errorResponse.code = Codes.FAILED_TO_DELETE_PRODUCT_DATA_ERROR_CODE;
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


        [HttpPost("GetProductBySupplierId")]
        public async Task<IActionResult> GetProductBySupplierId([FromBody] GetProductBySupplierIdRequest getProductBySupplierId, [FromHeader(Name = "userId")] int userId)
        {
            try
            {
                var supplierId = getProductBySupplierId.supplierId;  


                var supplierName = _context.Suppliers
                    .Where(c => c.SupplierId == supplierId)
                    .Select(c => c.SupplierName)
                    .FirstOrDefault();

                var products = await (from supplier in _context.Suppliers
                                      join purchase in _context.Purchases on supplier.SupplierId equals purchase.SupplierIdFk
                                      join purchaseRecord in _context.PurchaseRecords on purchase.PurchaseId equals purchaseRecord.PurchaseIdFk
                                      join product in _context.Product on purchaseRecord.ProductIdFk equals product.ProductId
                                      where supplier.SupplierId == supplierId
                                      group product by product.ProductName into grouped
                                      select new
                                      {
                                          ProductId = grouped.FirstOrDefault().ProductId,
                                          ProductName = grouped.Key,
                                          BrandIdFk = grouped.FirstOrDefault().BrandIdFk,
                                          ProductCategoryIdFk = grouped.FirstOrDefault().ProductCategoryIdFk,
                                          AlertQuantity = grouped.FirstOrDefault().AlertQuantity,
                                          Quantity = grouped.FirstOrDefault().Quantity,
                                          SequenceStoring = grouped.FirstOrDefault().SequenceStoring,
                                          DiscountPercent = grouped.FirstOrDefault().DiscountPercent,
                                          CustomField1 = grouped.FirstOrDefault().CustomField1,
                                          CustomField2 = grouped.FirstOrDefault().CustomField2,
                                          CustomField3 = grouped.FirstOrDefault().CustomField3,
                                          CreatedBy = grouped.FirstOrDefault().CreatedBy,
                                          UpdatedBy = grouped.FirstOrDefault().UpdatedBy,
                                          CreatedAt = grouped.FirstOrDefault().CreatedAt,
                                          UpdatedAt = grouped.FirstOrDefault().UpdatedAt
                                      }).ToListAsync();


                // Execute the query and retrieve the results
                var result =  products;

                if (products != null)
                {
                    var successResponse = new SuccessResponse();
                    successResponse.status = true;
                    successResponse.data = result;
                    return  Ok(successResponse);
                }
                else
                {
                    var errorResponse = new ErrorResponse();
                    errorResponse.message = Messages.PRODUCT_ID_DOES_NOT_EXISTS_MESSAGE;
                    errorResponse.code = Codes.FAILED_TO_FETCH_PRODUCT_DATA_ERROR_CODE;
                    var failureResponse = new FailureResponse();
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

        [HttpPost("GetProductByCustomerId")]
        public async Task<IActionResult> GetProductByCustomerId([FromBody] GetProductByCustomerIdRequest GetProductByCustomerId, [FromHeader(Name = "userId")] int userId)
        {
            try
            {
                var CustomerId = GetProductByCustomerId.CustomerId; // Replace 'yourSupplierId' with the actual supplierId you want to query

                var products = await (from customer in _context.Customers
                        join sales in _context.Sales on customer.CustomerId equals sales.CustomerIdFk
                        join salesRecord in _context.SalesRecord on sales.SalesId equals salesRecord.SaleIdFk
                        join product in _context.Product on salesRecord.ProductIdFk equals product.ProductId
                        where customer.CustomerId == CustomerId
                        group product by product.ProductName into grouped
                        select new
                        {
                            ProductId = grouped.FirstOrDefault().ProductId,
                            ProductName = grouped.Key,
                            BrandIdFk = grouped.FirstOrDefault().BrandIdFk,
                            ProductCategoryIdFk = grouped.FirstOrDefault().ProductCategoryIdFk,
                            AlertQuantity = grouped.FirstOrDefault().AlertQuantity,
                            Quantity = grouped.FirstOrDefault().Quantity,
                            SequenceStoring = grouped.FirstOrDefault().SequenceStoring,
                            DiscountPercent = grouped.FirstOrDefault().DiscountPercent,
                            CustomField1 = grouped.FirstOrDefault().CustomField1,
                            CustomField2 = grouped.FirstOrDefault().CustomField2,
                            CustomField3 = grouped.FirstOrDefault().CustomField3,
                            CreatedBy = grouped.FirstOrDefault().CreatedBy,
                            UpdatedBy = grouped.FirstOrDefault().UpdatedBy,
                            CreatedAt = grouped.FirstOrDefault().CreatedAt,
                            UpdatedAt = grouped.FirstOrDefault().UpdatedAt
                        }).ToListAsync();

                // Execute the query and retrieve the results
                var result =  products;

                if (products != null)
                {
                    var successResponse = new SuccessResponse();
                    successResponse.status = true;
                    successResponse.data = result;
                    return Ok(successResponse);
                }
                else
                {
                    var errorResponse = new ErrorResponse();
                    errorResponse.message = Messages.PRODUCT_ID_DOES_NOT_EXISTS_MESSAGE;
                    errorResponse.code = Codes.FAILED_TO_FETCH_PRODUCT_DATA_ERROR_CODE;
                    var failureResponse = new FailureResponse();
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


    }
}











