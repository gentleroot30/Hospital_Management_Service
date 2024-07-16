using System.Collections.Generic;

namespace HospitalMgmtService.RequestResponseModel.RequestModel
{
    public class UpdateQuotationTemplateRequest
    {
        public long quotationTemplateId { get; set; }
        public string quotationTemplateName { get; set; }
        public string quotationTemplateDescription { get; set; }
        public List<UpdateQuotationTempDTO> products { get; set; }
    }
    public class UpdateQuotationTempDTO
    {
        public long productId { get; set; }
    }
}

