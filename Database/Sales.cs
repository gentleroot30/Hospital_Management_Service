using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HospitalMgmtService.Database
{
    [Table("sale")]
    public class Sales
    {
        [Key]
        [Column("sales_id_pk")]
        public long SalesId { get; set; }

        [Required]
        [Column("customer_id_fk"), ForeignKey("customer_id_fk")]
        public long CustomerIdFk { get; set; }
        public virtual Customer Customer { get; set; }

        [Required]
        [MaxLength(50)]
        [Column("pos_number")]
        public string PosNo { get; set; }

        [Required]
        [Column("total_bill")]
        public double TotalBill { get; set; }

        [MaxLength(50)]
        [Column("total_paid")]
        public double TotalPaid { get; set; }

        [Column("pos_date")]
        public DateTime PosDate { get; set; }

        [Required]
        [Column("created_by")]
        public int? CreatedBy { get; set; }

        [MaxLength(50)]
        [Column("updated_by")]
        public int? UpdatedBy { get; set; }


        [MaxLength(50)]
        [Column("created_at")]
        public DateTime? CreatedAt { get; set; }

        [Column("updated_at")]
        public DateTime? UpdatedAt { get; set; }


        public ICollection<SalesPayment> SalesPayments { get; set; }
        public ICollection<SalesRecord> SalesRecords { get; set; }


    }
}
