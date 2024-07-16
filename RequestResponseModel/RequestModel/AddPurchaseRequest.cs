using Dynamitey.DynamicObjects;
using HospitalMgmtService.Database;
using System;
using System.Collections.Generic;

namespace HospitalMgmtService.RequestResponseModel.RequestModel
{
    public class AddPurchaseRequest
    {
        internal string invoiceNo;
        public long supplierId { get; set; }
        public DateTime purchaseDate { get; set; }
        public double totalBill { get; set; }

        public double totalPaid { get; set; }


        //public string documentLocation { get; set; }
        public List<AddPurchaseProductDTO> addProductDetails { get; set; }
        public List<purchasePaymentDTO> addPaymentDetails { get; set; }
        //public List<PurchaseDocumentDTO> documents { get; set; }
    }
    public class AddPurchaseProductDTO
    {
        public long productId { get; set; }
        public int quantity { get; set; }
        //public long batchId { get; set; }
        public string batchNo { get; set; }
        public DateTime? expiryDate { get; set; }
        public int packOf { get; set; }
        //public double totalBill { get; set; }
        public double mrpPerPack { get; set; }       

    }
    public class purchasePaymentDTO
    {
        //public long PaymentId { get; set; }
        public string recieverName { get; set; }
        public string recieverContact { get; set; }
        public double amount { get; set; }
        public DateTime? paymentDate { get; set; }
        public PurchasePaymentMethod paymentMethod { get; set; }

    }

}
