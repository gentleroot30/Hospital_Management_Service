namespace HospitalMgmtService.RequestResponseModel.RequestModel
{
    public class UpdateExpenseCategoryRequest
    {
        public long categoryId { get; set; }
        public string categoryName { get; set; }
        public string description { get; set; }
    }
}
