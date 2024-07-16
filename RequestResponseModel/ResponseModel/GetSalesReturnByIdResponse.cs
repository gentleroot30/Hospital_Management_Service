using HospitalMgmtService.Database;
using System;
using System.Collections.Generic;

namespace HospitalMgmtService.RequestResponseModel.ResponseModel
{
    public class GetSalesReturnByIdResponse
    {
        public long returnId { get; set; }
        public DateTime returnDate { get; set; }
        public string returnRefNo { get; set; }
        public string customerName { get; set; }

        public long customerId { get; set; }
        public double totalReturnAmount { get;set; }
        public double totalReturnAmountPaid { get;set; }
         public string contactNo { get; set; }
        public List<SalesReturnProductDTO> productDetails { get; set; }
        public List<SalesReturnPaymentDetails> paymentDetails { get; set; }
    }
    public class SalesReturnProductDTO
    {
        public string productName { get; set; }
        public long productId { get; set;}
        public long batchId { get; set;}
        public long mrpPerPack { get; set; }
         public string batchNo { get; set; }
        public int returnQuantity { get; set; }


    }

    public class SalesReturnPaymentDetails
    {
        public long PaymentId { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public double Amount { get; set; }
        public DateTime? PaymentDate { get; set; }
    }


}
