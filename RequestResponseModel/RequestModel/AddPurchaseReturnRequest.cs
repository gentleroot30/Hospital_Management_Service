using System.Collections.Generic;
using System;
using HospitalMgmtService.Database;

namespace HospitalMgmtService.RequestResponseModel.RequestModel
{
    public class AddPurchaseReturnRequest
    {
        public long supplierId { get; set; }
        public DateTime? returnDate { get; set; }
        public double totalReturnBill { get; set; }
        public double totalReturnPaid { get; set; }

        public List<TotalReturnDataDTO> totalReturnDataDTO { get; set; }

        public List<PurchaseReturnPaymentDetails> paymentDetails { get; set; }

    }

    public class TotalReturnDataDTO
    {
        public long productId { get; set; }
        public long batchId { get; set; }
        public int returnQuantity { get; set; }

        public int amount { get; set; }

    }
    public class PurchaseReturnPaymentDetails
    {
        public string recieverName { get; set; }
        public string recieverContact { get; set; }
        public double amount { get; set; }
        public DateTime? paymentDate { get; set; }

        public PaymentMethod paymentMethod { get; set; }
    }
}
