using System;

namespace HospitalMgmtService.RequestResponseModel.RequestModel
{
    public class GetProductsRequest
    {
        public long searchByType { get; set; }
        public string searchByValue { get; set; }
        public DateTime? fromtDate { get; set; }
        public DateTime? totDate { get; set; }
    }
}
