using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GPILWebApp.ViewModel
{
    public  class Propertycls
    {
        private string m_ProbertyStrLastCropYear = null;


        public string ProbertyStrLastCropYear
        {
            get { return m_ProbertyStrLastCropYear; }
            set { m_ProbertyStrLastCropYear = value; }
        }

    }
}