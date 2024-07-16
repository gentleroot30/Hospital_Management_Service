using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace HospitalMgmtService.Database
{
    [Table("quotation_temp")]
    public class QuotationTemp
    {
        [Key]
        [Column("template_id_pk")]
        public long TemplateId { get; set; }

        [Required]
        [MaxLength(50)]
        [Column("name")]
        public string Name { get; set; }


        [MaxLength(50)]
        [Column("description")]
        public string Description { get; set; }

        [Required]
        [Column("created_by")]
        [IgnoreDataMember]
        public int? CreatedBy { get; set; }

        [Column("updated_by")]
        [IgnoreDataMember]
        public int? UpdatedBy { get; set; }


        [Required]
        [Column("created_at")]
        public DateTime? CreatedAt { get; set; }

        [Column("updated_at")]
        public DateTime? UpdatedAt { get; set; }

        public ICollection<QuotationTempRecord> QuotationTempRecords { get; set; }

    }
}
