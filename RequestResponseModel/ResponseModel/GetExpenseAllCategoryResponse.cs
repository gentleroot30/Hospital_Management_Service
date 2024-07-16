using System;

namespace HospitalMgmtService.RequestResponseModel.ResponseModel
{
    public class GetExpenseAllCategoryResponse
    {
        public long categoryId { get; set; }
        public string categoryName { get; set; }
        public string description { get; set; }
        public int? createdBy { get; set; }
        public DateTime? createdAt { get; set; }
    }
}
