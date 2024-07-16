using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;

namespace HospitalMgmtService.Database
{
    [Table("po_records")]
    public class PoRecords
    {
      
        [Required]
        [Key, ForeignKey("po_id_fk"),Column("po_id_fk",Order = 0)]
        public long PoIdFk { get; set; }
        public virtual PurchaseOrder Po { get; set; }

        [Required]
        [Key, Column("product_id_fk",Order = 1), ForeignKey("product_id_fk")]
        public long ProductIdFk { get; set; }
        public virtual Product Product { get; set; }

        [Required]
        [MaxLength(50)]
        [Column("order_quantity")]
        public string OrderQuantity { get; set; }

        [Column("purchase_note")]
        public string PurchaseNote { get; set; }



        [Required]
        [Column("created_by")]
        public int CreatedBy { get; set; }


        [Column("updated_by")]
        public int? UpdatedBy { get; set; }

        [Required]
        [Column("created_at")]
        public DateTime CreatedAt { get; set; }


        [Column("updated_at")]
        public DateTime? UpdatedAt { get; set; }

    }
}
