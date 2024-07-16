using System.ComponentModel.DataAnnotations;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace HospitalMgmtService.Database
{

    [Table("sales_return_payment_record")]
    public class SalesReturnPaymentRecord
    {
        [Key]
        [Required]
        [MaxLength(50)]
        [Column("payment_id_pk")]
        public long PaymentId { get; set; }

        [Required]
        [Column("return_id_pk"), ForeignKey("return_id_fk")]
        public long ReturnIdfk { get; set; }
        public virtual SalesReturn Sales { get; set; }

        [Required]
        [MaxLength(50)]
        [Column("payment_method")]
        public PaymentMethod PaymentMethod { get; set; }

        [Required]
        [MaxLength(50)]
        [Column("amount")]
        public double Amount { get; set; }

        [Required]
        [MaxLength(50)]
        [Column("payment_date")]
        public DateTime? PaymentDate { get; set; }


        [Required]
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
