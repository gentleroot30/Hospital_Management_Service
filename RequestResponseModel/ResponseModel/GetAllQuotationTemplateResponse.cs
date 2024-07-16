using System;

namespace HospitalMgmtService.RequestResponseModel.ResponseModel
{
    public class GetAllQuotationTemplateResponse
    {
        public long quotationTemplateId { get; set; }
        public string quotationTemplateName { get; set; }
        public string quotationTemplateDescription { get; set; }

        public DateTime? createdAt { get; set; }
        public int? createdBy { get; set; }
    }
}
