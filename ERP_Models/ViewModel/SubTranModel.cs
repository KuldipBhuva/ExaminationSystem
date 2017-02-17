using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_Models.ViewModel
{
    public class SubTranModel
    {
        public int STID { get; set; }
        public Nullable<int> StudID { get; set; }
        public Nullable<int> SubID { get; set; }
        public Nullable<bool> IsChecked { get; set; }
    }
}
