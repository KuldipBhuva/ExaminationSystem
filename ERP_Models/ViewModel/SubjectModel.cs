using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_Models.ViewModel
{
    public class SubjectModel
    {    
        public bool IsChecked { get; set; }
        public Nullable<int> SubID { get; set; }
        public Nullable<int> StdID { get; set; }
        public string Subject { get; set; }
        public string Description { get; set; }
        public Nullable<bool> Status { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }


        public int ETID { get; set; }
        public Nullable<int> EID { get; set; }
        public Nullable<int> SchoolID { get; set; }
        //public Nullable<int> SubID { get; set; }
        //public Nullable<int> StdID { get; set; }
        //public Nullable<int> ACID { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        public string Timing { get; set; }
        public Nullable<decimal> TotalMarks { get; set; }
        public Nullable<decimal> PassingMarks { get; set; }
        
    }
}
