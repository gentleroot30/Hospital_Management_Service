namespace HospitalMgmtService.RequestResponseModel.RequestModel
{
    public class GetProductCategoriesRequest
    {
        public long searchByType { get; set; }
        public string searchByValue { get; set; }
    }
}
