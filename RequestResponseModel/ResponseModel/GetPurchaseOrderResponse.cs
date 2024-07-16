using Dynamitey.DynamicObjects;
using System;
using System.Collections.Generic;

namespace HospitalMgmtService.RequestResponseModel.ResponseModel
{
    public class GetPurchaseOrdersResponse
    {
        public long poId { get; set; }
        public string poNumber { get; set; }
        public DateTime poDate { get; set; }
        public string supplierName { get; set; }
        public long supplierId { get; set; }
        public string poStatus { get; set; }
        public string purchaseNote { get; set; }

        public List<PurchaseOrdersProductDataResponse> productData { get; set; }

    }

    public class PurchaseOrdersProductDataResponse
    {
        public string productName { get; set; }
        public long productId { get; set; }
        public string orderQuantity { get; set; }
        public int quantity { get; set; }


    }
}
