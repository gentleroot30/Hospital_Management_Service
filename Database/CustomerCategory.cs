using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace HospitalMgmtService.Database
{
    [Index(nameof(CategoryName), IsUnique = true)]
    [Table("customer_category")]

    public class CustomerCategory
    {
        [Key]
        [Column("category_id_pk")]
        public long CustomerCategoryId { get; set; }

        [Required]
        [MaxLength(50)]
        [Column("category_name")]
        public string CategoryName { get; set; }

        [MaxLength(50)]
        [Column("description")]
         public string Description { get; set; }

       
        [Required]
        [Column("created_by")]
        public int CreatedBy { get; set; }

        [Column("updated_by")]
        public int? UpdatedBy { get; set; }
      
        [Required]
        [Column("created_at")]
        public DateTime CreatedAt { get; set; } 

        [Column("updated_at")]
        public DateTime? UpdatedAt { get; set; }

       public ICollection<Customer> Customers { get; set;}

    }
}
