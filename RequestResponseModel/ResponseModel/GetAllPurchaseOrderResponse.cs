using Dynamitey.DynamicObjects;
using System;
using System.Collections.Generic;

namespace HospitalMgmtService.RequestResponseModel.ResponseModel
{
    public class GetAllPurchaseOrderResponse
    {
        public long PoId { get; set; }
        public long PoNumber { get; set; }
        public DateTime PoDate { get; set; }
        public long SupplierIdFk { get; set; }
        public string PoStatus { get; set; }
        public List<PurchaseOrderProductData> purchaseOrderProductData { get; set; }

    }

    public class PurchaseOrderProductData
    {
        public long ProductIdFk { get; set; }
        public string OrderQuantity { get; set; }
        public string PurchaseNote { get; set; }

    }
}
