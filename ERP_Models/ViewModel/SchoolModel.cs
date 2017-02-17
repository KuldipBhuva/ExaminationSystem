using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_Models.ViewModel
{
    public class SchoolModel
    {
        public int SchoolID { get; set; }
        
        public Nullable<int> DID { get; set; }
        [Required(ErrorMessage = "City Required")]
        public Nullable<int> CID { get; set; }
        [Required(ErrorMessage = "Zone Required")]
        public Nullable<int> ZID { get; set; }
        [Required(ErrorMessage = "Name Required")]
        public string Name { get; set; }
        public string CenterCode { get; set; }
        public string Prefix { get; set; }
        public string Email { get; set; }        
        public Nullable<decimal> Phone { get; set; }
        public string Address { get; set; }
        public string Pincode { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string Principal { get; set; }
        public string VicePrincipal { get; set; }
        public string Website { get; set; }
        public string Logo { get; set; }
        public string Sign { get; set; }
        public Nullable<bool> Status { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }

        public List<SchoolModel> ListSchool { get; set; }
        public List<CityModel> ListCity { get; set; }
        public List<ZoneModel> ListZone { get; set; }
    }
}
