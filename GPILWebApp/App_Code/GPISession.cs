#region File Header
/*
--------------------------------------
TeamLiftss IT Systems & solutions pvt. ltd.
Copyright (c) 2021, All rights reserved

Author: ANANDARAJ G 

Revision History:
Rev   Date        Who              Description
1.0   28/July/2021   Anandaraj G   Intial version.
--------------------------------------
*/
#endregion

using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Web.SessionState;
using System.Collections.Generic;

/// <summary>
/// This class holds session values inside properties.
/// </summary>
public class GPISession
{
    private HttpSessionState userHttpSessionState;

    public GPISession(HttpSessionState httpSessionState)
    {
        userHttpSessionState = httpSessionState;
    }

    public void AbandonSession()
    {
        //userHttpSessionState.Clear();
        userHttpSessionState.Abandon();
    }
    public string UserID
    {
        get
        {
            return (string)
                userHttpSessionState[SessionKey.UserID];
        }
        set { userHttpSessionState[SessionKey.UserID] = value; }
    }

    public string UserName
    {
        get
        {
            return (string)
                userHttpSessionState[SessionKey.UserName];
        }
        set { userHttpSessionState[SessionKey.UserName] = value; }
    }

    public string UserType
    {
        get
        {
            return (string)
                userHttpSessionState[SessionKey.UserType];
        }
        set { userHttpSessionState[SessionKey.UserType] = value; }
    }
}