using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for RateMaster
/// </summary>
public class RateMaster
{
    public RateMaster()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    
    private string m_LocCode;
    private string m_ItemCode;
    private string m_Rate;
    private System.DateTime m_LastUpdatedDate;

    public string LocCode
    {
        get { return m_LocCode; }
        set { m_LocCode = value; }
    }
    public string ItemCode
    {
        get { return m_ItemCode; }
        set { m_ItemCode = value; }
    }
    public string Rate
    {
        get { return m_Rate; }
        set { m_Rate = value; }
    }
    public DateTime LastUpdatedDate
    {
        get { return m_LastUpdatedDate; }
        set { m_LastUpdatedDate = value; }
    }
}