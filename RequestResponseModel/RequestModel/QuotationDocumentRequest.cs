using HospitalMgmtService.Database;
using Microsoft.AspNetCore.Http;

namespace HospitalMgmtService.RequestResponseModel.RequestModel
{
    public class QuotationDocumentRequest
    {
       
        public IFormFile file { get; set; }
        public DocumentType DocumentTypes { get; set; }
        public long QuotationId { get; set; }

        //public DocumentType DocumentTypes { get; set; }

        //public long QuoatationDocumentId { get; set; }

    }
}
