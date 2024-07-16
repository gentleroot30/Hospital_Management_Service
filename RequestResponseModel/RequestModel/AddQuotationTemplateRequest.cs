using Dynamitey.DynamicObjects;
using System.Collections.Generic;

namespace HospitalMgmtService.RequestResponseModel.RequestModel
{
    public class AddQuotationTemplateRequest
    {
        public string quotationTemplateName { get;set; }
        public string quotationTemplateDescription { get;set; }
        public List<QuotationTemplateDTO> products {  get; set; }
    }
    public class QuotationTemplateDTO
    {
        public long productId { get; set; }
    }
}
