using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HospitalMgmtService.Database
{
    [Table("purchase_return_record")]

    public class PurchaseReturnRecord
    {


        [Key]
        [Column("return_id_fk", Order = 0)]
        [ForeignKey("return_id_fk")]
        public long ReturnIdFk { get; set; }
        public PurchaseReturn PurchaseReturn { get; set; }


        [Key]
        [Column("product_id_fk", Order = 1)]
        [ForeignKey("product_id_fk")]
        public long ProductIdFk { get; set; }

        public Product Product { get; set; }

        [Required]
        [Column("batch_id_fk"),ForeignKey("batch_id_fk")]
        public long BatchIdFk { get; set; }

        public Batch Batch { get; set; }

        [Required]
        [Column("return_quantity")]
        public int ReturnQuantity { get; set; }

        [Required]
        [Column("amount")]
        public int? Amount { get; set; }

        [Required]
        [Column("created_by")]
        public int? CreatedBy { get; set; }

      
        [Column("updated_by")]
        public int? UpdatedBy { get; set; }
        [Column("created_at")]
        public DateTime? CreatedAt { get; set; }

        [Column("updated_at")]
        public DateTime? UpdatedAt { get; set; }
    }
}
