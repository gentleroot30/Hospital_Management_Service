using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HospitalMgmtService.Database
{
    [Table("purchase_record")]
    public class PurchaseRecord
    {
        [Required]
        [Key, Column("purchase_id_fk", Order = 1), ForeignKey("purchase_id_fk")]
        public long PurchaseIdFk { get; set; }
        public Purchase Purchase { get; set; }


        [Required]
        [Key, Column("product_id_fk", Order = 2), ForeignKey("product_id_fk")]
        public long ProductIdFk { get; set; }
        public virtual Product Product { get; set; }

        [Required]
        [Column("batch_id_fk"),ForeignKey("batch_id_fk")]
        public long BatchIdFk { get; set; }
        public virtual Batch Batch { get; set; }


        [Required]
        [MaxLength(50)]
        [Column("order_quantity")]
        public int OrderQuantity { get; set; }

   
        [Required]
        [MaxLength(50)]
        [Column("created_by")]
        public int CreatedBy { get; set; }


        [MaxLength(50)]
        [Column("updated_by")]
        public int? UpdatedBy { get; set; }

        [Required]
        [MaxLength(50)]
        [Column("created_at")]
        public DateTime CreatedAt { get; set; }

        [MaxLength(50)]
        [Column("updated_at")]
        public DateTime? UpdatedAt { get; set; }

    }
}
