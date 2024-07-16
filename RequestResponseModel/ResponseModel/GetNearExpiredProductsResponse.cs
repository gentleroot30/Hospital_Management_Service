using System;

namespace HospitalMgmtService.RequestResponseModel.ResponseModel
{
    public class GetNearExpiredProductsResponse
    {
        public string productName { get; set; }
        public DateTime expiredDate { get; set; }

        public DateTime? createdAt { get; set; }
        public int? createdBy { get; set; }

    }
}
