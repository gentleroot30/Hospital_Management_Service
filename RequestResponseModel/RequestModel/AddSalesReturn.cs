using Dynamitey.DynamicObjects;
using HospitalMgmtService.Database;
using System;
using System.Collections.Generic;

namespace HospitalMgmtService.RequestResponseModel.RequestModel
{
    public class AddSalesReturn
    {
        internal string ReturnRefNo;

        public long customerId { get; set; }
        public DateTime returnDate { get; set; }
        public double totalReturnAmount { get; set; }
        public double totalReturnAmountPaid { get;set; }
        public List<SalesReturnAddProduct> productDetails { get; set; }

        public List<SalesReturnPaymentRecordDTO> paymentDetails { get; set; }
    }
    public class SalesReturnAddProduct
    {
        public long productId { get; set;}
        public long batchId { get; set;}
        public int returnQuantity { get; set; }

        public int amount { get; set; } 
    }

    public class SalesReturnPaymentRecordDTO
    {
        //public long paymentId { get; set; }
        public PaymentMethod paymentMethod { get; set; }
        public double amount { get; set; }
        public DateTime? paymentDate { get; set; }



    }
}
