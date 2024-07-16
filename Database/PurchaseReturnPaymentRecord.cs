using System.ComponentModel.DataAnnotations;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace HospitalMgmtService.Database
{

    [Table("purchase_return_payment_record")]
    public class PurchaseReturnPaymentRecord
    {
        [Key]
        [Column("payment_id_pk")]
        public long PaymentId { get; set; }


        [Required]
        [Column("return_id_fk"), ForeignKey("return_id_pk")]
        public long ReturnIdFk { get; set; }

        public virtual PurchaseReturn Purchase { get; set; }

        [Required]
        [MaxLength(50)]
        [Column("receiver_name")]
        public string ReceiverName { get; set; }


        [Column("receiver_contact")]
        public string ReceiverContact { get; set; }

        [Required]
        [MaxLength(50)]
        [Column("payment_method")]
        public PaymentMethod PaymentMethod { get; set; }

        [Column("amount")]
        public double Amount { get; set; }

        [Required]
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