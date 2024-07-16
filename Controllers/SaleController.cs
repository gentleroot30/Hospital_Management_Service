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
using System.Threading.Tasks;
using static HospitalMgmtService.Controllers.CustomExceptions;
using static HospitalMgmtService.RequestResponseModel.Constants.Errors;

namespace HospitalMgmtService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SaleController : ControllerBase
    {
        private readonly DBContext _context;
        private SuccessResponse successResponse = new SuccessResponse();
        private FailureResponse failureResponse = new FailureResponse();
        private ErrorResponse errorResponse = new ErrorResponse();

        private readonly ILogger<DashBoardController> _logger;

        public SaleController(DBContext context, ILogger<DashBoardController> logger)
        {
            _context = context;
            logger = _logger;

        }

        [HttpPost("GetSalesByProductIdAndCustomerId")]
        public async Task<IActionResult> GetSalesByProductIdAndCustomerId(GetSalesByProductIdAndCustomerIdRequest getSalesByProductIdAndCustomerId, [FromHeader(Name = "userId")] int userId)
        {

            long? productId = getSalesByProductIdAndCustomerId.ProductId;
            long? customerId = getSalesByProductIdAndCustomerId.CustomerId;

            try
            {
                var sales = await (from s in _context.Sales
                                   join sr in _context.SalesRecord on s.SalesId equals sr.SaleIdFk
                                   join p in _context.Product on sr.ProductIdFk equals p.ProductId
                                   join c in _context.Customers on s.CustomerIdFk equals c.CustomerId
                                   join b in _context.Batches on sr.ProductIdFk equals b.ProductIdFk into batchGroup
                                   from batch in batchGroup.DefaultIfEmpty()
                                   where s.CustomerIdFk == customerId && sr.ProductIdFk == productId
                                   group new { s, sr, p, c, batch } by new { s.SalesId, sr.ProductIdFk } into grouped
                                   select new
                                   {
                                       salesId = grouped.Key.SalesId,
                                       productId = grouped.Key.ProductIdFk,
                                       productName = grouped.FirstOrDefault().p.ProductName,
                                       customerId = grouped.FirstOrDefault().s.CustomerIdFk,
                                       customerName = grouped.FirstOrDefault().c.CustomerName,
                                       totalQuantity = grouped.Sum(x => x.sr.Quantity),
                                       salesDate = grouped.FirstOrDefault().s.PosDate,
                                       totalBill = grouped.FirstOrDefault().s.TotalBill,
                                       totalPaid = grouped.FirstOrDefault().s.TotalPaid,
                                       batchData = grouped.Select(x => new
                                       {
                                           batchId = x.batch.BatchId,
                                           batchNo = x.batch.BatchNo,
                                           expiryDate = x.batch.ExpiryDate,
                                           packOf = x.batch.PackOf,
                                           mrpPerPack = x.batch.MrpPerPack
                                       }).ToList()
                                   }).ToListAsync();

                var result = sales.FirstOrDefault(); // Assuming you only want the first sale for the response

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
