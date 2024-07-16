using System;

namespace HospitalMgmtService.RequestResponseModel.ResponseModel
{
    public class GetCustomerResponse
    {
        public long customerId { get; set; }

        public string customerCode { get; set; }
        public long categoryId { get; set; }
        public string categoryName { get; set; }

        public string customerName { get; set; }

        public string ethnicity { get; set; }

        public string address { get; set; }

        public string customField_1 { get; set; }

        public string customField_2 { get; set; }

        public string contactNo_1 { get; set; }

        public string contactNo_2 { get; set; }

        public string contactNo_3 { get; set; }
        public int? createdBy { get; set; }
        public DateTime createdAt{get;set;}
    }
}
