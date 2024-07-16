using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HospitalMgmtService.Database
{
    [Index(nameof(PoNumber), IsUnique =true)]
    [Table("purchase_order")]

    public class PurchaseOrder
    {
        [Key]
        [Column("po_id_pk")]
        public long PoId { get; set; }

        [Required]
        [Column("supplier_id_fk"), ForeignKey("supplier_id_fk")]
        public long SupplierIdFk { get; set; }
        public virtual Supplier Supplier { get; set; }

        [Required]
        public string PoNumber { get; set; }

        [Required]
        public DateTime PoDate { get; set; }

        [Required]
        [MaxLength(50)]
        [Column("po_status")]
        public string PoStatus { get; set; }

        [Required]
        [Column("created_by")]
        public long CreatedBy { get; set; }

        [Column("updated_by")]
        public long? UpdatedBy { get; set; }

        [Required]
        [Column("created_at")]

        public DateTime CraetedAt { get; set; }

        [Column("updated_at")]
        public DateTime? UpdatedAt { get; set; }

        public ICollection<PoRecords> PoRecords { get; set; }



    }
}
