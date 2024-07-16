using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HospitalMgmtService.Database
{
    [Index(nameof(QuotationNo), IsUnique = true)]
    [Table("quotation")]
    public class Quotation
    {
        [Key]
        [Column("quotation_id_pk")]
        public long QuotationId { get; set; }

        [Required]
        [Column("customer_id_fk"),ForeignKey("customer_id_fk")]
        public long CustomerIdFk { get; set; }
        public virtual Customer Customer { get; set; }
        
        [Required]
        [MaxLength(50)]
        [Column("quotation_no")]
        public string QuotationNo { get; set; }

        [MaxLength(50)]
        [Column("document_location")]
        public string DocumentLocation { get; set; }

        [MaxLength(50)]
        [Column("quotation_date")]
        public DateTime? QuotationDate { get; set; }
        [MaxLength(50)]
        [Column("sale_note_1")]
        public string SaleNote1 { get; set; }
        [MaxLength(50)]
        [Column("sale_note_2")]
        public string SaleNote2 { get; set; }
        [MaxLength(50)]
        [Column("sale_note_3")]
        public string SaleNote3 { get; set; }
        [MaxLength(50)]
        [Column("sale_note_4")]
        public string SaleNote4 { get; set; }
        [MaxLength(50)]
        [Column("new_field_1")]
        public string NewField1 { get; set; }
        [MaxLength(50)]
        [Column("new_field_2")]
        public string NewField2 { get; set; }

        [MaxLength(50)]
        [Column("new_field_3")]
        public string NewField3 { get; set; }

        [MaxLength(50)]
        [Column("new_field_4")]
        public string NewField4 { get; set; }

        [MaxLength(50)]
        [Column("new_field_5")]
        public string NewField5 { get; set; }

        [MaxLength(50)]
        [Column("new_field_6")]
        public string NewField6 { get; set; }

        [MaxLength(50)]
        [Column("new_field_7")]
        public string NewField7 { get; set; }

        [MaxLength(50)]
        [Column("new_field_8")]
        public string NewField8 { get; set; }


        [MaxLength(50)]
        [Column("created_by")]
        public int? CreatedBy { get; set; }

        [MaxLength(50)]
        [Column("updated_by")]
        public int? UpdatedBy { get; set; }
        [Required]
        [Column("created_at")]
        public DateTime? CreatedAt { get; set; }

        [Column("updated_at")]
        public DateTime? UpdatedAt { get; set; }
        public ICollection<QuotationRecord> QuotationRecord { get; set; }
        public ICollection<QuotationDocument> QuotationDocuments { get; set; }
        //public string SaleNote1 { get; internal set; }
    }
}
