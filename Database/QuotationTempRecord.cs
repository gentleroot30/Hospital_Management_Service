using System.ComponentModel.DataAnnotations;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace HospitalMgmtService.Database
{
    [Table("quotation_temp_record")]
    public class QuotationTempRecord
    {
        
        [Key,Column("template_id_fk", Order = 0), ForeignKey("template_id_fk")]
        public long TemplateIdFk { get; set; }
        public virtual QuotationTemp QuotationTemp { get; set; }

        [Key,Column("product_id_fk", Order = 1), ForeignKey("product_id_fk")]
        public long ProductIdFk { get;set; }
        public virtual Product Products { get; set; }


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
}
