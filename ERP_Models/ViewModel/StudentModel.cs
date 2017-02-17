using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_Models.ViewModel
{
    public class StudentModel
    {
        public int StudID { get; set; }
        public string Prefix { get; set; }
        public Nullable<int> DID { get; set; }
        [Required(ErrorMessage = "City Required")]
        public Nullable<int> CID { get; set; }
        public Nullable<int> ZID { get; set; }
        [Required(ErrorMessage = "School Required")]
        public Nullable<int> SchoolID { get; set; }
        [Required(ErrorMessage = "Standard Required")]
        public Nullable<int> StdID { get; set; }
        public Nullable<int> ACID { get; set; }
        public string FormNo { get; set; }
        [Required(ErrorMessage = "First Name Required")]
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        [Required(ErrorMessage = "Last Name Required")]
        public string LastName { get; set; }
        public string FatherName { get; set; }
        public string MotherName { get; set; }
        [Required(ErrorMessage = "Gender Required")]
        public Nullable<int> Gender { get; set; }
        public Nullable<System.DateTime> DOB { get; set; }
        public string RollNo { get; set; }
        public string Photo { get; set; }
        public string Sign { get; set; }
        public string Thumb { get; set; }
        public Nullable<bool> Verified { get; set; }
        public Nullable<bool> Status { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }

        public List<StudentModel> ListStud { get; set; }
        public List<DistrictModel> ListDist { get; set; }
        public List<CityModel> ListCity { get; set; }
        public List<ZoneModel> ListZone { get; set; }
        public List<SchoolModel> ListSchool { get; set; }
        public List<StandardModel> ListStd { get; set; }
        public List<SubjectModel> ListSub { get; set; }

        public StandardModel StdDetail { get; set; }
        public DistrictModel DistDetail { get; set; }
        public ZoneModel ZoneDetail { get; set; }
        public ExamMaster ExamDetail { get; set; }
        public SchoolMaster SchoolDetail { get; set; }
        public SchoolModel SchlDetail { get; set; }
        public AccountingYear AcYearDetail { get; set; }
    }
}
