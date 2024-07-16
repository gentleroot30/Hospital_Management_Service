using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata;
using System.Runtime.Serialization;

namespace HospitalMgmtService.Database
{
    [Table("quotation_document")]
    public class QuotationDocument
    {
        [Key]

        [Column("quotation_document_id_pk")]
        public long QuoatationDocumentId { get; set; }

        [Required]
        [Column("quotation_id_fk"), ForeignKey("quotation_id_fk")]
        public long QuotationIdFk { get; set; } 
        public virtual Quotation Quotation { get; set; }  

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
        public DocumentType DocumentTypes { get; set; }

        [Required]
        [Column("created_by")]
        public int? CreatedBy { get; set; }

        [Column("updated_by")]
        public int? UpdatedBy { get; set; }


        [Required]
        [Column("created_at")]
        public DateTime CreatedAt { get; set; } 

        [Column("updated_at")]
        public DateTime? UpdatedAt { get; set; } 
        public ICollection<QuotationDocumentShareLink> QuotationDocumentShareLinks { get; set; }

    }

    public enum DocumentType
    {

        Word,
        Image,
        PDF
    }

}
