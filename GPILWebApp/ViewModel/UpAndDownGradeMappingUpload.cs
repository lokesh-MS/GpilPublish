using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GPILWebApp.ViewModel
{
    public class UpAndDownGradeMappingUpload
    {
      //   ,[PAIR_CODE],[BUYER_GRADE_GRP],[CLASSIFIER_GRADE_GRP],[PAIR_TYPE],[PAIR_DESC],[CREATED_BY],[CREATED_DATE],[LAST_UPDATED_BY]
      //,[LAST_UPDATED_DATE],[STATUS],[FLAG],[LASTUPDATE],[ATTRIBUTE1],[ATTRIBUTE2],[ATTRIBUTE3],[ATTRIBUTE4],[ATTRIBUTE5]

        public string CROP { get; set; }
        public string VARIETY { get; set; }
        public string BUYER_GRADE_GRP { get; set; }
        public string CLASSIFIER_GRADE_GRP { get; set; }
        public string PAIR_TYPE { get; set; }
        public string INS_STS { get; set; }
    }

    public class ListUpAndDownGradeMappingUpload
    {
        public List<UpAndDownGradeMappingUpload> UpAndDownGradeMappingUploads { get; set; }
    }
}
