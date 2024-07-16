using System.Collections.Generic;
using System;
using HospitalMgmtService.Database;

namespace HospitalMgmtService.RequestResponseModel.RequestModel
{
    public class UpdatePurchaseReturnRequest
    {
        public long returnId { get; set; }
        public long supplierId { get; set; }
        public DateTime? returnDate { get; set; }
        public double totalReturnBill { get; set; }

        public double totalReturnPaid { get; set; }
        public List<UpdateTotalReturnProductDataDTO> returnProductData { get; set; }

        public List <UpdateReturnPaymentDetails> paymentDetails { get; set; }
    }
    public class UpdateTotalReturnProductDataDTO
    {
        public long productId { get; set; }
        public long batchId { get; set; }
        public int returnQuantity { get; set; }

        public int amount { get; set; }

        public int opType { get; set; }

    }

    public class UpdateReturnPaymentDetails
    {
        public long paymentId { get; set; }
        public string recieverName { get; set; }
        public string recieverContact { get; set; }
        public double amount { get; set; }
        public DateTime? paymentDate { get; set; }

        public PaymentMethod paymentMethod { get; set; }

        public int opType { get; set; }
    }



}
