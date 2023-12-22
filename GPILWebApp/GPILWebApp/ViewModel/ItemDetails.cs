using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ItemDetails
/// </summary>
public class ItemDetails
{
    public ItemDetails()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    


    private int m_SNO;
    private string m_ITEMCODE;
    private string m_ITEMCODEGROUP;

    private string m_ITEMGROUP;
    private string m_ITEMTYPE;
    private string m_ITEMDESC;
    private string m_CROP;
    private string m_VARIETY;
    private string m_COSTCATEGORY;
    private string m_ORGNTYPE;


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
    public string ITEMCODE
    {
        get { return m_ITEMCODE; }
        set { m_ITEMCODE = value; }
    }
    public string ITEMCODEGROUP
    {
        get { return m_ITEMCODEGROUP; }
        set { m_ITEMCODEGROUP = value; }
    }
    public string ITEMGROUP
    {
        get { return m_ITEMGROUP; }
        set { m_ITEMGROUP = value; }
    }
    public string ITEMTYPE
    {
        get { return m_ITEMTYPE; }
        set { m_ITEMTYPE = value; }
    }
    public string ITEMDESC
    {
        get { return m_ITEMDESC; }
        set { m_ITEMDESC = value; }
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
    public string COSTCATEGORY
    {
        get { return m_COSTCATEGORY; }
        set { m_COSTCATEGORY = value; }
    }
    public string ORGNTYPE
    {
        get { return m_ORGNTYPE; }
        set { m_ORGNTYPE = value; }
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
    public string LASTUPDATE
    {
        get { return m_LASTUPDATE; }
        set { m_LASTUPDATE = value; }
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