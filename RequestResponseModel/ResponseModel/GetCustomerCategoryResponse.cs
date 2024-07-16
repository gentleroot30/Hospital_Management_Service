using System;
using System.ComponentModel.DataAnnotations;

namespace HospitalMgmtService.RequestResponseModel.ResponseModel
{
    public class GetCustomerCategoryResponse
    {
        public long categoryId { get; set; }
      
        public string categoryName { get; set; }

        public string description { get; set; }
        public DateTime createdAt {get; set; }
        public int createdBy { get; set; }

    }
}
