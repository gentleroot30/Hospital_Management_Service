using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace HospitalMgmtService.Database
{
    [Index(nameof(SupplierName), IsUnique=true)]
    [Index(nameof(ContactNo1), IsUnique = true)]

    [Table("supplier")]
   public  class Supplier
    {
        [Key]
        [Column("supplier_id_pk")]
        public long SupplierId { get; set; }

        [Column("supplier_code")]
        public string SupplierCode { get; set; }

        [Required]
        [MaxLength(50)]
        [Column("name")]
        public string SupplierName { get; set; }

        [Column("address")]
        public string Address { get; set; }

        [Required]
        [MaxLength(50)]
        [Column("contact_no_1")]
        public string ContactNo1 { get; set; }

        [MaxLength(50)]
        [Column("contact_no_2")]
        public string ContactNo2 { get; set; }

        [MaxLength(50)]
        [Column("contact_no_3")]
        public string ContactNo3 { get; set; }

        [Required]
        [Column("created_by")]
        public int CreatedBy { get; set; }

        [Column("updated_by")]
        public int? UpdatedBy { get; set; }

        [Required]
        [Column("created_at")]
        public DateTime CreatedAt { get; set;} 

        [Column("updated_at")]
        public DateTime? UpdatedAt { get; set; } 

        public ICollection<Purchase> Purchases { get; set;}
        public ICollection<PurchaseReturn> PurchaseReturns { get; set;}
        public ICollection<PurchaseOrder> PurchaseOrders { get; set; }
    }


   
}
