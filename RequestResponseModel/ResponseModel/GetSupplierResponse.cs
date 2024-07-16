using System;

namespace HospitalMgmtService.RequestResponseModel.ResponseModel
{
    public class GetSupplierResponse
    {
        public long SupplierId { get; set; }
        public string SupplierName { get; set; }
        public string SupplierCode { get; set; }
        public string Address { get; set; }
        public string ContactNo1 { get; set; }
        public string ContactNo2 { get; set; }
        public string ContactNo3 { get; set; }
        public double PurchaseDue { get; set; }
        public double PurchaseReturns { get; set; }
        public DateTime? createdAt { get; set; }
        public int? createdBy { get; set; }


    }
}
