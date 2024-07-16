using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;

namespace HospitalMgmtService.Database
{
    [Table("sales_record")]

    public class SalesRecord
    {
        [Required]
        [Column("sales_id_fk",Order =1), ForeignKey("sales_id_fk")]
          public long SaleIdFk { get; set; } 
          public virtual Sales Sales { get; set; }

        [Required]
        [Column("product_id_fk", Order = 2),ForeignKey("product_id_fk")] 
        public long ProductIdFk { get; set; }
        public virtual Product Product { get; set; }

        [Required]
        [Column("batch_id_fk", Order = 3), ForeignKey("batch_id_fk")]
        public long BatchIdFk { get; set; }
        public virtual Batch Batch { get; set; }

        [Required]
        [Column("applied_discount")]
        public double AppliedDiscount { get; set; }

        //[MaxLength(50)]
        //    [Column("text")]
        //    public string Text { get; set; }
        [Required]
        [Column("quantity")]
        public int Quantity { get; set; }

        [Required]
        [Column("amount")]
        public float Amount { get; set; }

        [Required]
        [Column("created_by")]
         public int CreatedBy { get; set; } 
        
        [Column("updated_by")]
        public int UpdatedBy { get; set; }
        
        [Required]
        [Column("created_at")]
         public DateTime CreatedAt { get; set; }
       
        [Column("updated_at")]
        public DateTime? UpdatedAt { get; set; }




    }
}

