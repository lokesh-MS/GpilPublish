using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Variety
/// </summary>
public class Variety
{
    public Variety()
    {
        //
        // TODO: Add constructor logic here
        //
    }



    private string m_varietyCode = null;
    private string m_VarietyType = null;
    private string m_varietyName = null;
    private string m_varietyDesc = null;
    private string m_status = null;
    private string m_createdBy = null;
 
    public string VarietyCode
    {
        get { return m_varietyCode; }
        set { m_varietyCode = value; }
    }

    public string VarietyType
    {
        get { return m_VarietyType ; }
        set { m_VarietyType = value; }
    }
    public string VarietyName
    {
        get { return m_varietyName; }
        set { m_varietyName = value; }
    }

    public string VarietyDesc
    {
        get { return m_varietyDesc; }
        set { m_varietyDesc = value; }
    }

    public string Status
    {
        get { return m_status; }
        set { m_status = value; }
    }

    public string CreatedBy
    {
        get { return m_createdBy; }
        set { m_createdBy = value; }
    }

}