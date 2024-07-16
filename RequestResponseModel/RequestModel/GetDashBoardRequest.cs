using System;

namespace HospitalMgmtService.RequestResponseModel.RequestModel
{
    public class GetDashBoardDataRequest
    {
        public int UserId { get; set; }
    }


    public class GetSalesAndPurchases
    {
        public int ChartDataType { get; set; }
        public int UserId { get; set; }

    }

    public class GetSalesByProductData
    {
        public int ChartDataType { get; set; }
        public int UserId { get; set; }

    }
}
