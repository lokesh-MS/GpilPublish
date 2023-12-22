using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GPILWebApp.Models
{
    public class MenuModel
    {
        public List<modules> modules { get; set; }
    }

    public class menu
    {
        public string menuName { get; set; }
        public string controllerName { get; set; }
        public string actionName { get; set; }
    }

    public class modules
    {
        public string moduleName { get; set; }

        public List<menu> submenu { get; set; }
    }
}