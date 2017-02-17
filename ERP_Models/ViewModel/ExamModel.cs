using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_Models.ViewModel
{
    public class ExamModel
    {
        
        public Nullable<int> StudID { get; set; }
        public Nullable<int> SubID { get; set; }
        [Required(ErrorMessage = "Marks Required")]
        public string Marks { get; set; }
        [Required(ErrorMessage = "Code Required")]
        public string QRCode { get; set; }
        [Required(ErrorMessage = "City Required")]
        public Nullable<int> CID { get; set; }
        [Required(ErrorMessage = "Zone Required")]
        public Nullable<int> ZID { get; set; }
        public Nullable<int> CenterID { get; set; }
        public int EID { get; set; }
        [Required(ErrorMessage = "Standard Required")]
        public Nullable<int> StdID { get; set; }
        public Nullable<int> ACID { get; set; }
        [Required(ErrorMessage = "School Required")]
        public Nullable<int> SchoolID { get; set; }
        [Required(ErrorMessage = "Session Required")]
        public string Session { get; set; }
        [Required(ErrorMessage = "Name Required")]
        public string Name { get; set; }
        public Nullable<bool> Status { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }

        public List<ExamTranModel> ListSchedule { get; set; }

        public List<CityModel> ListCity { get; set; }
        public List<ZoneModel> ListZone { get; set; }
        public List<SchoolModel> ListSchool { get; set; }
        public List<StandardModel> ListStd { get; set; }
        public List<ExamModel> ListExam { get; set; }
        public List<StudentModel> ListStud { get; set; }

        public SubjectMaster SubDetail { get; set; }
        public ResultMaster ResultDetail { get; set; }
        public StudentMaster StudDetail { get; set; }
        public StandardMaster StdDetail { get; set; }
        public StandardModel StandardDetail { get; set; }
        public SchoolModel SchoolDetail { get; set; }

        public List<ResultModel> ListResult { get; set; }
        public List<SubjectModel> ListSub { get; set; }
        public ExamMaster ExamDetails { get; set; }
        public SchoolMaster SchoolMastDetail { get; set; }
    }
}
