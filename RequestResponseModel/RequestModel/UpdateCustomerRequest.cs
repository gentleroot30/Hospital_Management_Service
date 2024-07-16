namespace HospitalMgmtService.RequestResponseModel.RequestModel
{
    public class UpdateCustomerRequest
    {
        public long CustomerId { get; set; }
        public string CustomerName { get; set; }
        public long CategoryId { get; set; }
        public string Ethnicity { get; set; }
        public string Address { get; set; }
        public string CustomField_1 { get; set; }

        public string CustomField_2 { get; set; }

        public string ContactNo_1 { get; set; }

        public string ContactNo_2 { get; set; }

        public string ContactNo_3 { get; set; }

    }
}
