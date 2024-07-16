using System;

namespace HospitalMgmtService.RequestResponseModel.ResponseModel
{
    public class GetPurchaseReturnResponse
    {
        public long returnId { get; set; }
        public string supplierName { get; set; }
        public string returnRefNo { get; set; }
        public DateTime? returnDate { get; set; }
        public double totalReturnBill { get; set; }

        public double totalReturnPaid { get; set; }
        public double totalSupplierPendingPayement { get; set; }
        public int? CreatedBy { get; set; }


    }
}
