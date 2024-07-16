using System.Collections.Generic;
using System;

namespace HospitalMgmtService.RequestResponseModel.ResponseModel
{
    public class GetPurchaseOrderByIdResponse
    {
        public long poId { get; set; }
        public string poNumber { get; set; }
        public DateTime poDate { get; set; }
        public long supplierId { get; set; }
        public string supplierName { get; set; }
        public string poStatus { get; set; }
        public string purchaseNote { get; set; }

        public List<PurchaseOrderByIdProductData> productData { get; set; }
    }

    public class PurchaseOrderByIdProductData
    {
        public long productId { get; set; }
        public string productName { get; set; }
        public string orderQuantity { get; set; }
        public int quantity { get; set; }


    }
}

