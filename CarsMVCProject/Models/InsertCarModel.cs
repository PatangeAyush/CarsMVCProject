using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CarsMVCProject.Models
{
    public class InsertCarModel
    {
        [Required (ErrorMessage = "Enter car name")]
        public string CarName { get; set; }
        //public string CarColour { get; set; }
        [Required (ErrorMessage = "Enter car price")]
        public decimal Carprice { get; set; }
        [Required (ErrorMessage ="Enter Your name ")]
        public string CusName { get; set; }
        [Required(ErrorMessage ="Enter Email")]
        public string Email { get; set; }

        [Display (Name = "Select Colour")]
        [Required (ErrorMessage = "Please Select Car Colour")]
        public int CID { get; set; }

        [Required(ErrorMessage = "Please upload Images")]

        public string UploadImage { get; set; }
        public HttpPostedFileBase file { get; set; }

         
    }
}