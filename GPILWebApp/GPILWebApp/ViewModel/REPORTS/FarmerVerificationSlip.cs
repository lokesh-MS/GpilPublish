using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using GPILWebApp.RDLCMODELS.LD;
namespace GPILWebApp.ViewModel.REPORTS
{
    public class FarmerVerificationSlip
    {
        string strsql;
        DataLoaderManagement dlMgt = new DataLoaderManagement();
        DataTable ds1 = new DataTable();
        public List<GPILWebApp.RDLCMODELS.LD.FarmerPurchase> GetDetails()
        {
            //strsql = "SELECT F.FARM_NAME AS FARM_NAME,F.FARM_FATHER_NAME AS FARM_FATHER,H.HEADER_ID AS PURCH_REF,H.DATE_OF_PURCH AS DATE_OF_PURCH,V.VILLAGE_NAME,D.FARMER_CODE AS FARMER_CODE,D.TB_LOT_NO AS LOT_NO,D.GPIL_BALE_NUMBER AS GPIL_BALE_NUMBER,F.BANK_ACCOUNT_NO AS BANK_ACCOUNT_NO,F.IFSC_CODE AS IFSC_CODE,F.BANK_NAME AS BANK_NAME,F.BRANCH_NAME AS BRANCH_NAME, FM.FREIGHT_CHARGE AS FREIGHT_CHARGE,F.LOAN_AMOUNT AS LOAN_AMOUNT,F.ALERT_MSG AS ALERT_MSG  FROM GPIL_TAP_FARM_PURCHS_DTLS D (NOLOCK),GPIL_TAP_FARM_PURCHS_HDR  H (NOLOCK),GPIL_FARMER_MASTER F (NOLOCK),GPIL_VILLAGE_MASTER V (NOLOCK),GPIL_FARMER_FREIGHT_CHARGE_MASTER FM (NOLOCK) WHERE H.HEADER_ID=D.HEADER_ID AND D.FARMER_CODE=F.FARM_CODE AND FM.VILLAGE_CODE=F.VILLAGE_CODE AND F.VILLAGE_CODE=V.VILLAGE_CODE AND FM.ORGN_CODE=H.ORGN_CODE AND FM.CROP=H.CROP AND FM.VARIETY=H.VARIETY AND H.ORGN_CODE='" + cbxOrgnCode.Text + "' AND D.FARMER_CODE='" + cbxFarmerCode.Text + "' AND H.HEADER_ID='" + cbxOrgnCode.Text + "20170609" + "' ORDER BY D.GPIL_BALE_NUMBER ";//DateTime.Now.ToString("yyyyMMdd")
            ds1 = dlMgt.GetQueryResult(strsql);


            List<GPILWebApp.RDLCMODELS.LD.FarmerPurchase> lData = new List<GPILWebApp.RDLCMODELS.LD.FarmerPurchase>();

            if (ds1.Rows.Count >= 0)
            {
                lData.Add(new GPILWebApp.RDLCMODELS.LD.FarmerPurchase() {

                    //        FARM_NAME FARM_FATHER_NAME HEADER_ID DATE_OF_PURCH VILLAGE_NAME FARMER_CODE TB_LOT_NO
                    FARM_NAME = ds1.Rows[0]["FARM_NAME"].ToString(),
                    FARM_FATHER_NAME= ds1.Rows[0]["FARM_FATHER_NAME"].ToString(),
                    HEADER_ID = ds1.Rows[0]["HEADER_ID"].ToString(),
                    DATE_OF_PURCH = ds1.Rows[0]["DATE_OF_PURCH"].ToString(),
                    VILLAGE_NAME = ds1.Rows[0]["VILLAGE_NAME"].ToString(),
                    FARMER_CODE = ds1.Rows[0]["FARMER_CODE"].ToString(),
                    TB_LOT_NO = ds1.Rows[0]["TB_LOT_NO"].ToString(),
                    //GPIL_BALE_NUMBER BANK_ACCOUNT_NO IFSC_CODE BANK_NAME BRANCH_NAME FREIGHT_CHARGE LOAN_AMOUNT
                    GPIL_BALE_NUMBER = ds1.Rows[0]["GPIL_BALE_NUMBER"].ToString(),
                    BANK_ACCOUNT_NO = ds1.Rows[0]["BANK_ACCOUNT_NO"].ToString(),
                    IFSC_CODE = ds1.Rows[0]["IFSC_CODE"].ToString(),
                    BANK_NAME = ds1.Rows[0]["BANK_NAME"].ToString(),
                    BRANCH_NAME = ds1.Rows[0]["BRANCH_NAME"].ToString(),
                    FREIGHT_CHARGE = ds1.Rows[0]["FREIGHT_CHARGE"].ToString(),
                    LOAN_AMOUNT = ds1.Rows[0]["LOAN_AMOUNT"].ToString(),
                    //ALERT_MSG ORGN_CODE CROP VARIETY
                    ALERT_MSG = ds1.Rows[0]["GPIL_BALE_NUMBER"].ToString(),
                    ORGN_CODE = ds1.Rows[0]["BANK_ACCOUNT_NO"].ToString(),
                    CROP = ds1.Rows[0]["IFSC_CODE"].ToString(),
                    VARIETY = ds1.Rows[0]["BANK_NAME"].ToString(),


                });
            }
            return lData;
        }
    }
}