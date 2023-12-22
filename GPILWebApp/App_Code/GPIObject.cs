#region File Header
/*
--------------------------------------
TeamLiftss IT Systems & solutions pvt. ltd.
Copyright (c) 2021, All rights reserved

Author      : ANANDARAJ G 


Revision History:
Rev   Date                   Who                    Description
1.0   28/July/2021          Anandaraj G            Intial version.
--------------------------------------
*/
#endregion
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Reflection;

namespace GPI
{
    public abstract class GPIObject
    {
        internal GPIObject() { }
       
       DataServerSync dataServer = new DataServerSync();
       
        protected internal string Version
        {
            get
            {
                return "1.0";
            }
        }

        internal DataServerSync ODataServer
        {
            get
            {
                return dataServer;
            }
        }
        protected internal void ExceptionHandler(Exception ex)
        {
            throw ex;
        }

        protected internal bool CompareObject<T>(T x, T y)
        {
            Type type = typeof(T);
            PropertyInfo[] properties = type.GetProperties();// (BindingFlags.DeclaredOnly | BindingFlags.Public);
            //FieldInfo[] fields = type.GetFields(BindingFlags.DeclaredOnly | BindingFlags.Public);
            int compareValue = 0;

            foreach (PropertyInfo property in properties)
            {
                IComparable valx = property.GetValue(x, null) as IComparable;
                if (valx == null)
                    continue;
                object valy = property.GetValue(y, null);
                compareValue = valx.CompareTo(valy);
                if (compareValue != 0)
                    return false;
            }
            /*
            foreach (FieldInfo field in fields)
            {
                IComparable valx = field.GetValue(x) as IComparable;
                if (valx == null)
                    continue;
                object valy = field.GetValue(y);
                compareValue = valx.CompareTo(valy);
                if (compareValue != 0)
                    return false;
            }
            */
            return (compareValue == 0 ? true : false);
        }


    }
}