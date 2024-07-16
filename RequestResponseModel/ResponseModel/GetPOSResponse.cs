using System;

namespace HospitalMgmtService.RequestResponseModel.ResponseModel
{
    public class GetPOSResponse
    {
        public long salesId { get; set; }
        public DateTime posDate { get; set; }
        public string customerCategoryName { get; set; }
        public string customerName { get; set; }
        public double paymentDue { get; set; }
        public double totalBill { get; set; }

        public double totalPaid { get; set; }
        public string contactNo { get; set; }

        public DateTime? createdAt { get; set; }
        public int? createdBy { get; set; }

    }
}
