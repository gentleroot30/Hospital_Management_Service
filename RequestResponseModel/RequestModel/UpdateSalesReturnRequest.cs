using System.Collections.Generic;
using System;
using HospitalMgmtService.Database;

namespace HospitalMgmtService.RequestResponseModel.RequestModel
{
    public class UpdateSalesReturnRequest
    {
        public long returnId { get; set; }
        public long customerId { get; set; }
        public DateTime returnDate { get; set; }
        public double totalReturnAmount { get; set; }
        public double totalReturnAmountPaid { get; set; }
        public List<SalesReturnUpdateProduct> productDetails { get; set; }
        public List <UpdateSalesReturnPaymentRequestDTO> paymentDetails { get; set; }
    }
    public class SalesReturnUpdateProduct
    {
        public long productId { get; set; }
        public long batchId { get; set; }
        public int returnQuantity { get; set; }

        public int amount { get; set; }
        public int opType { get; set; }
    }

    public class UpdateSalesReturnPaymentRequestDTO
    {
        public long paymentId { get; set; }
        public PaymentMethod paymentMethod { get; set; }
        public double amount { get; set; }

        public int opType { get;set ; }
        public DateTime? paymentDate { get; set; }

    }
}
