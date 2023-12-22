using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for FarmerDetails
/// </summary>
public class FarmerDetails
{
    public FarmerDetails()
    {
        //
        // TODO: Add constructor logic here
        //
    }



    private int m_SNO;
    private string m_FARMCODE;
    private string m_FARMCATEGORY;
    private string m_FARMNAME;
    private string m_FARMFATHERNAME;
    private string m_VILLAGECODE;
    private string m_SOILTYPE;
    private string m_FARMADDRESS1;
    private string m_FARMADDRESS2;
    private string m_FARMADDRESS3;
    private string m_FARMADDRESS4;
    private string m_FARMADDRESS5;
    private string m_FARMADDRESS6;
    private string m_COUNTRY;
    private string m_PINCODE;
    private string m_TELNO;
    private string m_MOBILENO;

    private string m_CROP;
    private string m_VARIETY;


    private string m_EMAILID;
    private string m_BANKACCOUNTNO;
    private string m_BANKNAME;
    private string m_BRANCHNAME;
    private string m_IFSCCODE;
    private string m_CREATEDBY;
    private string m_CREATEDDATE;
    private string m_LASTUPDATEDBY;
    private string m_LASTUPDATEDDATE;
    private string m_STATUS;
    private string m_FLAG;
    private string m_LASTUPDATE;
    private string m_LOANAMOUNT;
    private string m_BALANCEAMOUNT;
    private string m_REDMARK;
    private string m_ADHAARNO;


    private string m_ALERTFLAG;
    private string m_ALERTMSG;
    private string m_ATTRIBUTE1;
    private string m_ATTRIBUTE2;
    private string m_ATTRIBUTE3;
    private string m_ATTRIBUTE4;
    private string m_ATTRIBUTE5;



    public string ADHAARNO
    {
        get { return m_ADHAARNO; }
        set { m_ADHAARNO = value; }
    }
    public string REDMARK
    {
        get { return m_REDMARK; }
        set { m_REDMARK = value; }
    }
    public string BALANCEAMOUNT
    {
        get { return m_BALANCEAMOUNT; }
        set { m_BALANCEAMOUNT = value; }
    }
    public string CROP
    {
        get { return m_CROP; }
        set { m_CROP = value; }
    }
    public string VARIETY
    {
        get { return m_VARIETY; }
        set { m_VARIETY = value; }
    }


    public int SNO
    {
        get { return m_SNO; }
        set { m_SNO = value; }
    }
    public string FARMCODE
    {
        get { return m_FARMCODE; }
        set { m_FARMCODE = value; }
    }
    public string FARMCATEGORY
    {
        get { return m_FARMCATEGORY; }
        set { m_FARMCATEGORY = value; }
    }
    public string FARMNAME
    {
        get { return m_FARMNAME; }
        set { m_FARMNAME = value; }
    }
    public string FARMFATHERNAME
    {
        get { return m_FARMFATHERNAME; }
        set { m_FARMFATHERNAME = value; }
    }
    public string VILLAGECODE
    {
        get { return m_VILLAGECODE; }
        set { m_VILLAGECODE = value; }
    }
    public string SOILTYPE
    {
        get { return m_SOILTYPE; }
        set { m_SOILTYPE = value; }
    }
    public string FARMADDRESS1
    {
        get { return m_FARMADDRESS1; }
        set { m_FARMADDRESS1 = value; }
    }
    public string FARMADDRESS2
    {
        get { return m_FARMADDRESS2; }
        set { m_FARMADDRESS2 = value; }
    }
    public string FARMADDRESS3
    {
        get { return m_FARMADDRESS3; }
        set { m_FARMADDRESS3 = value; }
    }
    public string FARMADDRESS4
    {
        get { return m_FARMADDRESS4; }
        set { m_FARMADDRESS4 = value; }
    }
    public string FARMADDRESS5
    {
        get { return m_FARMADDRESS5; }
        set { m_FARMADDRESS5 = value; }
    }
    public string FARMADDRESS6
    {
        get { return m_FARMADDRESS6; }
        set { m_FARMADDRESS6 = value; }
    }
    public string COUNTRY
    {
        get { return m_COUNTRY; }
        set { m_COUNTRY = value; }
    }
    public string PINCODE
    {
        get { return m_PINCODE; }
        set { m_PINCODE = value; }
    }
    public string TELNO
    {
        get { return m_TELNO; }
        set { m_TELNO = value; }
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
    public string BANKACCOUNTNO
    {
        get { return m_BANKACCOUNTNO; }
        set { m_BANKACCOUNTNO = value; }
    }
    public string BANKNAME
    {
        get { return m_BANKNAME; }
        set { m_BANKNAME = value; }
    }
    public string BRANCHNAME
    {
        get { return m_BRANCHNAME; }
        set { m_BRANCHNAME = value; }
    }
    public string IFSCCODE
    {
        get { return m_IFSCCODE; }
        set { m_IFSCCODE = value; }
    }
    public string CREATEDBY
    {
        get { return m_CREATEDBY; }
        set { m_CREATEDBY = value; }
    }
    public string CREATEDDATE
    {
        get { return m_CREATEDDATE; }
        set { m_CREATEDDATE = value; }
    }
    public string LASTUPDATEDBY
    {
        get { return m_LASTUPDATEDBY; }
        set { m_LASTUPDATEDBY = value; }
    }
    public string LASTUPDATEDDATE
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
    public string LASTUPDATE
    {
        get { return m_LASTUPDATE; }
        set { m_LASTUPDATE = value; }
    }
    public string LOANAMOUNT
    {
        get { return m_LOANAMOUNT; }
        set { m_LOANAMOUNT = value; }
    }

    public string ALERTFLAG
    {
        get { return m_ALERTFLAG; }
        set { m_ALERTFLAG = value; }
    }
    public string ALERTMSG
    {
        get { return m_ALERTMSG; }
        set { m_ALERTMSG = value; }
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