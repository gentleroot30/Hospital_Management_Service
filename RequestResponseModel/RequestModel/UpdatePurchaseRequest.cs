using HospitalMgmtService.Database;
using System.Collections.Generic;
using System;

namespace HospitalMgmtService.RequestResponseModel.RequestModel
{
    public class UpdatePurchaseRequest
    {
        public long purchaseId { get; set; }
        public long supplierId { get; set; }
        public double totalBill { get; set; }

        
        public double totalPaid { get; set; }
        public DateTime purchaseDate { get; set; }

        public List<UpdatePurchaseProductDTO> updatePurchaseProductDetails { get; set; }
        public List<UpdatepurchasePaymentDTO> updatePurchasePaymentDetails { get; set; }
    }
    public class UpdatePurchaseProductDTO
    {
        public long productId { get; set; }
        public int quantity { get; set; }
        public long batchId { get; set; }
        public string batchNo { get; set; }
        public DateTime expiryDate { get; set; }
        public int packOf { get; set; }
        public double mrpPerPack { get; set; }

        public int opType { get; set; }

    }
    public class UpdatepurchasePaymentDTO
    {
        public long paymentId { get; set; }
        public string recieverName { get; set; }
        public string recieverContact { get; set; }
        public double amount { get; set; }
        public DateTime paymentDate { get; set; }
        public PurchasePaymentMethod paymentMethod { get; set; }

        public int opType { get; set; }

    }

}

