using System.Collections.Generic;

namespace HospitalMgmtService.RequestResponseModel.ResponseModel
{
    public class GetPurchaseDocumentResponse
    {
        public List<GetPurchaseDocumentDTO> documents { get; set; }
    }
}
