﻿namespace HospitalMgmtService.RequestResponseModel.RequestModel
{
    public class UpdateCustomerCategory
    {
        public long categoryId { get; set; }
        public string categoryName { get; set; }
        public string description { get; set; }
    }
}
