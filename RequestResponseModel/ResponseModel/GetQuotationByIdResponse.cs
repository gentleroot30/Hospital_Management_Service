using Dynamitey.DynamicObjects;
using HospitalMgmtService.Database;
using System;
using System.Collections.Generic;

namespace HospitalMgmtService.RequestResponseModel.ResponseModel
{
    public class GetQuotationByIdResponse
    {

        public long quotationId { get; set; }
        public string quotationNo { get; set; }
        public long customerId { get; set; }
        public string customerName { get; set; }
        public string ethnicity { get; set; }
        public string address { get; set; }
        public string customerContactNo { get; set; }
        public DateTime? quotationDate { get; set; }
        public string newField1 { get; set; }
        public string newField2 { get; set; }
        public string newField3 { get; set; }
        public string newField4 { get; set; }
        public string newField5 { get; set; }
        public string newField6 { get; set; }
        public string newField7 { get; set; }
        public string newField8 { get; set; }

        public string saleNote1 { get; set; }
        public string saleNote2 { get; set; }
        public string saleNote3 { get; set; }
        public string saleNote4 { get; set; }
        public DateTime? createdAt { get; set; }
        public int? createdBy { get; set; }
        public List<DocumentsDTO> Documents { get; set; }
        public List<ProductDto> Products { get; set; }
    }
    public class DocumentsDTO
    {
        public DocumentType documentType { get; set; }

        public long documentId { get; set; }

        public string documentName { get; set; }


    }
    public class ProductDto
    {
        public long productId { get; set; }

        public string productName { get; set; }

        public string customField1 { get; set; }

        public string customField2 { get; set; }

        public string customField3 { get; set;}

    }
}
