namespace HospitalMgmtService.RequestResponseModel.RequestModel
{
    public class AddCustomerRequest
    {

        public long categoryId { get; set; }

        public string customerName { get; set; }

        public string ethnicity { get; set; }

        public string address { get; set; }

        public string customField_1 { get; set; }

        public string customField_2 { get; set; }

        public string contactNo_1 { get; set; }

        public string contactNo_2 { get; set; }

        public string contactNo_3 { get; set; }

    }
}
