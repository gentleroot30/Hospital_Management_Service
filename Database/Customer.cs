using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata;
using System.Runtime.Serialization;

namespace HospitalMgmtService.Database
{
    [Index(nameof(CustomerCode), IsUnique = true)]
    [Table("customer")]
    public class Customer
    {
        [Key]

        [Column("customer_id_pk")]
        public long CustomerId { get; set; }

        [Required]
        [Column("category_id_fk"),ForeignKey("category_id_fk")]
        public long CustomerCategoryIdFk { get; set; } 

        public virtual CustomerCategory CustomerCategory { get; set; }  

        [Required]
        [MaxLength(50)]
        [Column("customer_code")]
        public string CustomerCode { get; set; }

        [Required]
        [MaxLength(50)]
        [Column("name")]
        public string CustomerName { get; set; }


        [Column("ethinity")]
        [MaxLength(50)]
        public string Ethnicity { get; set; }


        [Column("address")]
        [MaxLength(50)]
        public string Address { get; set; }

        [MaxLength(50)]
        [Column("custom_field_1")]
        public string CustomField_1 { get; set; }

        [MaxLength(50)]
        [Column("custom_field_2")]
        public string CustomField_2 { get; set; }

    
        [Required]
        [Phone]
        [Column("contact_no_1")]
        public string ContactNo_1 { get; set; }

       
        [Column("contact_no_2")]
        [Phone]
        public string ContactNo_2 { get; set; }

      
        [Column("contact_no_3")]
        [Phone]
        public string ContactNo_3 { get; set; }

        [Required]
        [Column("created_by")]
        public int? CreatedBy { get; set; } = 1;

        [Column("updated_by")]
        public int? UpdatedBy { get; set; }

        [Required]
        [Column("created_at")]       
        public DateTime CreatedAt{ get; set; } 

       [Column("updated_at")]
        public DateTime? UpdatedAt { get; set; }


        public ICollection<Quotation> Quotation { get; set; }
      public ICollection<Sales> Sales { get; set; }
        public ICollection<SalesReturn> SalesReturns { get; set; }

    }
   

}
