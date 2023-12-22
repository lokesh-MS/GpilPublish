using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GPILWebApp.ViewModel
{
    public class SkipRemnantThreshingIssue
    {

        //UPDATE GPIL_THRESH_RECON_DTLS_1 SET REMARKS=BATCH_NO,BATCH_NO='2015/TH/094/P27/0142/1' WHERE  BALE_TYPE='IPB' AND GRADE LIKE 'L%' AND BATCH_NO IN (SELECT BATCH_NO FROM GPIL_THRESH_RECON_HDR (NOLOCK) WHERE STATUS='N' AND FLAG IS NULL)
        //UPDATE GPIL_THRESH_RECON_DTLS_1 SET BATCH_NO=REMARKS WHERE BATCH_NO='2015/TH/094/P27/0142/1' AND BALE_TYPE='IPB' AND GRADE LIKE 'L%' AND REMARKS IN (SELECT BATCH_NO FROM GPIL_THRESH_RECON_HDR (NOLOCK) WHERE BATCH_NO IN (SELECT REMARKS FROM GPIL_THRESH_RECON_DTLS_1 (NOLOCK) WHERE BATCH_NO='2015/TH/094/P27/0142/1' AND BALE_TYPE='IPB' AND GRADE LIKE 'L%') and STATUS='N' and FLAG='Y')
        public string SNO { get; set; }
        public string BATCH_NO { get; set; }
        public string BALE_TYPE { get; set; }
        public string GRADE { get; set; }
        public string PRODUCT_TYPE { get; set; }
        public string MARKED_WT { get; set; }
        public string ASCERTAIN_WT { get; set; }
        public string SUBINVENTORY_CODE { get; set; }

        public string GPIL_BALE_NUMBER { get; set; }
        public string REMARKS { get; set; }

        
    }
    public class ListSkipRemnantThreshingIssue
    {
        public List<SkipRemnantThreshingIssue> SkipRemnantThreshingIssues { get; set; }
    }
}