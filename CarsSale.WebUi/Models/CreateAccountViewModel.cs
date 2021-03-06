﻿using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace CarsSale.WebUi.Models
{
    public class CreateAccountViewModel
    {
        [Remote("CheckLogin", "Account")]
        [Required(ErrorMessage = "The Login is required")]
        [MaxLength(50, ErrorMessage = "The Login can not be longer 50 characters")]
        public string Login { get; set; }

        [Required(ErrorMessage = "The Name is required")]
        [MaxLength(30, ErrorMessage = "Name can not be longer 30 characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "The Password is required")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        public string Password { get; set; }

        [Required(ErrorMessage = "The ConfirmPassword is required")]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "ConfirmPassword field must be the same Password")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "The Email is required")]
        [MaxLength(50, ErrorMessage = "The Password can not be longer 50 characters")]
        [EmailAddress]
        [Remote("CheckEmail", "Account")]
        public string Email { get; set; }

        [Required(ErrorMessage = "The Phone is required")]
        [DataType(DataType.PhoneNumber)]
        [Remote("CheckPhone", "Account")]
        public string Phone { get; set; }

        [DataType(DataType.Date)]
        public DateTime Birthday { get; set; }
    }
}