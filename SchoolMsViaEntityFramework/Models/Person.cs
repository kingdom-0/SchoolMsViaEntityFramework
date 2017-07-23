﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SchoolMsViaEntityFramework.Models
{
    public class Person
    {
        public int ID { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name ="Last Name")]
        public string LastName { get; set; }

        [Required]
        [StringLength(50,ErrorMessage ="First name cannot be longer than 50 characters.")]
        [Display(Name = "First Name")]
        [Column("FirstName")]
        public string FirstMidName { get; set; }

        [Display(Name ="Full Name")]
        public string FullName
        {
            get
            {
                return string.Concat("LastName", ",", "FirstName");
            }
        }
    }
}