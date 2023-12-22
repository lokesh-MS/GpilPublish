using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for BankDetails
/// </summary>
public class BankDetails
{
    public BankDetails()
    {
        //
        // TODO: Add constructor logic here
        //
    }
   

    private string m_BankName;
    private string m_Branch;
    private string m_IFSC;
    private string m_LASTUPDATE;
    private string m_ATTRIBUTE1;
    private string m_ATTRIBUTE2;
    private string m_ATTRIBUTE3;
    private string m_ATTRIBUTE4;
    private string m_ATTRIBUTE5;
    public string BankName
    {
        get { return m_BankName; }
        set { m_BankName = value; }
    }
    public string Branch
    {
        get { return m_Branch; }
        set { m_Branch = value; }
    }
    public string IFSC
    {
        get { return m_IFSC; }
        set { m_IFSC = value; }
    }
    public string ATTRIBUTE1
    {
        get { return m_ATTRIBUTE1; }
        set { m_ATTRIBUTE1 = value; }
    }
    public string ATTRIBUTE2
    {
        get { return m_ATTRIBUTE2; }
        set { m_ATTRIBUTE2 = value; }
    }
    public string ATTRIBUTE3
    {
        get { return m_ATTRIBUTE3; }
        set { m_ATTRIBUTE3 = value; }
    }
    public string ATTRIBUTE4
    {
        get { return m_ATTRIBUTE4; }
        set { m_ATTRIBUTE4 = value; }
    }
    public string ATTRIBUTE5
    {
        get { return m_ATTRIBUTE5; }
        set { m_ATTRIBUTE5 = value; }
    }

}