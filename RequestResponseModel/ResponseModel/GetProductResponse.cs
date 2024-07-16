using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HospitalMgmtService.RequestResponseModel.ResponseModel
{
    public class GetProductResponse
    {
        public long productId { get; set; }
        public string productName { get; set; }
        public long brandId { get; set; }
        public string brandName { get; set; }
        public long productCategoryId { get; set; }
        public string productCategoryName { get; set; }
        public long alertQuantity { get; set; }
        public string sequenceStoring { get; set; }
        public double discountPercent { get; set; }
        public DateTime? createdAt { get; set; }
        public int? createdBy { get; set; }
        public string customField1 { get; set; }
        public string customField2 { get; set; }
        public string CustomField3 { get; set; }

        public int quantity { get; set;}


    }
}
