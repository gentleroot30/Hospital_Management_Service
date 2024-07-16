using System.Collections.Generic;

namespace HospitalMgmtService.RequestResponseModel.ResponseModel
{

    

    public class GetProductBatchesResponse
    {
        public long BatchId { get; set; }
        public string BatchNo { get; set; }

        public long productId { get; set; }
        public double mrpPerPack { get; set;}
        //array
    }
}
