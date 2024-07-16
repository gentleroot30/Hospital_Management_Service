using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HospitalMgmtService.Database
{
    [Table("quotation_record")]
    public class QuotationRecord
    {

        [Key, Column("quotation_id_fk", Order = 0), ForeignKey("quotation_id_fk")]
        public long QuotationIdFk { get; set; }
        public virtual Quotation Quotation { get; set; }

        [Key, Column("product_id_fk", Order = 1), ForeignKey("product_id_fk")]
        public long ProductIdFk { get; set; }
        public virtual Product Product { get; set; }


        [MaxLength(50)]
        [Column("new_field_1")]
        public string NewField1 { get; set; }

        [MaxLength(50)]
        [Column("new_field_2")]
        public string NewField2 { get; set; }
   
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
