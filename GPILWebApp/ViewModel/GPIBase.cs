#region File Header
/*
--------------------------------------
TeamLiftss IT Systems & solutions pvt. ltd.
Copyright (c) 2021, All rights reserved

Author      : ANANDARAJ G 
Description : Application BASE class implemeted.

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
using System.Data;
using System.Configuration;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

public delegate void Button_Click(object sender, GPIEvtArgs e);
public enum GPIEvents
{
    LogoutEvent,
    GeneralEvent
}

public class GPIEvtArgs : EventArgs
{
    GPIEvents _evtId;
    object _object;
    object[] _objectArr;

    public GPIEvtArgs(EventArgs evt)
        : base()
    {
        _evtId = GPIEvents.GeneralEvent;
        _object = null;
        _objectArr = null;
    }

    public GPIEvents EventId
    {
        get { return _evtId; }
        set { _evtId = value; }
    }

    public Object EventObject
    {
        get { return _object; }
        set { _object = (object)value; }
    }

    public Object[] EventObjectArray
    {
        get { return _objectArr; }
        set { _objectArr = (object[])value; }
    }
}


public class AQUAEventsArgs : EventArgs
{


    public bool chkAdd;

    public AQUAEventsArgs(EventArgs evt)
        : base()
    {

    }
}


/// <summary>
/// Base class from where the web forms inherited from.
/// </summary>
/// 


public class AQUABase : System.Web.UI.Page
{
    public GPISession aquaSession;

    public AQUABase()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    protected override void OnLoad(EventArgs e)
    {
        aquaSession = new GPISession(this.Session);
        base.OnLoad(e);
    }

    protected void CheckSession()
    {
        try
        {
            if (aquaSession != null)
                if (aquaSession.UserID == null)
                {
                    Response.Redirect("Login.aspx");
                }
        }
        catch (NullReferenceException ex)
        {
            Response.Redirect("Login.aspx");
        }
    }

    protected void ClearSessionValues()
    {

    }


}