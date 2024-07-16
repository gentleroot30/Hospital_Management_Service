using HospitalMgmtService.Database;
using Microsoft.AspNetCore.Http;

namespace HospitalMgmtService.RequestResponseModel.RequestModel
{
    public class PurchaseDocumentRequest
    {
        public IFormFile file { get; set; }
        public PurchaseDocumentType DocumentTypes { get; set; }
        public long PurchaseId { get; set; }


    }
}
