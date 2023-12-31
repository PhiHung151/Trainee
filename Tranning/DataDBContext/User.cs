﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Tranning.DataDBContext
{
    public class User
    {
        [Key]
        public int id { get; set; }

        [ForeignKey("role_id")]
        public int role_id { get; set; }

        [Column("username", TypeName = "Varchar(50)"), Required]
        public string username { get; set; }

        [Column("password", TypeName = "Varchar(50)"), Required]
        public string password { get; set; }

        [Column("email", TypeName = "Varchar(50)"), Required]
        public string email { get; set; }
        [Column("phone", TypeName = "Varchar(20)"), Required]
        public string phone { get; set; }
        [Column("address", TypeName = "Varchar(100)"), AllowNull]
        public string? address { get; set; }

        [Column("avatar", TypeName = "Varchar(100)"), AllowNull]
        public string? avatar { set; get; }

        [Column("gender", TypeName = "Varchar(50)"), Required]
        public string gender { set; get; }
       
        public int status { get; set; }
        [Column("full_name", TypeName = "Varchar(50)"), Required]
        public string full_name { get; set; }
        [Column("birthday"), AllowNull]
        public DateTime? birthday { get; set; }
        [Column("last_login"), AllowNull]
        public DateTime? last_login { get; set; }
        [Column("last_logout"), AllowNull]
        public DateTime? last_logout { get; set; }


        [AllowNull]
        public DateTime? created_at { get; set; }
        [AllowNull]
        public DateTime? updated_at { get; set; }
        [AllowNull]
        public DateTime? deleted_at { get; set; }

    }
}