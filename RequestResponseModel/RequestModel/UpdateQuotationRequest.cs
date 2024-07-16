using System.Collections.Generic;
using System;
using HospitalMgmtService.Database;

namespace HospitalMgmtService.RequestResponseModel.RequestModel
{
    public class UpdateQuotationRequest
    {
        public long quotationId { get; set; }

        public long customerId { get; set; }
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
        //public List<UpdateDocumentsDTO> Documents { get; set; }
        public List<UpdateProductsDTO> Products { get; set; }
    }
    //public class UpdateDocumentsDTO
    //{
    //    public long documentId { get; set; }
    //    public string documentPath { get; set; }
    //    public DocumentType documentType { get; set; }

    //}
    public class UpdateProductsDTO
    {
        public long productId { get; set; }
    }

}

