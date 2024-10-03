using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CarsMVCProject.Models
{
    public class UpdateCarModel
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Please enter Car Name")]
        public string CarName { get; set; }

        [Required(ErrorMessage = "Please Enter car Price")]
        public decimal Carprice { get; set; }

        [Required(ErrorMessage = "Please Enter Your name")]
        public string CusName { get; set; }

        [Required(ErrorMessage = "Please Enter Your Email")]
        public string Email { get; set; }

        [Display(Name = "Select Colour")]
        [Required(ErrorMessage = "Please Select Car Colour")]
        public int CID { get; set; }
    }
}