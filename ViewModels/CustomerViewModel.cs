using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System;

namespace HospitalMgmtService.ViewModels
{
    public class CustomerViewModel
    {

        public long CustomerId { get; set; }

        public virtual long category_id_fk { get; set; }

        public string CustomerCode { get; set; }

        public string CustomerName { get; set; }

        public string Ethinity { get; set; }

        public string CustomerAddress { get; set; }

        public string CustomField1 { get; set; }

        public string CustomField2 { get; set; }


        public string CustomerContactNo1 { get; set; }

    
        public string CustomerContactNo2 { get; set; }

        public string CustomerContactNo3 { get; set; }



    }

  
}
