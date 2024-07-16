using Dynamitey.DynamicObjects;
using HospitalMgmtService.Database;
using HospitalMgmtService.RequestResponseModel.RequestModel;
using System;
using System.Collections.Generic;

namespace HospitalMgmtService.RequestResponseModel.ResponseModel
{
    public class GetPurchaseByIdResponsr
    {
        public long purchaseId { get; set; }

        public long supplierId { get; set; }
        public string suppliername { get; set; }
        public long createdBy { get; set; }
        public DateTime createdAt { get; set; }
        public DateTime purchaseDate { get; set; }
        public string invoiceNumber { get; set; }
        public double totalBill { get; set; }
        public double totalPaid { get; set; }
        public List<PurchaseProductDTO> productDetails { get; set; }

        public List<GetPurchaseDocumentDTO> documents { get; set; }

        public List<PurchasePaymentDTO> paymentDetails { get; set; }
    }

    public class PurchaseProductDTO
    {
        public long productId { get; set; }
        public string productName { get; set; }
        public long batchId { get; set; }
        public string batchNo { get; set; }
        public DateTime expiryDate { get; set; }
        public int packOf { get; set; }
        public double mrpPerPack { get; set; }
        public int quantity { get; set; }

        public double discountPercent {  get; set; }
       

    }
    public class GetPurchaseDocumentDTO
    {
        public PurchaseDocumentType documentType { get; set; }

        public long documentId { get; set; }

        public string documentName { get; set; }
    }

    public class PurchasePaymentDTO
    { 
        public long PaymentId { get; set; }
        public string recieverName { get; set; }
        public string recieverContact { get; set; }
        public double amount { get; set; }
        public DateTime? paymentDate { get; set; }
        public PurchasePaymentMethod paymentMethod { get; set; }

    }
}
