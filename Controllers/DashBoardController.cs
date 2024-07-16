using System.Linq;
using System.Threading.Tasks;
using System;
using HospitalMgmtService.Model;
using HospitalMgmtService.RequestResponseModel;
using HospitalMgmtService.RequestResponseModel.RequestModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using static HospitalMgmtService.Controllers.CustomExceptions;
using static HospitalMgmtService.RequestResponseModel.Constants.Errors;
using Microsoft.EntityFrameworkCore;
using NLog;
using HospitalMgmtService.RequestResponseModel.ResponseModel;
using HospitalMgmtService.Database;
using System.Globalization;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using Newtonsoft.Json.Linq;

namespace HospitalMgmtService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashBoardController : ControllerBase
    {
        private readonly DBContext _context;
        private SuccessResponse successResponse = new SuccessResponse();
        private FailureResponse failureResponse = new FailureResponse();
        private ErrorResponse errorResponse = new ErrorResponse();
        
        private readonly ILogger<DashBoardController> _logger;

        public DashBoardController(DBContext context , ILogger<DashBoardController> logger)
        {
            _context = context;
            logger = _logger;

        }



        [HttpPost("GetDashboardData")]
        public async Task<IActionResult> GetDashboardData([FromBody] GetDashBoardDataRequest getDashBoardDataRequest, [FromHeader(Name = "userId")] int userId )
        {
            try
            {
                var combinedDataQuery = await Task.Run(() => _context.Sales

            .Select(sale => new { DateAndTime = sale.PosDate, TotalSalesPaid = sale.TotalPaid, TotalSales = sale.TotalBill, TotalPurchasePaid = 0.0, TotalPurchase = 0.0, TotalExpense = 0.0 })

            .Union(_context.Purchases
                .Select(purchase => new { DateAndTime = purchase.PurchaseDate, TotalSalesPaid = 0.0, TotalSales = 0.0, TotalPurchasePaid = purchase.TotalPaid, TotalPurchase = purchase.TotalBill, TotalExpense = 0.0 }))

            .Union(_context.Expenses
                .Select(expense => new { DateAndTime = DateTime.Now, TotalSalesPaid = 0.0, TotalSales = 0.0, TotalPurchasePaid = 0.0, TotalPurchase = 0.0, TotalExpense = expense.Amount })));




                var overallSum = new GetDashBoardDataResponse
                {
                    
                    TotalSales = combinedDataQuery.Sum(x => x.TotalSales),
                    TotalPurchase = combinedDataQuery.Sum(x => x.TotalPurchase),
                    TotalExpense = combinedDataQuery.Sum(x => x.TotalExpense),
                    TotalSalesDue = combinedDataQuery.Sum(x => x.TotalSales) - combinedDataQuery.Sum(x => x.TotalSalesPaid),
                    TotalPurchaseDue = combinedDataQuery.Sum(x => x.TotalPurchase) - combinedDataQuery.Sum(x => x.TotalPurchasePaid),
                    

                };



                var successResponse = new SuccessResponse
                {
                    status = true,
                    data = overallSum
                };

               // Console.WriteLine(summaryData);

                return Ok(successResponse);
            }
            catch (Exception ex)
            {
                // Log the exception details
                _logger.LogError($"Exception Message: {ex.Message}");
                _logger.LogError($"Exception Stack Trace: {ex.StackTrace}");

                return BadRequest(ex.Message);
            }
        }





        [HttpPost("GetSalesAndPurchases")]
        public async Task<IActionResult> GetSalesAndPurchases([FromBody] GetSalesAndPurchases getSalesAndPurchases, [FromHeader(Name = "userId")] int userId)
        {



            try
            {
                var combinedDataQuery = _context.Sales

                    .Select(sale => new { DateAndTime = sale.PosDate, TotalSalesPaid = sale.TotalPaid, TotalSales = sale.TotalBill, TotalPurchasePaid = 0.0, TotalPurchase = 0.0 })
                    .Union(_context.Purchases

                        .Select(purchase => new { DateAndTime = purchase.PurchaseDate, TotalSalesPaid = 0.0, TotalSales = 0.0, TotalPurchasePaid = purchase.TotalPaid, TotalPurchase = purchase.TotalBill })).AsQueryable();

                var allSalesAndPurchase = Enumerable.Empty<object>().AsQueryable();





                if (getSalesAndPurchases.ChartDataType == 1)
                {
                    

                    allSalesAndPurchase = combinedDataQuery
                        
                        .GroupBy(data => data.DateAndTime.Year)
                        .Select(groupedData => new
                        {
                            Year = groupedData.Key.ToString(),
                            TotalSales = groupedData.Sum(x => x.TotalSales),
                            TotalPurchase = groupedData.Sum(x => x.TotalPurchase)
                        });
                }
                else if (getSalesAndPurchases.ChartDataType == 2) // Assuming ChartDataType 2 represents grouping by month
                {
                    DateTime oneYearAgo = DateTime.Now.AddYears(-1);

                    allSalesAndPurchase = combinedDataQuery
                        .Where(data => data.DateAndTime.Date >= oneYearAgo)
                        .GroupBy(data => new { data.DateAndTime.Year, data.DateAndTime.Month })
                        .OrderBy(groupedData => groupedData.Key.Year).ThenBy(groupedData => groupedData.Key.Month)
                        .Select(groupedData => new
                        {
                            Year = groupedData.Key.Year.ToString() + " " + CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(groupedData.Key.Month).ToString(),
                            TotalSales = groupedData.Sum(x => x.TotalSales),
                            TotalPurchase = groupedData.Sum(x => x.TotalPurchase)
                        });

                }
                else if (getSalesAndPurchases.ChartDataType == 3) // Assuming ChartDataType 3 represents grouping by month
                {
                    DateTime sixMonthsAgo = DateTime.Now.AddMonths(-6);

                    allSalesAndPurchase = combinedDataQuery
                        .Where(data => data.DateAndTime.Date >= sixMonthsAgo)
                        .GroupBy(data => new { data.DateAndTime.Year, data.DateAndTime.Month })
                        .OrderBy(groupedData => groupedData.Key.Year).ThenBy(groupedData => groupedData.Key.Month)
                        .Select(groupedData => new
                        {
                            Year = groupedData.Key.Year.ToString() + " " + CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(groupedData.Key.Month).ToString(),
                            TotalSales = groupedData.Sum(x => x.TotalSales),
                            TotalPurchase = groupedData.Sum(x => x.TotalPurchase)
                        });

                }
                else if (getSalesAndPurchases.ChartDataType == 4) // Assuming ChartDataType 4 represents grouping by Date
                {
                    DateTime oneMonthsAgo = DateTime.Now.AddMonths(-1);

                    allSalesAndPurchase = combinedDataQuery
                        .Where(data => data.DateAndTime.Date >= oneMonthsAgo)
                        .GroupBy(data => data.DateAndTime.Date)
                        .OrderBy(groupedData => groupedData.Key.Year).ThenBy(groupedData => groupedData.Key.Month)
                        .Select(groupedData => new
                        {
                           
                            Year = groupedData.Key.ToString(),
                            TotalSales = groupedData.Sum(x => x.TotalSales),
                            TotalPurchase = groupedData.Sum(x => x.TotalPurchase)
                        });

                }
                else if (getSalesAndPurchases.ChartDataType == 5) // Assuming ChartDataType 5 represents grouping by hours
                {
                    DateTime OneDayAgo = DateTime.Now.AddDays(-1);

                    allSalesAndPurchase = combinedDataQuery
                        .Where(data => data.DateAndTime.Date == OneDayAgo.Date)
                        .OrderBy(data => data.DateAndTime.Hour)
                        .Select(data => new
                        {
                            Year = data.DateAndTime.ToString(),
                            TotalSales = data.TotalSales,
                            TotalPurchase = data.TotalPurchase
                        });


                }
                else if (getSalesAndPurchases.ChartDataType == 6) // Assuming ChartDataType 5 represents grouping by hours
                {
                    DateTime today = DateTime.Now;

                    allSalesAndPurchase = combinedDataQuery
                        .Where(data => data.DateAndTime.Date == today.Date)
                        .OrderBy(data => data.DateAndTime.Hour)
                        .Select(data => new
                        {
                            Year = data.DateAndTime.ToString(),
                            TotalSales = data.TotalSales,
                            TotalPurchase = data.TotalPurchase
                        });



                }





                var overallSum = new
                {
                    salesAndPurchasesChartData = await allSalesAndPurchase.ToListAsync()

                };




                var successResponse = new SuccessResponse
                {
                    status = true,
                    data = overallSum
                };



                return Ok(successResponse);
            }
            catch (Exception ex)
            {
                
                _logger.LogError($"Exception Message: {ex.Message}");
                _logger.LogError($"Exception Stack Trace: {ex.StackTrace}");

                return BadRequest(ex.Message);
            }
        }



        [HttpPost("GetSalesByProductData")]
        public async Task<IActionResult> GetSalesByProductData([FromBody] GetSalesAndPurchases getSalesAndPurchases, [FromHeader(Name = "userId")] int userId)
        {



            try
            {
                var combinedDataQuery =  (from product in _context.Product
                                                    join saleRecord in _context.SalesRecord on product.ProductId equals saleRecord.ProductIdFk into salesRecords
                                                    from saleRecord in salesRecords.DefaultIfEmpty()
                                                    join sale in _context.Sales on saleRecord.SaleIdFk equals sale.SalesId into sales
                                                    from sale in sales.DefaultIfEmpty()
                                                    select new
                                                    {
                                                        productId=product.ProductId,
                                                        productName = product.ProductName,
                                                        TotalBill = sale != null ? sale.TotalBill : 0,
                                                        Quantity = saleRecord != null ? saleRecord.Quantity : 0,
                                                        DateAndTime = sale != null ? sale.PosDate : (DateTime?)null
                                                    }).AsQueryable();

                


                var allSalesByProductData = Enumerable.Empty<object>().AsQueryable();

                if (getSalesAndPurchases.ChartDataType == 1)
                {

                    allSalesByProductData = combinedDataQuery
                                          .GroupBy(data => data.productName)
                                          .Select(groupedData => new
                                          {
                                              ProductName = groupedData.Key, // Use Key to get the grouped value
                                              TotalBill = groupedData.Sum(item => item.TotalBill),
                                              Quantity = groupedData.Sum(item => item.Quantity)
                                          });

                }
                else if (getSalesAndPurchases.ChartDataType == 2) // Assuming ChartDataType 2 represents grouping by month
                {
                    DateTime oneYearAgo = DateTime.Now.AddYears(-1);

                    allSalesByProductData = combinedDataQuery
                                            .Where(data => data.DateAndTime.Value.Date >= oneYearAgo)
                                          .GroupBy(data => data.productName)
                                          .Select(groupedData => new
                                          {
                                              ProductName = groupedData.Key, // Use Key to get the grouped value
                                              TotalBill = groupedData.Sum(item => item.TotalBill),
                                              Quantity = groupedData.Sum(item => item.Quantity)
                                          });



                }
                else if (getSalesAndPurchases.ChartDataType == 3) // Assuming ChartDataType 3 represents grouping by month
                {
                    DateTime sixMonthsAgo = DateTime.Now.AddMonths(-6);

                    allSalesByProductData = combinedDataQuery
                                            .Where(data => data.DateAndTime.Value.Date >= sixMonthsAgo)
                                          .GroupBy(data => data.productName)
                                          .Select(groupedData => new
                                          {
                                              ProductName = groupedData.Key, // Use Key to get the grouped value
                                              TotalBill = groupedData.Sum(item => item.TotalBill),
                                              Quantity = groupedData.Sum(item => item.Quantity)
                                          });


                }
                else if (getSalesAndPurchases.ChartDataType == 4) // Assuming ChartDataType 4 represents grouping by Date
                {
                    DateTime oneMonthsAgo = DateTime.Now.AddMonths(-1);

                    allSalesByProductData = combinedDataQuery
                        .Where(data => data.DateAndTime.HasValue && data.DateAndTime.Value.Date >= oneMonthsAgo)
                        .GroupBy(data => data.productName)
                        .Select(groupedData => new
                        {
                            ProductName = groupedData.Key,
                            TotalBill = groupedData.Sum(item => (item.TotalBill )),  // Replace null with 0 for TotalBill
                            Quantity = groupedData.Sum(item => (item.Quantity ))     // Replace null with 0 for Quantity
                        });





                }
                else if (getSalesAndPurchases.ChartDataType == 5) // Assuming ChartDataType 5 represents grouping by hours
                {
                    DateTime OneDayAgo = DateTime.Now.AddDays(-1);

                    allSalesByProductData = combinedDataQuery
                                            .Where(data => data.DateAndTime.Value.Date >= OneDayAgo)
                                          .GroupBy(data => data.productName)
                                          .Select(groupedData => new
                                          {
                                              ProductName = groupedData.Key, // Use Key to get the grouped value
                                              TotalBill = groupedData.Sum(item => item.TotalBill),
                                              Quantity = groupedData.Sum(item => item.Quantity)
                                          });


                }
                else if (getSalesAndPurchases.ChartDataType == 6) // Assuming ChartDataType 5 represents grouping by hours
                {
                    DateTime today = DateTime.Now;

                    allSalesByProductData = combinedDataQuery
                                            .Where(data => data.DateAndTime.Value.Date == today)
                                          .GroupBy(data => data.productName)
                                          .Select(groupedData => new
                                          {
                                              ProductName = groupedData.Key, // Use Key to get the grouped value
                                              TotalBill = groupedData.Sum(item => item.TotalBill),
                                              Quantity = groupedData.Sum(item => item.Quantity)
                                          });


                }



                var overallSum = new
                {
                    salesAndPurchasesChartData = await allSalesByProductData.ToListAsync()

                };




                var successResponse = new SuccessResponse
                {
                    status = true,
                    data = overallSum
                };



                return Ok(successResponse);
            }
            catch (Exception ex)
            {

                _logger.LogError($"Exception Message: {ex.Message}");
                _logger.LogError($"Exception Stack Trace: {ex.StackTrace}");

                return BadRequest(ex.Message);
            }
        }


    }






}
