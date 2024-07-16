namespace HospitalMgmtService.RequestResponseModel.RequestModel
{
    public class UpdateProductRequest
    {
        public long productId { get; set; }
        public string productName { get; set; }
        public long brandId { get; set; }
        public long productCategoryId { get; set; }
        public int alertQuantity { get; set; }
        public string sequenceStoring { get; set; }
        public double discountPercent { get; set; }
        public string customField1 { get; set; }
        public string customField2 { get; set; }
        public string customField3 { get; set; }



    }
}
