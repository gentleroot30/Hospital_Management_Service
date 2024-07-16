using Dynamitey.DynamicObjects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Numerics;

namespace HospitalMgmtService.Database
{ 

[Table("role_feature")]

    public class RoleFeature
    {

        [Key, Column("feature_id_fk",Order = 0), ForeignKey("feature_id_fk")]
        public long FeatureIdFk { get; set; }
        public virtual Feature Feature { get; set; }


         [Key, Column("role_id_fk",Order = 1), ForeignKey("role_id_fk")]
         public long RoleIdFk { get; set; } 
         public virtual Role Role { get; set; }

        [Column("view_perm")]
        public bool ViewPerm { get; set; }

        [Column("add_perm")]
        public bool AddPerm { get; set; }

        [Column("edit_perm")]
        public bool EditPerm { get; set; }

        [Column("delete_perm")]
        public bool DeletePerm { get; set; }

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
    }
}
