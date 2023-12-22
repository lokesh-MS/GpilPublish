#region File Header
/*
--------------------------------------
TeamLiftss IT Systems & solutions pvt. ltd.
Copyright (c) 2021, All rights reserved

Author      : ANANDARAJ G 
Description : Enum creation

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

namespace Pfizer
{
    public enum ParamType
    {
        INTEGER, VARCHAR, DATETIME, BOOLEAN, NTEXT
    }
    public enum ParamDirection
    {
        INPUT, OUTPUT
    }
    public enum ExceptionKind
    {
        Add = 1,
        Edit = 2,
        Delete = 3,
        Select = 4,

    }
}