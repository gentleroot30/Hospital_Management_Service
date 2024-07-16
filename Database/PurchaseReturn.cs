
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HospitalMgmtService.Database
{
    [Index(nameof(ReturnRefNo), IsUnique = true)]

    [Table("purchase_return")]
    public class PurchaseReturn  
    {
        [Key]
        [Column("return_id_pk")]
        public long ReturnId { get; set; }

        [Required]
        [Column("supplier_id_fk"), ForeignKey("supplier_id_fk")]
        public long SupplierIdFk { get; set; }
        public virtual Supplier Supplier { get; set; }

        [Required]
        [MaxLength(50)]

        [Column("return_ref_no")] 
        public string ReturnRefNo { get; set; }

        [Required]
        [MaxLength(50)]
        [Column("return_date")]
        public DateTime? ReturnDate { get; set; }

        [Required]
        [MaxLength(50)]
        [Column("total_return_bill")]
        public double TotalReturnBill { get; set; }

        [Required]
        [MaxLength(50)]
        [Column("total_return_paid")]
        public double TotalReturnPaid { get; set; }


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
        public ICollection<PurchaseReturnRecord> PurchaseReturnRecords { get; set; }

        public ICollection<PurchaseReturnPaymentRecord> PurchaseReturnPaymentRecords{ get; set; }
    }
}
