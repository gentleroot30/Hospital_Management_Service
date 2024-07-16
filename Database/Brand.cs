using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HospitalMgmtService.Database
{
    [Index(nameof(BrandName), IsUnique =true)]
    [Table("brand")]
    public class Brand
    {
        [Key]
        [Column("brand_id_pk")]
        public long BrandId { get; set; }

        [Required]
        [MaxLength(50)]
        [Column("brand_name")]
        public string BrandName { get; set; }


        [Column("description")]
        [MaxLength(50)]
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

     public ICollection<Product>Product { get; set; }


    }
}
