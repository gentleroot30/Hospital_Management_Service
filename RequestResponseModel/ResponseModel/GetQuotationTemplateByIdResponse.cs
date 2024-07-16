using System;
using System.Collections.Generic;

namespace HospitalMgmtService.RequestResponseModel.ResponseModel
{
    public class GetQuotationTemplateByIdResponse
    {
        public long quotationTemplateId { get; set; }
        public string quotationTemplateName { get; set; }

        public string quotationTemplateDescription { get; set; }
       
        public List<QuotationTempProductDTO> quotationTempProducts { get; set; }
    }
    public class QuotationTempProductDTO
    {
        public long productId { get; set; }
        public string productName { get; set; }
        public string customField1 { get; set; }
        public string customField2 { get; set; }
        public string customField3 { get; set; }

    }

}
