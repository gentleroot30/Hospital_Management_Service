using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HospitalMgmtService.Database
{
    [Table("sales_payment")]
    public class SalesPayment
    {
        [Key]
        [Required]
        [MaxLength(50)]
        [Column("payment_id_pk")]
        public long PaymentId { get; set; }

        [Required]
        [Column("sales_id_fk"), ForeignKey("sales_id_fk")]
        public long SalesIdFk { get; set; }
        public virtual Sales Sales { get; set; }

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
    public enum  PaymentMethod
        {
        Cash,
        UPI,
        InternetBanking
      }
    
}

