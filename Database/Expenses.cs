using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HospitalMgmtService.Database
{
    [Table("expense")]
    public class Expense
    {
        [Key]
        [Column("expense_id_pk")]
        public long ExpenseId { get; set; }

      
        [Column("expense_cat_id_fk"),ForeignKey("expense_cat_id_fk")]
        public long ExpenseCategoryIdFK { get; set; } 
        public virtual ExpenseCategory ExpenseCategory { get; set; }  

        [Column("amount")]
        public double Amount { get; set; }

        [Column("date")]

        public DateTime? Date { get; set; }
        [Column("note")]
        
        public string Note { get; set; }
  
        
        [MaxLength(50)]
        [Column("created_by")]
        public int? CreatedBy { get; set; }

        [MaxLength(50)]
        [Column("updated_by")]
        public int? UpdatedBy { get; set; }


        [Required]
        [Column("created_at")]
        public DateTime? CreatedAt { get; set; }
        
        [MaxLength(50)]
        [Column("updated_at")]
        public DateTime? UpdatedAt { get; set; }

  
    }
}
