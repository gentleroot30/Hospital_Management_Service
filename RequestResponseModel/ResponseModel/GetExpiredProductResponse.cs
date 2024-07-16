using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;

namespace HospitalMgmtService.RequestResponseModel.ResponseModel
{
    public class GetExpiredProductResponse
    {
        public string productName { get; set; }
        public DateTime expiredDate { get; set; }

        public DateTime? createdAt { get; set; }
        public int? createdBy { get; set; }


    }
}
