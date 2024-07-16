using System;

namespace HospitalMgmtService.RequestResponseModel.ResponseModel
{
    public class GetAllSalesReturn
    {
        public long returnId { get; set; }
        public DateTime returnDate { get; set; }
        public string returnRefNo { get; set; }
        public string customerName { get; set; }
        public double totalReturnAmount { get; set; }
        public double totalReturnAmountPaid { get; set; }
        public string contactNo { get; set; }
        public long createdBy { get; set; }
        public DateTime createdAt{ get; set; }
    }
}
