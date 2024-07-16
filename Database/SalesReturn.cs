using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net.Mail;
using System.Runtime.Serialization;

namespace HospitalMgmtService.Database
{
    [Index(nameof(ReturnRefNo), IsUnique = true)]
    
    [Table("sales_return")]
    public class SalesReturn
    {
        [Key]
        [Column("return_id_pk")]
        public long ReturnId { get; set; }

        [Required]
        [Column("customer_id_fk"), ForeignKey("customer_id_fk")]
        public long CustomerIdFk { get; set; }
        public virtual Customer Customers { get; set; }
       
        [Column("return_ref_no")]
        public string ReturnRefNo { get; set; }

        [Required]
        
        [Column("return_date")]
        public DateTime ReturnDate { get; set; }
      
        [Column("total_return_amount")]
        public double TotalReturnAmount { get; set; }


        [Column("total_return_amount_paid")]
        public double TotalReturnAmountPaid { get; set; }

        [Required]
        [Column("created_by")]
        public long CreatedBy { get; set; }

        
        [Column("updated_by")]
        public long? UpdatedBy { get; set; }

        [Required]
        [Column("created_at")]
        public DateTime CreatedAt { get; set; }
       
        [Column("updated_at")]
        public DateTime? UpdatedAt { get; set; }

        public ICollection<SalesReturnRecord> SalesReturnRecord { get; set; }

        public ICollection<SalesReturnPaymentRecord> SalesReturnPayment { get; set; }
    }
}
