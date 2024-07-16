using System;

namespace HospitalMgmtService.RequestResponseModel.RequestModel
{
    public class UpdateExpenseRequest
    {
        public long expenseId { get; set; }
        public long expenseCategoryId { get; set; }

        public double amount { get; set; }
        public  DateTime expenseDate { get; set; }
        public string expenseNote { get; set; }


    }
}
