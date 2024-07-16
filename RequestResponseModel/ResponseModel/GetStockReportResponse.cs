using Microsoft.VisualBasic;
using System;

namespace HospitalMgmtService.RequestResponseModel.ResponseModel
{
    public class GetStockReportResponse
    {

        public string productName { get; set; }

        public int currentStock { get; set; }
        public double currentStockValue { get; set; }
        public int unitSold  { get; set; }

        public DateTime posDate { get; set; }
    
    }
}
