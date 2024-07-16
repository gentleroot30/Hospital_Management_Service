using HospitalMgmtService.Database;
using System.Collections.Generic;
using System;

namespace HospitalMgmtService.RequestResponseModel.RequestModel
{
    public class UpdatePOSRequest
    {
        public long salesId { get; set; }
        public long customerId { get; set; }
        public double totalBill { get; set; }
        public double totalPaid { get; set; }
        public DateTime posDate { get; set; }
        public List<UpdatePOSProduct> productDetails { get; set; }
        public List<UpdatePOSPaymentDetailDTO> paymentDetails { get; set; }
    } 
    public class UpdatePOSProduct
    {
        public long productId { get; set; }
        public int quantity { get; set; }
        public float amount { get; set; }

        public long batchId { get; set; }

        public int opType { get; set; }
    }
    public class UpdatePOSPaymentDetailDTO
    {
        public long paymentId { get; set; }
        public PaymentMethod paymentMethod { get; set; }
        public double amount { get; set; }
        public int opType { get; set; }
        public DateTime? paymentDate { get; set; }

    }

}
