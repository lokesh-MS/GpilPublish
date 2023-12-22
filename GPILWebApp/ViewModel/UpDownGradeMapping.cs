using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GPILWebApp.ViewModel
{
    public class UpDownGradeMapping
    {

        public string SNO { get; set; }
        public string CROP { get; set; }
        public string VARIETY { get; set; }
        public string BUYER_GRADE_GRP { get; set; }
        public string CLASSIFIER_GRADE_GRP { get; set; }
        public string PAIR_TYPE { get; set; }
        public string PAIR_CODE { get; set; }
        public string INS_STS { get; set; }
    }
    public class ListUpDownGradeMapping
    {
        public List<UpDownGradeMapping> UpDownGradeMappingNew { get; set; }
    }
}