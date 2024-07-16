namespace HospitalMgmtService.RequestResponseModel.RequestModel
{
    public class UpdateSupplierRequest
    {
        public long SupplierId { get; set; }
        public string SupplierName { get; set; }
        public string Address { get; set; }
        public string ContactNo1 { get; set; }
        public string ContactNo2 { get; set; }
        public string ContactNo3 { get; set; }
    }
}
