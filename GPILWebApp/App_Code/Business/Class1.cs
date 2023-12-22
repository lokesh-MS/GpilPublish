using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GPILWebApp.App_Code.Business
{
    public class Class1
    {
        private string m_barcode = null;
        private string m_materialOutType = null;
        private string m_storeRoomNo = null;
        private string m_palletId = null;
        private string m_rackNo = null;

        private string m_locNO = null;
        private string m_poNo = null;
        private string m_cargoNo = null;
        private string m_matTemp = null;
        private string m_containerNo = null;

        

        private string m_trailerOrVehNo = null;
        private string m_sealNo = null;
        private string m_monitoredBy = null;
        private string m_approvedBy = null;
        private string m_remarks = null;

        

        private DateTime m_dateAndTime;
        private string m_status = null;        
        private string m_updatedBy = null;
        private DateTime m_updatedDate;

        
        public string barcode
        {
            get { return m_barcode; }
            set { m_barcode = value; }
        }


        public string materialOutType
        {
            get { return m_materialOutType; }
            set { m_materialOutType = value; }
        }

        public string storeRoomNo
        {
            get { return m_storeRoomNo; }
            set { m_storeRoomNo = value; }
        }
        public string palletId
        {
            get { return m_palletId; }
            set { m_palletId = value; }
        }
        public string rackNo
        {
            get { return m_rackNo; }
            set { m_rackNo = value; }
        }


        public string locNO
        {
            get { return m_locNO; }
            set { m_locNO = value; }
        }
        public string poNo
        {
            get { return m_poNo; }
            set { m_poNo = value; }
        }
        public string cargoNo
        {
            get { return m_cargoNo; }
            set { m_cargoNo = value; }
        }
        public string matTemp
        {
            get { return m_matTemp; }
            set { m_matTemp = value; }
        }
        public string containerNo
        {
            get { return m_containerNo; }
            set { m_containerNo = value; }
        }

        public string trailerOrVehNo
        {
            get { return m_trailerOrVehNo; }
            set { m_trailerOrVehNo = value; }
        }
        public string sealNo
        {
            get { return m_sealNo; }
            set { m_sealNo = value; }
        }
        public string monitoredBy
        {
            get { return m_monitoredBy; }
            set { m_monitoredBy = value; }
        }
        public string approvedBy
        {
            get { return m_approvedBy; }
            set { m_approvedBy = value; }
        }
        public string remarks
        {
            get { return m_remarks; }
            set { m_remarks = value; }
        }

        public DateTime dateAndTime
        {
            get { return m_dateAndTime; }
            set { m_dateAndTime = value; }
        }
        public string status
        {
            get { return m_status; }
            set { m_status = value; }
        }
        public string updatedBy
        {
            get { return m_updatedBy; }
            set { m_updatedBy = value; }
        }
        public DateTime updatedDate
        {
            get { return m_updatedDate; }
            set { m_updatedDate = value; }
        }


    }
}