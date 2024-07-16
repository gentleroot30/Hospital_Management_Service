using System;

namespace HospitalMgmtService.RequestResponseModel.ResponseModel
{
    public class GetAllPurchaseReturnResponse
    {
        public long ReturnId { get; set; }
        public string SupplierName { get; set; }
        public DateTime? ReturnDate { get; set; }
        public double TotalBill { get; set; }
        public double tolatPendingBill { get; set; }
    }
}
