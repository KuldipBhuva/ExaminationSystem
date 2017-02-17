using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_Models.ViewModel
{
    public class QRModel
    {
        public int ScID { get; set; }
        public Nullable<int> StdID { get; set; }
        public Nullable<int> EID { get;set; }
        public int QRID { get; set; }
        public Nullable<int> ETID { get; set; }
        public Nullable<int> StudID { get; set; }
        public Nullable<int> SubID { get; set; }
        public byte[] QRCodeImage { get; set; }
        public string QRCode { get; set; }
        public Nullable<int> SchoolID { get; set; }

        public List<QRModel> ListQR { get; set; }
        public List<SchoolModel> ListSchool { get; set; }
        public List<StandardModel> ListStd { get; set; }
        public List<ExamModel> ListExam { get; set; }

        public StudentModel studDetails { get; set; }
        public ExamModel ExamDetail { get; set; }
        public SubjectModel SubDetail { get; set; }
        public SchoolModel SchoolDetail { get; set; }
        public SchoolModel CenterDetail { get; set; }
        public StandardModel StdDetail { get; set; }
    }
}
