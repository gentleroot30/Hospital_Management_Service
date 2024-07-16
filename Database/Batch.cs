using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HospitalMgmtService.Database
{
    [Index(nameof(BatchNo),IsUnique =true)]
    [Table("batch")]
    public class Batch
    {
        [Key]
        [Column("batch_id_pk")]
        public long BatchId { get; set; }

        [Required]
        [Column("product_id_fk"), ForeignKey("product_id_fk")]
        public long ProductIdFk { get; set; }//declaraction of for
        public virtual Product Product { get; set; }//creating reference

        [Required]
        [MaxLength(50)]
        [Column("batch_no")]
        public string BatchNo { get; set; }

        [Required]
        [MaxLength(50)]
        [Column("expiry_date")]
        public DateTime ExpiryDate { get; set; }

        [Required]
        [MaxLength(50)]
        [Column("pack_of")]
        public int PackOf { get; set; }

        [Required]
        [MaxLength(50)]
        [Column("mrp_per_pack")]
        public double MrpPerPack { get; set; }

        [Required]
        [MaxLength(50)]
        [Column("created_by")]
        public int CreatedBy { get; set; }

        [MaxLength(50)]
        [Column("updated_by")]
        public int? UpdatedBy { get; set; }

        [Required]
        [Column("created_at")]
        public DateTime CreatedAt { get; set; }

        [MaxLength(50)]
        [Column("updated_at")]
        public DateTime? UpdatedAt { get; set; }

        public ICollection<PurchaseRecord> PurchaseRecords { get; set; }

        public ICollection<SalesRecord> SalesRecords { get; set; }








    }
}
