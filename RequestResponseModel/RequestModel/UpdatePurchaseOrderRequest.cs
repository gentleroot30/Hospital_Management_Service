using Dynamitey.DynamicObjects;
using System;
using System.Collections.Generic;

namespace HospitalMgmtService.RequestResponseModel.RequestModel
{
    public class UpdatePurchaseOrderRequest
    {
        public long PoId { get; set; }
        public DateTime PoDate { get; set; }
        public long SupplierIdFk { get; set; }
        public string PoStatus { get; set; }

        public string PurchaseNote { get; set; }
        public List<UpdatePurchaseOrderProductDataDTO> updatePurchaseOrderProductDataDTO { get; set; }


    }
    public class UpdatePurchaseOrderProductDataDTO
    {
        public long ProductIdFk { get; set; }
        public string OrderQuantity { get; set; }
        


    }
}
