<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="GRADINGCHARTPROCESS.aspx.cs" Inherits="GPILWebApp.CrystalReport.WebForms.REPORTS.GRADINGCHARTPROCESS" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
    <asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="page-header">

        <h2 style="text-align: center; color: #438EB9">GRADING CHART REPORT</h2>

    </div>


    <div class="row">
        <div class="col-sm-4">
            <div class="form-sm-3-md-4">
                <label class="control-label">FROM Date</label>
                <div class="form-control-sm">
                    <asp:textbox id="txt_Report_Date" runat="server" class="form-control" textmode="Date">
                    </asp:textbox>
                </div>
            </div>
        </div>
        <div class="col-sm-4">
    <div class="form-sm-3-md-4">
        <label class="control-label">TO Date</label>
        <div class="form-control-sm">
            <asp:textbox id="txttodate" runat="server" class="form-control" textmode="Date">
            </asp:textbox>
        </div>
    </div>
</div>
        <div class="col-sm-4">
           
                <div class="form-sm-3-md-4">
                    <label class="control-label">Crop Year</label>
                    <div>
                        <asp:dropdownlist runat="server" cssclass="form-control" id="ddlCrop">
                        <asp:ListItem Value="0">Select To Crop</asp:ListItem>
                    </asp:dropdownlist>
                    </div>
                </div>
            <%--</div>--%>

        </div>



        <div class="col-sm-4">
            <div class="form-sm-3-md-4">
                <label class="control-label">Variety</label>
                <div>
                    <asp:dropdownlist runat="server" cssclass="form-control" id="ddlVariety">
                        <asp:ListItem Value="0">Select To Variety</asp:ListItem>
                    </asp:dropdownlist>
                </div>
            </div>
        </div>
                <div class="col-sm-4">
            <div class="form-sm-3-md-4">
                <label class="control-label">Select Org Code</label>
                <div>
                    <asp:dropdownlist runat="server" cssclass="form-control" id="ddlorgcode">
                        <asp:ListItem Value="0">Select</asp:ListItem>
                    </asp:dropdownlist>
                </div>
            </div>
        </div>
                <div class="col-sm-4">
            <div class="form-sm-3-md-4">
                <label class="control-label">RecipeCode</label>
                <div>
                    <asp:dropdownlist runat="server" cssclass="form-control" id="ddlrecipecode">
                        <asp:ListItem Value="0">Select</asp:ListItem>
                    </asp:dropdownlist>
                </div>
            </div>
        </div>
               <div class="col-sm-4">
           <div class="form-sm-3-md-4">
                    <label class="control-label"></label>
                    <div class="form-control-sm">
                        <asp:RadioButton runat="server" ID="rdoQuantity" GroupName="rdbrpttyp" Text="Quantity (kgs) Only" ForeColor="Black" Checked="true" />
                    </div>
                </div>
            </div>
            <div class="col-sm-4">
         <div class="form-sm-3-md-4">
         <label class="control-label"></label>
         <div class="form-control-sm">
             <asp:RadioButton runat="server" ID="rdoShare" GroupName="rdbrpttyp" Text="Share (%) Only" ForeColor="Black" Checked="true" />
         </div>
     </div>
 </div>
            <div class="col-sm-4">
<div class="form-sm-3-md-4">
         <label class="control-label"></label>
         <div class="form-control-sm">
             <asp:RadioButton runat="server" ID="rdoBoth" GroupName="rdbrpttyp" Text="Both Quantity (kgs) & Share (%)" ForeColor="Black" Checked="true" />
         </div>
     </div>
 </div>




    </div>
    <div class="row">
        <label></label>
    </div>

    <div class="row">
        <div class="col-sm-3">
            <div>
                <label></label>
            </div>
            <div class="form-control-sm">
                <asp:button id="btnview" runat="server" cssclass="btn btn-sm btn-success" text="View" onclick="btnview_Click" />
                <asp:button id="btnclose" runat="server" cssclass="btn btn-sm btn-danger" text="Clear" onclick="btnclose_Click" />
            </div>
        </div>
        <div class="col-sm-3">
            <div class="form-control-sm">
            </div>
        </div>
    </div>

    <hr />



    <div class="col-sm-12" style="width: 100%">
        <center>
             <asp:GridView ID="GridViewSamp" runat="server" BorderColor="#CCCCCC" BorderStyle="Solid" 
            BorderWidth="1px" Font-Names="Verdana" 
            ShowFooter="True" 
            Width="100%" ForeColor="Black" 
                   onpageindexchanging="GridViewSamp_PageIndexChanging" 
               
                onrowdatabound="GridViewSamp_RowDataBound">
            <AlternatingRowStyle BackColor="#FFD4BA" />
            <FooterStyle BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px"  Width="150px" />
            <PagerStyle BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px" 
                       HorizontalAlign="Right"  Width="200px" />
            <HeaderStyle BackColor="#307ECC" ForeColor="white"   BorderColor="#CCCCCC" BorderStyle="Solid"
                BorderWidth="1px" Font-Size="13px" Height="30px" VerticalAlign="Middle" 
                       Width="200px" />
            <RowStyle BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px" 
                Font-Size="13px" Height="20px"  Width="200px" ForeColor="Black" />
        </asp:GridView>
            <div style="margin-top:10px">
                <asp:button id="btnExportToExcel" text="Export to Excel" CssClass="btn btn-sm btn-success"
    runat="server"
    width="110px" height="40px" onclick="btnExportToExcel_Click" />
            </div>
        
        

            </center>
        <br />
        <br />
        
        
        <div style="margin-top:20px">
            <center>
                            <asp:label id="lblSummary" forecolor="Red" backcolor="white" font-size="Large"
    font-bold="True" runat="server"></asp:label>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
<asp:label id="lblSummary1" forecolor="Red" backcolor="white" font-size="Large"
    font-bold="True" runat="server"></asp:label>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
<asp:label id="lblSummary2" forecolor="Red" backcolor="white" font-size="Large"
    font-bold="True" runat="server"></asp:label>
            </center>
            

        </div>
       


    </div>

</asp:Content>