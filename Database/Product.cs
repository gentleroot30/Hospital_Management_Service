using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Numerics;

namespace HospitalMgmtService.Database
{
    [Table("product")]
    public class Product
    {
        [Key]
        [Column("product_id_pk")]
        public long ProductId { get; set; }

        [Required]
        [MaxLength(50)]
        [Column("product_name")]
        public string ProductName { get; set; } 

        [Column("brand_id_fk"), ForeignKey("brand_id_fk")]
        public long BrandIdFk { get; set; }

        public virtual Brand Brand { get; set; } 

        [Column("product_category_id_fk"), ForeignKey("product_category_id_fk")]
        public long ProductCategoryIdFk { get; set; } 
         public virtual ProductCategory ProductCategory { get; set; } 

        [Required]
        [Column("alert_quantity")]
        public int AlertQuantity { get; set; }

        [Column("quantity")]
        public int Quantity { get; set; }

        [Column("sequence_sorting")]
        public string SequenceStoring { get; set; }

        [Column("discount_percent")]
        public double DiscountPercent { get; set; }

        [Column("custom_field_1")]
        public string CustomField1 { get; set; }
      
        [Column("custom_field_2")]
        public string CustomField2 { get; set; } 

        [Column("custom_field_3")]

        public string CustomField3 { get; set; }


        [Required] 
        [Column("created_by")]
        public int CreatedBy { get; set; }

        [Column("updated_by")]
        public long? UpdatedBy { get; set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; set; }

        [Column("updated_at")]
        public DateTime? UpdatedAt { get; set; }

        public ICollection<Batch> Batch { get; set; }
       public ICollection<PurchaseRecord> PurchaseRecord { get; set; }
        public ICollection<PoRecords> PoRecords { get; set; }
        public ICollection<PurchaseReturnRecord> PurchaseReturnRecords { get; set; }
        public ICollection<QuotationRecord> QuotationRecords { get; set; }
        public ICollection<QuotationTempRecord> QuotationTempRecords { get; set; }
     public ICollection<SalesRecord> SalesRecords { get; set; }
        public ICollection<SalesReturnRecord> SalesReturnRecord { get; set; }


    }
}