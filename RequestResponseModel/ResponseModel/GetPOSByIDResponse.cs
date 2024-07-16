using Dynamitey.DynamicObjects;
using HospitalMgmtService.Database;
using System;
using System.Collections.Generic;

namespace HospitalMgmtService.RequestResponseModel.ResponseModel
{
    public class GetPOSByIDResponse
    {
       

        public long SalesId { get; set; }
        public string CategoryName { get; set; }
        public string CustomerName { get; set; }
        public long CustomerId { get; set; }
        public DateTime? PosDate { get; set; }
        public double TotalBill { get; set; }
        public double TotalPaid { get; set; }
        public string ContactNo_1 { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }
        public List<POSProductDataDTO> productDetails { get; set; }
        public List<POSPaymentDetailsDTO> payementDetails { get; set; }

    }

    public class POSProductDataDTO
    {

        public long ProductId { get; set; }

        public long BatchId { get; set; }

        public string ProductName { get; set; }
        public int Quantity { get; set; }

        public double DiscountPercent { get; set; }
        public float Amount { get; set; }

    }

    public class POSPaymentDetailsDTO
    {
        public long PaymentId { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public double Amount { get; set; }

        public DateTime? PaymentDate { get; set; }

    }
}
