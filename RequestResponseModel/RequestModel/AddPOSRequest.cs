using Dynamitey.DynamicObjects;
using HospitalMgmtService.Database;
using System;
using System.Collections.Generic;

namespace HospitalMgmtService.RequestResponseModel.RequestModel
{
    public class AddPOSRequest
    {
        public long customerId { get; set; }
        public double totalBill { get; set; }
        public double totalPaid { get; set; }
        public DateTime posDate { get; set; }
        public List<AddPOSProductDTO> productDetails { get; set; }
        public List<AddPOSPaymentDetailDTO> paymentDetails { get; set; }
    }
    public class AddPOSProductDTO
    {
        public long productId { get; set; }
        public int quantity { get;set; }

        public long batchId { get; set; }
        public float amount { get; set; }

        

    }
    public class AddPOSPaymentDetailDTO
    {
        //public long paymentId { get; set; }
        public PaymentMethod paymentMethod { get; set; }
        public double amount { get; set; }
        public DateTime? paymentDate { get; set; }

    }
}
