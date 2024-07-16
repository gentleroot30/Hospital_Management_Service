namespace HospitalMgmtService.RequestResponseModel.RequestModel
{
    public class GetExpenseCategoriesRequest
    {

        public long searchByType { get; set; }
        public string searchByValue { get; set; }
    }
}
