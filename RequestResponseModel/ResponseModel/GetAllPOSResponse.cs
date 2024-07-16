using System;

namespace HospitalMgmtService.RequestResponseModel.ResponseModel
{
    public class GetAllPOSResponse
    {
        public long salesId { get; set; }
        public DateTime posDate { get; set; }
        public string customerCategoryname { get; set; }
        public string customername { get; set; }
        public double paymentDue { get; set; }
        public double totalBill { get; set; }
        public double totalPaid { get; set; }
        public string contactNo { get; set; }



    }
}
