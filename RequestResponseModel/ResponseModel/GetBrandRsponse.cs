using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.VisualBasic;
using System;

namespace HospitalMgmtService.RequestResponseModel.ResponseModel
{
    public class GetBrandResponse
    {
        public long brandId { get; set; }   
        public string brandName { get; set; }
        public string description { get; set; }
        public DateTime createdAt { get; set; }
        public int createdBy { get; set; }
    }
}
