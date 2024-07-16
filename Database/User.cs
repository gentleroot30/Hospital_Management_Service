using Microsoft.EntityFrameworkCore;
using System.Drawing;
using System.ComponentModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata;
using System.Text.RegularExpressions;
using System.Runtime.Serialization;
namespace HospitalMgmtService.Database
{
    [Index(nameof(Email), IsUnique = true)]
    [Index(nameof(ContactNo1), IsUnique = true)]

    [Table("user")]
    public class User
    {
        [Key]
        [Column("user_id_pk")]
        public long UserId { get; set; }

        [Required]
        [ForeignKey("role_id_fk")]
        public  long RoleIdFk { get; set; }
        public virtual Role Roles { get; set; }


        [Column("user_code")]
        public string UserCode { get; set; }

        [Required]
        [MaxLength(50)]
        [Column("name")]
        public string Name { get; set; }



        [Required]
        [DataType(DataType.Password)]
        [Column("password")]
        public string Password { get; set; }


        [MaxLength(50)]
        [Column("address")]
        public string Address { get; set; }

        [Required]
        [EmailAddress]
        [Column("email")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber)]
        [Column("contact_no_1")]
        public string ContactNo1 { get; set; }

        [Column("contact_no_2")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression("^([0-9]{10})$", ErrorMessage = "Invalid Mobile Number.")]
        public string ContactNo2 { get; set; }

        [Column("contact_no_3")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression("^([0-9]{10})$", ErrorMessage = "Invalid Mobile Number.")]
        public string ContactNo3 { get; set; }

        [Column("profile_picture")]
        public byte[] ProfilePicture { get; set; }

        public enum Status { paid, unpaid}
        [Column("user_status")]
        public Status UserStatus { get; set; }


        [Required]
        [Column("created_by")]
        [IgnoreDataMember]
        public int? CreatedBy { get; set; }

        [Column("updated_by")]
        [IgnoreDataMember]
        public int? UpdatedBy { get; set; }

        [Required]
        [Column("created_at")]
        [IgnoreDataMember]
        public DateTime CreatedAt { get; set; } 

        [Column("updated_at")]
        [IgnoreDataMember]
        public DateTime? UpdatedAt { get; set; } 


    

    }
}
