using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CarsSale.WebUi.Models
{
    public class Account
    {
        [Required(ErrorMessage = "The Login is required")]
        [MaxLength(50, ErrorMessage = "The Login can not be longer 50 characters")]
        public string Login { get; set; }

        [Required(ErrorMessage = "The Name is required")]
        [MaxLength(30, ErrorMessage = "Name can not be longer 30 characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "The Password is required")]
        [MaxLength(30, ErrorMessage = "The Password can not be longer 30 characters")]
        public string Password { get; set; }

        [Required(ErrorMessage = "The ConfirmPassword is required")]
        [Compare("Password", ErrorMessage = "ConfirmPassword field must be the same Password")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "The Email is required")]
        [MaxLength(50, ErrorMessage = "The Password can not be longer 50 characters")]
        public string Email { get; set; }

        [Required(ErrorMessage = "The Phone is required")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "The Birthday is required")]
        public DateTime Birthday { get; set; }
    }
}