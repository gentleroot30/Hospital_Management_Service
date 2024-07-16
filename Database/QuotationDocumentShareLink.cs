using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HospitalMgmtService.Database
{
    [Table("quotation_documents_share_link")]
    public class QuotationDocumentShareLink
    {
        [Key]
        [Column("quotation_document_share_link_id_pk")]
        public long QuotationDocumentShareLinkId { get; set; }


        [Required]
        [MaxLength(50)]
        [Column("uuid")]
        public string UUID { get; set; }

        [Column("quotation_document_id_fk"), ForeignKey("quotation_document_id_fk")]
        public long QuotationDocumentIdFk { get; set; }
        public virtual QuotationDocument QuotationDocument { get; set; }



        [MaxLength(50)]
        [Column("sequence_no")]
        public long SequenceNo { get; set; }


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

       










    }
}
