using Dynamitey.DynamicObjects;
using System;
using System.Collections.Generic;

namespace HospitalMgmtService.RequestResponseModel.RequestModel
{
    public class AddPurchaseOrderRequest
    {
        internal string PoNumber;
        public DateTime PoDate { get; set; }
        public long SupplierIdFk { get; set; }
        public string PoStatus { get; set; }

        public string PurchaseNote { get; set; }
        public List<PurchaseOrderProductDataDTO> purchaseOrderProductDataDTO { get; set; }
    }

    public class PurchaseOrderProductDataDTO
    {
        public long ProductIdFk { get; set; }
        public string OrderQuantity { get; set; }
        

    }
}

