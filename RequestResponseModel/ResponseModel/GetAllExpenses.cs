using System;

namespace HospitalMgmtService.RequestResponseModel.ResponseModel
{
    public class GetAllExpenses
    {
        public long expenseId { get; set; }
        public long categoryId { get; set; }
        public string categoryName { get; set; }
        public double amount { get; set; }
        public DateTime? expenseDate {  get; set; }  
        public string expenseNote { get; set; }
        public DateTime? createdAt { get; set; }
       public int? createdBy { get; set; }
    }
}
