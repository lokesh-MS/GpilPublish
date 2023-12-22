using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GPILWebApp.ViewModel.Verificationn
{
    public class UserMaster
    {

        public int SNO { get; set; }

        //[Required(ErrorMessage = "UserID is required")]
        //[StringLength(20, MinimumLength = 4)]
        public string USER_ID { get; set; }
        public string USER_NAME { get; set; }

        //[Required(ErrorMessage = "Password is required")]
        //[StringLength(10, MinimumLength = 4)]
        public string PASSWORD { get; set; }


        //[NotMapped] // Does not effect with your database
        //[Compare("PASSWORD")]
        public string ConfirmPassword { get; set; }


        public string USER_ERP_NAME { get; set; }

        //[Required(ErrorMessage = "EmpCode is required")]
        public string EMP_CODE { get; set; }

        //[Required(ErrorMessage = "Designation is required")]
        public string DESIGNATION { get; set; }

        //[Required(ErrorMessage = "Department is required")]
        public string DEPARTMENT { get; set; }

        //[Required(ErrorMessage = "UserRights is required")]
        public string USER_RIGHTS { get; set; }
        public string SYNC_ID { get; set; }
        public string SYNC_PASSWORD { get; set; }

        //[Required(ErrorMessage = "MobileNO is required")]
        public string MOBILE_NO { get; set; }

        //[Required(ErrorMessage = "EmailID is required")]
        public string EMAIL_ID { get; set; }

        public string Message { get; set; }
        public string CREATED_BY { get; set; }
        public Nullable<System.DateTime> CREATED_DATE { get; set; }
        public string LAST_UPDATED_BY { get; set; }
        public Nullable<System.DateTime> LAST_UPDATED_DATE { get; set; }

        //[Required(ErrorMessage = "Status is required")]
        public string STATUS { get; set; }
        public string FLAG { get; set; }
        public byte[] LASTUPDATE { get; set; }
       
    }
}