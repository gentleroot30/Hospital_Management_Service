using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata;

namespace HospitalMgmtService.Database
{
    [Index(nameof(FeatureName), IsUnique = true)]

    [Table("feature")]
    public class Feature
    {

        [Key]
        [Column("feature_id_pk")]
        public long FeatureId { get; set; }

        [Required]
        [MaxLength(50)]
        [Column("feature_name")]
        public string FeatureName { get; set; }

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
        public DateTime? CreatedAt { get; set; }

        [Column("updated_at")]
        public DateTime? UpdatedAt { get; set; }

        public ICollection<RoleFeature> RoleFeatures { get; set; }

    }
}
