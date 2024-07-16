using System.Collections.Generic;
using System;
using HospitalMgmtService.Database;

namespace HospitalMgmtService.RequestResponseModel.ResponseModel
{
    public class GetPurchaseReturnByIdResponse
    {
        public long returnId { get; set; }

        public long supplierId { get; set; }
        public string supplierName { get; set; }
        public DateTime? returnDate { get; set; }
        public string returnRefNo { get; set; }

        public double totalReturnBill { get; set; }

        public double totalReturnPaid { get; set; }
        public double totalSupplierPendingPayement { get; set; }
        public List<ReturnProductDataDTO> returnProductData { get; set; }

        public List<RetrunPaymentDetails> paymentDetails { get; set; }

    }

    public class ReturnProductDataDTO
    {
        public string productName { get; set; }
        public long productId { get; set; }
        public long batchId { get; set; }

        public string batchNo { get; set; }
        public int? returnQuantity { get; set; }

        public long mrpPerPack { get; set; }
    }

    public class RetrunPaymentDetails
    {
        public long paymentId { get; set; }
        public string recieverName { get; set; }
        public string recieverContact { get; set; }
        public double amount { get; set; }
        public DateTime? paymentDate { get; set; }

        public PaymentMethod paymentMethod { get; set; }
    }



}
