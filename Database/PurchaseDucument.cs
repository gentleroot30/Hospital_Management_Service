using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HospitalMgmtService.Database
{
    [Table("purchase_document")]
    public class PurchaseDocument
    {
        [Key]
        [Column("prchase_document_id_pk")]
        public long PurchaseDocumentId { get; set; }

        [Required]
        [Column("purchase_id_fk"), ForeignKey("purchase_id_fk")]
        public long PurchaseIdFk { get; set; }
        public virtual Purchase Purchase { get; set; }

        [Required]
        [MaxLength(50)]
        [Column("document_name")]
        public string DocumentName { get; set; }

        [Required]
        [MaxLength(50)]
        [Column("document_path")]
        public string DocumentPath { get; set; }

       
        [Required]
        [MaxLength(50)]
        [Column("document_type")]
        public PurchaseDocumentType DocumentTypes { get; set; }


        [Required]
        [MaxLength(50)]
        [Column("created_by")]
        public int CreatedBy { get; set; }

        [MaxLength(50)]
        [Column("updated_by")]
        public int UpdatedBy { get; set; }


        [Required]
        [Column("created_at")]
        public DateTime CreatedAt { get; set; } 

        [MaxLength(50)]
        [Column("updated_at")]
        public DateTime? UpdatedAt { get; set; }

    
        public ICollection<PurchaseDocumentShareLink> PurchaseDocumentShareLinks { get; set; }
        public ICollection<Purchase> Purchases { get; set; }
    }
    public enum PurchaseDocumentType
    {
        Word,
        Image,
        PDF
    }

}
