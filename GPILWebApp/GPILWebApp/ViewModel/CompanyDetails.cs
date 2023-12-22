using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for CompanyMaster
/// </summary>
public class CompanyDetails
{
    public CompanyDetails()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    //private int _SNO;

    private string m_COMPANYCODE;
    private string m_COMPANYNAME;
    private string m_SUPPLIERFLAG;
    private string m_SUPPLIEDTO;
    private string m_CREATEDBY;
    private System.DateTime m_CREATEDDATE;
    private string m_LASTUPDATEDBY;
    private System.DateTime m_LASTUPDATEDDATE;
    private string m_STATUS;
    private string m_FLAG;  
    private string m_COMPSHORTNAME;
    private string m_COMPGROUPCODE;

    public string COMPANYCODE
    {
        get { return m_COMPANYCODE; }
        set { m_COMPANYCODE = value; }
    }
    public string COMPANYNAME
    {
        get { return m_COMPANYNAME; }
        set { m_COMPANYNAME = value; }
    }
    public string SUPPLIERFLAG
    {
        get { return m_SUPPLIERFLAG; }
        set { m_SUPPLIERFLAG = value; }
    }
    public string SUPPLIEDTO
    {
        get { return m_SUPPLIEDTO; }
        set { m_SUPPLIEDTO = value; }
    }
    public string CREATEDBY
    {
        get { return m_CREATEDBY; }
        set { m_CREATEDBY = value; }
    }
    public DateTime CREATEDDATE
    {
        get { return m_CREATEDDATE; }
        set { m_CREATEDDATE = value; }
    }
    public string LASTUPDATEDBY
    {
        get { return m_LASTUPDATEDBY; }
        set { m_LASTUPDATEDBY = value; }
    }
    public DateTime LASTUPDATEDDATE
    {
        get { return m_LASTUPDATEDDATE; }
        set { m_LASTUPDATEDDATE = value; }
    }
    public string STATUS
    {
        get { return m_STATUS; }
        set { m_STATUS = value; }
    }
    public string FLAG
    {
        get { return m_FLAG; }
        set { m_FLAG = value; }
    }
    public string COMPSHORTNAME
    {
        get { return m_COMPSHORTNAME; }
        set { m_COMPSHORTNAME = value; }
    }
    public string COMPGROUPCODE
    {
        get { return m_COMPGROUPCODE; }
        set { m_COMPGROUPCODE = value; }
    }
   

}