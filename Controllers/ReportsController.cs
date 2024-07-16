using HospitalMgmtService.Database;
using HospitalMgmtService.Model;
using HospitalMgmtService.RequestResponseModel;
using HospitalMgmtService.RequestResponseModel.RequestModel;
using HospitalMgmtService.RequestResponseModel.ResponseModel;
using Humanizer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using ServiceStack.Web;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using static HospitalMgmtService.Controllers.CustomExceptions;
using static HospitalMgmtService.RequestResponseModel.Constants.Errors;

namespace HospitalMgmtService.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class ReportsController : ControllerBase
    {

        private readonly DBContext _context;
        private SuccessResponse successResponse = new SuccessResponse();
        private FailureResponse failureResponse = new FailureResponse();
        private ErrorResponse errorResponse = new ErrorResponse();

        public ReportsController(DBContext context)
        {
            _context = context;
        }

        [HttpPost("GetStockReport")]
        public async Task<IActionResult> GetReports([FromBody] GetStockReportRequest getStockReportResponse, [FromHeader(Name = "userId")] int userId)
        {
            try
            {
                
                var products = await _context.Product.ToListAsync();
                var batches = await _context.Batches.ToListAsync();
                var salesRecords = await _context.SalesRecord.ToListAsync();


                var reportsQuery = products
                    .GroupJoin(
                        batches,
                        product => product.ProductId,
                        batch => batch.ProductIdFk,
                        (product, batches) => new { product, batches }
                    )
                    .GroupJoin(
                        salesRecords,
                        p => p.product.ProductId,
                        salesRecord => salesRecord.ProductIdFk,
                        (p, salesRecords) => new { p.product, p.batches, salesRecords }
                    )
                    .Select(g => new GetStockReportResponse
                    {
                        productName = g.product.ProductName,
                        currentStock = g.product.Quantity,
                        currentStockValue = g.batches.Sum(b => b.MrpPerPack * g.product.Quantity),
                        unitSold = g.salesRecords.Sum(sr => sr.Quantity),
                       

                    }).AsQueryable();

               
                if (getStockReportResponse.searchByType == 1 && !string.IsNullOrEmpty(getStockReportResponse.searchByValue))
                {
                    reportsQuery = reportsQuery.Where(q => q.productName.ToUpper().Contains(getStockReportResponse.searchByValue.ToUpper()));
                }
                var reports = reportsQuery.ToList();
              
                var successResponse = new SuccessResponse();
                successResponse.status = true;
                successResponse.data = reports;
                return Ok(successResponse);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}
