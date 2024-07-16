using HospitalMgmtService.Database;
using HospitalMgmtService.Model;
using HospitalMgmtService.RequestResponseModel;
using HospitalMgmtService.RequestResponseModel.RequestModel;
using HospitalMgmtService.RequestResponseModel.ResponseModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration.UserSecrets;
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
    public class ProductCategoryController : ControllerBase
    {
        private readonly DBContext _context;
        private SuccessResponse successResponse = new SuccessResponse();
        private FailureResponse failureResponse = new FailureResponse();
        private ErrorResponse errorResponse = new ErrorResponse(); 
        private readonly ILogger<ProductCategoryController> _logger;

        public ProductCategoryController(DBContext context, ILogger<ProductCategoryController> logger)
        {
            _context = context;
            logger = _logger;
        }


        [HttpPost("GetProductCategories")]
        public async Task<IActionResult> GetProductCategories([FromBody] GetProductCategoriesRequest getCategories, [FromHeader(Name = "userId")] int userId)
        {
            try
            {
                var categoryQuery = _context.ProductCategory.Select(x => new GetAllProductCategoryResponse
                {
                    categoryId = x.ProductCategoryId,
                    categoryName = x.ProductCategoryName,
                    description = x.CategoryDescription,
                    createdBy =x.CreatedBy,
                    createdAt = x.CreatedAt

                }).AsQueryable();


                if (getCategories.searchByType == 1)
                {
                    categoryQuery = categoryQuery.Where(category => category.categoryName.Contains(getCategories.searchByValue));
                }

                else
                {
                    throw new InvalidSearchType();

                }
                var roles = await categoryQuery.ToListAsync();

                var successResponse = new SuccessResponse();
                successResponse.status = true;
                successResponse.data = roles;
                return Ok(successResponse);

            }
            catch (InvalidSearchType ex)
            {
                _logger.LogError($" Exception Message: {ex.Message}");
                _logger.LogError($" Exception Stack Trace: {ex.StackTrace}");
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
                _logger.LogError($" Exception Message: {ex.Message}");
                _logger.LogError($" Exception Stack Trace: {ex.StackTrace}");
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("GetProductCategoryById")]
        public async Task<IActionResult> GetProductById([FromBody] GetProductCategoryById getProductCategoryById, [FromHeader(Name = "userId")] int userId)
        {
            try
            {
                var productCategory = await _context.ProductCategory
                 .Where(p => p.ProductCategoryId == getProductCategoryById.ProductCategoryId)
                    .Select(p => new GetAllProductCategoryResponse
                    {
                        categoryId = p.ProductCategoryId,
                        categoryName = p.ProductCategoryName,
                        description = p.CategoryDescription,
                        createdBy = p.CreatedBy,
                        createdAt = p.CreatedAt
                    })
                     .FirstOrDefaultAsync();


                if (productCategory != null)
                {
                    var successResponse = new SuccessResponse();
                    successResponse.status = true;
                    successResponse.data = productCategory;
                    return Ok(successResponse);
                }
                else
                {
                    throw new ProductCategoryIdDoesnotExists();
                }

            }
            catch(ProductCategoryIdDoesnotExists ex)
            {

                _logger.LogError($" Exception Message: {ex.Message}");
                _logger.LogError($" Exception Stack Trace: {ex.StackTrace}");
                errorResponse = new ErrorResponse();
                errorResponse.message = Constants.Errors.Messages.PRODUCT_CATEGORY_ID_DOES_NOT_EXISTS_MESSAGE;
                errorResponse.code = Constants.Errors.Codes.PRODUCT_CATEGORY_ID_DOES_NOT_EXISTS_ERROR_CODE;
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
        [HttpPost("AddProductCategory")]
        public async Task<IActionResult> AddProductCategory([FromBody] AddProductCategoryRequest productCategoryRequest, [FromHeader(Name = "userId")] int userId)
        {
            try
            {
                var existingProductCategory = _context.ProductCategory.FirstOrDefault(p => p.ProductCategoryName == productCategoryRequest.categoryName);

                if (existingProductCategory == null)
                {


                    var newProductCategory = new ProductCategory();
                    newProductCategory.ProductCategoryName = productCategoryRequest.categoryName;
                    newProductCategory.CategoryDescription = productCategoryRequest.description;
                    newProductCategory.CreatedBy = userId;
                    newProductCategory.CreatedAt = DateTime.Now;

                    _context.ProductCategory.Add(newProductCategory);
                    var products = _context.SaveChanges();


                    if (products <= 0)
                    { 
                        throw new ProductCategoryAlreadyExists();
                      
                    }
                    else
                    {
                        successResponse.status = true;
                        successResponse.data = new
                        {
                            id = newProductCategory.ProductCategoryId,
                            message = Constants.SuccessMessages.PRODUCT_CATEGORY_SAVED_MESSAGE,

                        };
                        return Ok(successResponse);
                    }

                }
                else
                {
                    throw new FailedToSaveProductCategory();

                }
            }
            catch(FailedToSaveProductCategory ex)
            {

                _logger.LogError($" Exception Message: {ex.Message}");
                _logger.LogError($" Exception Stack Trace: {ex.StackTrace}");
                var failureResponse = new FailureResponse();
                failureResponse.status = false;
                errorResponse = new ErrorResponse();
                errorResponse.message = Constants.Errors.Messages.FAILED_TO_SAVE_PRODUCT_CATEGORY_MESSAGE;
                errorResponse.code = Constants.Errors.Codes.FAILED_TO_SAVE_PRODUCT_CATEGORY_CODE;
                return BadRequest(failureResponse);
            }
            catch (ProductCategoryAlreadyExists ex)
            {

                _logger.LogError($" Exception Message: {ex.Message}");
                _logger.LogError($" Exception Stack Trace: {ex.StackTrace}");
                errorResponse = new ErrorResponse();
                errorResponse.message = Constants.Errors.Messages.PRODUCT_CATEGORY_EXIST_MESSAGE;
                errorResponse.code = Constants.Errors.Codes.PRODUCT_CATEGORY_EXIST_ERROR_CODE;
                failureResponse = new FailureResponse();
                failureResponse.status = false;
                failureResponse.error = errorResponse;
                return Conflict(failureResponse);
            }
            catch (Exception ex)
            {
                _logger.LogError($" Exception Message: {ex.Message}");
                _logger.LogError($" Exception Stack Trace: {ex.StackTrace}");
                return BadRequest(ex.Message);
            }

        }

        [HttpPost("UpdateProductCategory")]
        public async Task<IActionResult> UpdateProductCategory([FromBody] UpdateProductCategoryRequest productCategoryRequest, [FromHeader(Name = "userId")] int userId)
        {
            try
            {
                var existingProductCategory = _context.ProductCategory.FirstOrDefault(p => p.ProductCategoryId == productCategoryRequest.categoryId);


                if (existingProductCategory != null)
                {
                    existingProductCategory.ProductCategoryId = productCategoryRequest.categoryId;
                    existingProductCategory.ProductCategoryName = productCategoryRequest.categoryName;
                    existingProductCategory.CategoryDescription = productCategoryRequest.description;
                    existingProductCategory.UpdatedBy = userId;
                    existingProductCategory.UpdatedAt = DateTime.Now;

                    await _context.SaveChangesAsync();

                    var successResponse = new SuccessResponse();
                    successResponse.status = true;
                    successResponse.data = new
                    {
                        message = Constants.SuccessMessages.PRODUCT_CATEGORY_UPDATED_MESSAGE
                    };
                    return Ok(successResponse);
                }
                else
                {
                    throw new FailedToUpdateProductCategory();
                }

            }
            catch(FailedToUpdateProductCategory ex)
                {

                _logger.LogError($" Exception Message: {ex.Message}");
                _logger.LogError($" Exception Stack Trace: {ex.StackTrace}");
                var failureResponse = new FailureResponse();
                failureResponse.status = false;
                errorResponse = new ErrorResponse();
                errorResponse.message = Constants.Errors.Messages.FAILED_TO_UPDATE_PRODUCT_CATEGORY_MESSAGE;
                errorResponse.code = Constants.Errors.Codes.FAILED_TO_UPDATE_PRODUCT_CATEGORY_CODE;
                return BadRequest(failureResponse);
            } 
            catch (Exception ex)
            {
                _logger.LogError($" Exception Message: {ex.Message}");
                _logger.LogError($" Exception Stack Trace: {ex.StackTrace}");
                return BadRequest(ex.Message);
            }
            finally
            {
            }
        }


        [HttpPost("DeleteProductCategory")]
        public async Task<IActionResult> DeleteProductCategory([FromBody] DeleteProductCategoryRequest productCategoryRequest, [FromHeader(Name = "userId")] int userId)
        {
            try
            {
                var existingProductCategory = _context.ProductCategory.FirstOrDefault(p => p.ProductCategoryId == productCategoryRequest.categoryId);


                if (existingProductCategory != null)
                {
                    _context.ProductCategory.Remove(existingProductCategory);

                    await _context.SaveChangesAsync();

                    var successResponse = new SuccessResponse();
                    successResponse.status = true;
                    successResponse.data = new
                    {
                        message = Constants.SuccessMessages.PRODUCT_CATEGORY_DELETED_MESSAGE
                    };

                    return Ok(successResponse);
                }
                else
                {
                    throw new FailedToDeleteProductCategory();}

            }
            catch(FailedToDeleteProductCategory ex)
            {

                _logger.LogError($" Exception Message: {ex.Message}");
                _logger.LogError($" Exception Stack Trace: {ex.StackTrace}");
                var failureResponse = new FailureResponse();
                failureResponse.status = false;
                errorResponse = new ErrorResponse();
                errorResponse.message = Constants.Errors.Messages.FAILED_TO_DELETE_PRODUCT_CATEGORY_MESSAGE;
                errorResponse.code = Constants.Errors.Codes.PRODUCT_CATEGORY_NOT_DELETED_CODE;
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












