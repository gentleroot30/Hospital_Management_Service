using System.Collections.Generic;
using System;
using HospitalMgmtService.Database;

namespace HospitalMgmtService.RequestResponseModel.RequestModel
{
    public class AddQuotationRequest
    {

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
       
        public List<ProductDTO> Products { get; set; }
    }
    
    public class ProductDTO
    {
        public long productId { get; set; }
    }


}
