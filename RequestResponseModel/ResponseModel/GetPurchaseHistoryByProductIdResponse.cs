using System;
using System.Collections.Generic;

namespace HospitalMgmtService.RequestResponseModel.ResponseModel
{
    public class GetPurchaseHistoryByProductIdResponse
    {
        public string productName { get; set; }
        public int currentStock { get; set; }
        public List<PurchaseHistory> purchaseHistory { get; set; }

    }
    public class PurchaseHistory
    {
        public DateTime purchaseDate { get; set; }
        public string batchNo { get; set; }
        public DateTime expiryDate { get; set; }
        public int packOf { get; set; }
        public double mrpPerPack { get; set; }
        public int orderQuantity { get; set; }
        public double unitPrice { get; set; }
        public double totalMRP { get; set; }
    }

}
  

