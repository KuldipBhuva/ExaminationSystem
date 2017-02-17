using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_Models.ViewModel
{
    public class ResultModel
    {
        public int RID { get; set; }
        public Nullable<int> EID { get; set; }
        public Nullable<int> StudID { get; set; }
        public Nullable<int> SubID { get; set; }
        public string Marks { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }

        public StudentModel StudDetail { get; set; }
        public SubjectModel SubDetail { get; set; }
        public ExamModel ExamDetail { get; set; }
        public ZoneModel ZoneDetail { get; set; }
        public SchoolModel SchoolDetail { get; set; }
        public StandardModel StdDetail { get; set; }
        public DistrictModel DistDetail { get; set; }
        public ExamTranModel ExamSheduleDetail { get; set; }
    }
}
