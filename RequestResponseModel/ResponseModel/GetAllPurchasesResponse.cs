using System;

namespace HospitalMgmtService.RequestResponseModel.ResponseModel
{
    public class GetAllPurchasesResponse
    {
        public long purchaseId { get; set; }
        public string suppliername { get; set; }
        public DateTime purchaseDate { get; set; }
        public string invoiceNumber { get; set; }
        public double totalBill { get; set; }
        public double totalPaid { get; set; }

        public DateTime createdAt { get; set; }
        public long createdBy { get; set; }
    }
}
