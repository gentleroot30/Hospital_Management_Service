using System;

namespace HospitalMgmtService.RequestResponseModel.ResponseModel
{
    public class GetAllQuotationsResponse
    {
        public long quotationId { get; set; }
        public string customerCategoryName { get; set; }
        public string customerName { get; set; }
        public string ethnicity { get; set; }
        public string address { get; set; }
        public string customerContactNo { get; set; }
        public DateTime? quotationDate { get; set; }
        public string quotationNo  { get; set; }
        public DateTime? createdAt { get; set; }
        public int? createdBy { get; set; }






    }
}
