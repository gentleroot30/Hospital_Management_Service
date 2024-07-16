using Dynamitey.DynamicObjects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HospitalMgmtService.Database
{
    [Index(nameof(RoleName), IsUnique = true)]
    [Table("role")]
    public class Role
    {
        [Key]
        [Column("role_id_pk")]
        public long RoleId { get; set; }

        [Required]
        [MaxLength(50)]
        [Column("role_name")]
        public string RoleName { get; set; }

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


        public ICollection<User> User { get; set; }
        public ICollection<RoleFeature> RoleFeatures { get; set; }


    }
}
