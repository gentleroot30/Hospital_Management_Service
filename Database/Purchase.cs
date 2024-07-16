using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace HospitalMgmtService.Database
{
    [Index(nameof(InvoiceNumber), IsUnique = true)]

    [Table("purchase")]
    public class Purchase
    {

        [Key]
        [Column("purchase_id_pk")]
        public long PurchaseId { get; set; }


        [Required]
        [Column("supplier_id_fk"),ForeignKey("supplier_id_fk")]
        public long SupplierIdFk { get; set; }
        public virtual Supplier Supplier { get; set; }


        [Column("invoice_number")]
        public string InvoiceNumber { get; set; }

        [Required]
        [Column("purchase_date")]
        public DateTime PurchaseDate { get; set; }

        [Required]
        [Column("total_bill")]
        public double TotalBill { get; set; }

        [Required]
        [Column("total_paid")]
        public double TotalPaid { get; set; }

        //[Required]
        //[Column("document_location")]
        //public string DocumentLocation { get; set; }

        [Required]
        [Column("created_by")]
        public long CreatedBy { get; set; }


       
        [Column("updated_by")]
        [IgnoreDataMember]
        public long? UpdatedBy { get; set; }

        [Required]
        [Column("created_at")]
        public DateTime CreatedAt { get; set; }

        
        [Column("updated_at")]
        public DateTime? UpdatedAt { get; set; }


       public ICollection<PurchaseRecord> PurchaseRecords{ get; set; }
    public ICollection<PurchaseDocument> PurchaseDocuments { get; set; }
        public ICollection<PurchasePayment> PurchasePayments { get; set; }
    }
}
