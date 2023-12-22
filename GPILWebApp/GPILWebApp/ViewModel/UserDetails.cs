using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for UserDetails
/// </summary>
public class UserDetails
{
    public UserDetails()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    
    private int m_SNO;
    private string m_USERID;
    private string m_USERNAME;

    private string m_PASSWORD;
    private string m_USERERPNAME;
    private string m_EMPCODE;
    private string m_DESIGNATION;
    private string m_DEPARTMENT;
    private string m_USERRIGHTS;
    private string m_MOBILENO;
    private string m_EMAILID;

    private string m_CREATEDBY;
    private System.DateTime m_CREATEDDATE;
    private string m_LASTUPDATEDBY;
    private System.DateTime m_LASTUPDATEDDATE;
    private string m_STATUS;
    private string m_FLAG;
    private string m_LASTUPDATE;
    private string m_ATTRIBUTE1;
    private string m_ATTRIBUTE2;
    private string m_ATTRIBUTE3;
    private string m_ATTRIBUTE4;
    private string m_ATTRIBUTE5;

    public int SNO
    {
        get { return m_SNO; }
        set { m_SNO = value; }
    }
    public string USERID
    {
        get { return m_USERID; }
        set { m_USERID = value; }
    }

    public string USERNAME
    {
        get { return m_USERNAME; }
        set { m_USERNAME = value; }
    }
    public string PASSWORD
    {
        get { return m_PASSWORD; }
        set { m_PASSWORD = value; }
    }
    public string USERERPNAME
    {
        get { return m_USERERPNAME; }
        set { m_USERERPNAME = value; }
    }
    public string EMPCODE
    {
        get { return m_EMPCODE; }
        set { m_EMPCODE = value; }
    }
    public string DESIGNATION
    {
        get { return m_DESIGNATION; }
        set { m_DESIGNATION = value; }
    }
    public string DEPARTMENT
    {
        get { return m_DEPARTMENT; }
        set { m_DEPARTMENT = value; }
    }
    public string USERRIGHTS
    {
        get { return m_USERRIGHTS; }
        set { m_USERRIGHTS = value; }
    }
    public string MOBILENO
    {
        get { return m_MOBILENO; }
        set { m_MOBILENO = value; }
    }
    public string EMAILID
    {
        get { return m_EMAILID; }
        set { m_EMAILID = value; }
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