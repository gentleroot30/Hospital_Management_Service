using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace HospitalMgmtService.Database
{
    [Index(nameof(ProductCategoryName), IsUnique = true)]
    [Table("product_category")]

    public class ProductCategory
    {
        [Key]
        [Column("category_id_pk")]
        public long ProductCategoryId { get; set; }

        [Column("category_name")]
        public string ProductCategoryName { get; set; }

        [Column("description")]
        public string CategoryDescription { get; set; }

        [Required]
        [Column("created_by")]
        public int CreatedBy { get; set; }

        [Column("updated_by")]
         public int? UpdatedBy { get; set; }

        [Required]
        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [Column("updated_at")]
        public DateTime? UpdatedAt { get; set; } = DateTime.Now;
        public ICollection<Product> Product { get; set; }


    }
}
