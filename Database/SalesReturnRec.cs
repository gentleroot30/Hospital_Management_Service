
using ServiceStack;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HospitalMgmtService.Database
{

    [Table("sales_return_record")]
    public class SalesReturnRecord
    {
 

        [Key, Column("return_id_fk",Order = 0), ForeignKey("return_id_fk")]
        public long SalesReturnIdFk { get; set; }
        public virtual SalesReturn SalesReturn { get; set; }


        [Key, Column("product_id_fk", Order = 1), ForeignKey("product_id_fk")]
        public long ProductIdFk { get; set; }

        public virtual Product Product { get; set; }

        [Required]
        [Column("batch_id_fk", Order = 2),ForeignKey("batch_id_fk")]
        public long BatchIdFk { get; set; }

        public virtual Batch Batch { get; set; }

        [Required]
        [Column("return_quantity")]
        public int ReturnQuantity { get; set; }

        [Required]
        [Column("amount")]
        public int Amount { get; set; }

        [Column("created_by")]
        public int? CreatedBy { get; set; }

        [Column("updated_by")]
        public int? UpdatedBy { get; set; }

        [Required]
        [Column("created_at")]
        public DateTime? CreatedAt { get; set; }
        [Column("updated_at")]
        public DateTime? UpdatedAt { get; set; }

    }

}
