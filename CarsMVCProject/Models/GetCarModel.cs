using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarsMVCProject.Models
{
    public class GetCarModel
    {
        public int ID { get; set; }
        public string CarName { get; set; }
        public decimal Carprice { get; set; }
        public string CusName { get; set; }
        public string Email { get; set; }
        public DateTime ManufacturingDate { get; set; }
        public int CID { get; set; }

        public string UploadImage { get; set; }

    }
}